using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//プレイヤから逃げる敵
public class EscapeEnemy1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;
	bool dead = false;
	bool damageSet;					 //被ダメージ処理、一時的に移動不可(下記参照)
	public float DamageTime = 0.5f;	 //ダメージ処理(硬直)時間
	bool freezeSet;					 //フリーズ処理、一時的に移動不可
	public float FreezeTime = 1.0f;	 //フリーズ処理(硬直)時間
	public float LastEnemySpeed;	 //ダメージ、フリーズ処理する前の敵の基本スピード
	private float chargeTime = 5.0f; //方向転換するまでの時間制限
	private float timeCount;
	public float Dash = 3.0f;		 //通常移動とダッシュ移動の比率
    Rigidbody rigidbody;
    int LayerMask = ~(1 << 8);		//8はlayerのPlayer。　playerにはRayCastHitしない
    public bool RighrtMove = false;
    public bool LeftMove = false;
    public float RandomMoeCount = 0;

    // Use this for initialization
    void Start ()
    {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
        rigidbody = GetComponent<Rigidbody>();
    }

	// Update is called once per frame
	void Update ()
    {
		damageSet = GetComponent<EnemyBasic> ().DamageSet;
		freezeSet = GetComponent<EnemyBasic> ().FreezeSet;
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.01f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		enemyBasic.timer += Time.deltaTime;
		timeCount += Time.deltaTime;
        
        rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.0f))
        {
            RandomMoeCount -= Time.deltaTime;
            if (RandomMoeCount <= 0)
            {
                RandomMove();
                RandomMoeCount = 5;
            }
        }
        if (RighrtMove == true)
        {
            //Debug.Log("右");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 10.0f);
            //rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed * MoveTime);
        }
        else if (LeftMove == true)
        {
            //Debug.Log("左");
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 10.0f);
            //rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed * MoveTime);
        }
        //Playerが近くにいなければ敵の移動方向をランダムで変える
        else if (timeCount > chargeTime)
        {
            Vector3 course = new Vector3(0, Random.Range(0, 180), 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
            timeCount = 0;
        }
        
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
            //ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
		}
        // ターゲット（プレイヤー）との距離がSearch以内なら
        if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの反対方向を向きダッシュで逃げる
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(transform.position - enemyBasic.target.transform.position ), Time.deltaTime * enemyBasic.EnemySpeed * Dash);
                rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed * Dash);
        }
		// Animator の dead が true なら Update 処理を抜ける
		if( enemyBasic.animator.GetBool("dead") == true ) return;

		//damageSet時、スピードが0なら何もしない。0でないならDamageSetCoroutine起動（下記参照）
		if (damageSet == true) {
			if (LastEnemySpeed == 0) {
				return;
			} else {
				StartCoroutine ("DamageSetCoroutine");
			}
		}
		//freezeSet時、スピードが0なら何もしない。0でないならDamageSetCoroutine起動（下記参照）
		if (freezeSet == true) {
			if (LastEnemySpeed == 0) {
				return;
			} else {
				StartCoroutine ("FreezeSetCoroutine");
			}
		}
	}

    public void RandomMove()
    {
        int num = Random.Range(0, 9);
        if (num <= 4)
        {
            RighrtMove = true;
            LeftMove = false; ;
        }
        else if (num > 5)
        {
            LeftMove = true;
            RighrtMove = false;
        }
    }

    //攻撃が当たったらDamageTime分だけSpeedをゼロにする（動きを止める）
    IEnumerator DamageSetCoroutine (){
		enemyBasic.DamageSet = false;
		LastEnemySpeed = enemyBasic.EnemySpeed;			//直前の動きの速さをLastEnemySpeedとして保存
		enemyBasic.EnemySpeed = 0;						//スピードを0にする（硬直処理）
		yield return new WaitForSeconds(DamageTime);	//DamageTimeが経過したら
		enemyBasic.EnemySpeed = LastEnemySpeed;			//LastEnemySpeedに戻して再び移動可能にする
		//Debug.Log (LastEnemySpeed);
	}

	//攻撃が当たったらFreezeTime分だけSpeedをゼロにする（動きを止める）
	IEnumerator FreezeSetCoroutine (){
		freezeSet = false;
		float LastEnemySpeed = enemyBasic.EnemySpeed;
		enemyBasic.EnemySpeed = 0;
		yield return new WaitForSeconds(FreezeTime);
		enemyBasic.EnemySpeed = LastEnemySpeed;
	}
}

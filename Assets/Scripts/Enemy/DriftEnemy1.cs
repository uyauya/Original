using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//フヨフヨ漂いながら移動する（収縮する）、ヒットさせるとしぼんでいく
public class DriftEnemy1 : MonoBehaviour {
	private Animator animator;			// 《Animator》を使う
	protected EnemyBasic enemyBasic;
    Rigidbody rigidbody;
    public Transform EnemyMuzzle;       // 弾発射元（銃口）
    public GameObject EnemyFire;		// 弾
    private float chargeTime = 5.0f;    //方向転換するまでの時間制限
    private float timeCount;
    public float Minimum = 1.0f;		// 最小（元の）サイズ
	public float Magspeed = 0.001f;		// 拡大スピード
	public float Magnification = 1.0f;	// 拡大率
	public Vector3 BasicPoint;			// 出現時の座標（地上からの高さを決める）
	bool damageSet;					 	//被ダメージ処理、一時的に移動不可(下記参照)
	public float DamageTime = 0.5f;	 	//ダメージ処理(硬直)時間
	bool freezeSet;					 	//フリーズ処理、一時的に移動不可
	public float FreezeTime = 1.0f;	 	//フリーズ処理(硬直)時間
	public float LastEnemySpeed;	 	//ダメージ、フリーズ処理する前の敵の基本スピード
    int LayerMask = ~(1 << 8);		    //8はlayerのPlayer。　playerにはRayCastHitしない
    public bool RighrtMove = false;
    public bool LeftMove = false;
    public bool UpMove = false;
    public float RandomMoeCount = 0;
    int AttackPhase = 0;
    float AttackPhaseTime = 0.0f;

    void Start () {
		animator = GetComponent<Animator>();			// Animatorを使う場合は設定する
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
        rigidbody = GetComponent<Rigidbody>();
        BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 1.3f, this.transform.position.z);
	}

    void Update() {
        AttackPhaseTime += Time.deltaTime;
        switch (AttackPhase)
        {

            case 1:
                rigidbody.velocity = Vector3.zero;
                if (AttackPhaseTime >= 6)
                {
                    AttackPhase = 2;
                    AttackPhaseTime = 0;
                }

                //一定間隔でショット
                enemyBasic.shotInterval += Time.deltaTime;
                // 次の攻撃待ち時間が一定以上になれば
				if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax)
                {
					animator.SetTrigger ("BombFire");
                    GameObject enemyFire = GameObject.Instantiate(EnemyFire) as GameObject;
                    enemyFire.transform.position = EnemyMuzzle.position;
                    enemyFire.transform.rotation = EnemyMuzzle.transform.rotation;
                    enemyBasic.shotInterval = 0;
                }
                break;
            case 2:
                if (AttackPhaseTime >= 3)
                {
                    AttackPhase = 0;
                    AttackPhaseTime = 0;
                }
                break;
        }
        damageSet = GetComponent<EnemyBasic>().DamageSet;
        freezeSet = GetComponent<EnemyBasic>().FreezeSet;

        //rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
        gameObject.transform.eulerAngles = new Vector3(1, Ros.y, 1);
        enemyBasic.timer += Time.deltaTime;
        timeCount += Time.deltaTime;
        if (AttackPhase == 0) { 
        rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        }
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
		else if (UpMove == true)
		{
			//Debug.Log("左");

		}
		//Playerが近くにいなければ敵の移動方向をランダムで変える
		else if (timeCount > chargeTime)
		{
			Vector3 course = new Vector3(0, Random.Range(0, 180), 0);
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			timeCount = 0;
		}
        // 拡大、縮小繰り返す
        // MinimumのサイズからMathf.Sin式でX、Y、Z軸に拡大率をかける
        this.transform.localScale = new Vector3(this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification,
												this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification, 
												this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification);
		//Debug.Log (transform.localScale.x);
		//Debug.Log (this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification);
		enemyBasic.timer += Time.deltaTime;
		// 上下移動
		// 上記同様Y軸にSin式を入れ上下させる
		this.transform.position = new Vector3 (this.transform.position.x, this.BasicPoint.y + Mathf.Sin (Time.time), 
			this.transform.position.z);
        //敵の攻撃範囲を設定する
        //相手の位置と自分の位置の差がTargetRange以内なら
        if (AttackPhase == 0)
        {
            if (Vector3.Distance(enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange)
            {
                //ターゲットの方を徐々に向く
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                    (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
                rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
            }
            //Playerが近くにいなければ敵の移動方向をランダムで変える
            else if (timeCount > chargeTime)
            {
                Vector3 course = new Vector3(0, Random.Range(0, 360), 0);
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                    (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
                timeCount = 0;
            }
        }
        // ターゲット（プレイヤー）との距離がSearch以内なら
        if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
            if(AttackPhase == 0)
            {
                AttackPhase = 1;
				animator.SetTrigger ("BombFire");
				Debug.Log("ファイア");
                /*iTween.RotateTo(gameObject, iTween.Hash("x", 30f, "time", 0.3f, "easetype", iTween.EaseType.linear));  //下向いて
                iTween.RotateAdd(gameObject, iTween.Hash("y", 90f, "time", 2f, "delay",0.3f,"easetype", iTween.EaseType.linear));  //右向いて
                iTween.RotateAdd(gameObject, iTween.Hash("y", -179f,"time", 2f, "delay", 2.3f, "easetype", iTween.EaseType.linear));   //左向いて
                iTween.RotateAdd(gameObject, iTween.Hash("y", 90f, "time", 2f, "delay",0.3f,"easetype", iTween.EaseType.linear));  //正面に戻して
                iTween.RotateAdd(gameObject, iTween.Hash("x", -30f, "time", 2f, "delay", 4.3f, "easetype", iTween.EaseType.linear));  //上向いて元に戻す*/
                AttackPhaseTime = 0.0f;
            }
						
		}
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

	//WallBlockに当たる寸前にランダムで方向転換（上記参照）
    public void RandomMove()
    {
        int num = Random.Range(0, 8);
		//0～2の間なら右
        if (num <= 2)
        {
            RighrtMove = true;
            LeftMove = false;
            UpMove = false;
        }
		//3～5の間なら左
        else if (num > 3 && num <= 5)
        {
            LeftMove = true;
            RighrtMove = false;
            UpMove = false;
        }
		//6～8の間なら上
        else if (num > 6)
        {
            UpMove = true;
            RighrtMove = false;
            LeftMove = false;
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

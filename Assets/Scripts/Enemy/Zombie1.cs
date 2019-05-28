
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

// ゾンビ（うろうろ歩くザコキャラ）
public class Zombie1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;//継承元（protectedにする）のEnemyBasicをenemyBasicとする
	bool dead = false;				//死亡判定
	bool damageSet;					//被ダメージ処理、一時的に移動不可(下記参照)
	public float DamageTime = 0.5f;	//ダメージ処理(硬直)時間
	bool freezeSet;					//フリーズ処理、一時的に移動不可
	public float FreezeTime = 1.0f;	//フリーズ処理(硬直)時間
	public float LastEnemySpeed;	//ダメージ、フリーズ処理する前の敵の基本スピード
	public float Speed;
	//public float MoveTime;			//自動的に進む時間（障害物が有った時に使用）
	Rigidbody rigidbody;
    //int layerMask = ~0;
	int LayerMask = ~(1 << 8);		//8はlayerのPlayer。　playerにはRayCastHitしない
    public bool RighrtMove = false;
    public bool LeftMove = false;
    public float RandomMoeCount = 0;
    public float InvincibleTime = 0.5f;                    // 無敵時間
    public float KnockBackRange = 1.5f;

    /*[CustomEditor(typeof(Zombie1))]
	public class Zombie1 : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			Zombie1 Zn1 = target as Zombie1;
			Zn1.DamageTime= EditorGUILayout.FloatField( "被ダメージ硬直時間", Zn1.DamageTime);
			Zn1.FreezeTime= EditorGUILayout.FloatField( "フリーズ硬直時間", Zn1.FreezeTime);
		}
	}*/

    // Use this for initialization
    void Start () {
		// EnemyBasicスクリプトのデータを最初に呼び出しenemyBasicとする
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		LastEnemySpeed = enemyBasic.EnemySpeed;
		rigidbody = GetComponent<Rigidbody>();
		//gameObject.layer = LayerMask.NameToLayer("Enemy");
	}

	// Update is called once per frame
	void Update () 
	{
		damageSet = GetComponent<EnemyBasic> ().DamageSet;
		freezeSet = GetComponent<EnemyBasic> ().FreezeSet;
		// Animator の dead が true なら Update 処理を抜ける
		if( enemyBasic.animator.GetBool("dead") == true ) return;
		// オブジェクトの場所取りをする
		Vector3 Pog = this.gameObject.transform.position;
		// Y軸（高さ）を発生位置から0.01上で固定（Y軸を固定してオブジェクトの傾きを防ぐ）
		gameObject.transform.position = new Vector3(Pog.x , 0, Pog.z);
		// 横回転設定
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		enemyBasic.timer += Time.deltaTime;
		// ターゲット（プレイヤ）と自分の距離がTargetRange値以内なら
        if(Vector3.Distance(enemyBasic.battleManager.Player.transform.position, transform.position) <= enemyBasic.TargetRange) 
		{
		//ターゲットの方を徐々に向く
		transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
        (enemyBasic.battleManager.Player.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
            // enemySpeed × 時間でプレイヤに向かって直線的に移動
            //障害物など全て無視して等速（最初からトップスピード）で進む場合はtransform.position +=処理（障害物はすり抜けるワープ処理）
            //transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
            //徐々に加速させる場合はrigidbody.AddForce（後ろにForceMode.VelocityChangeで質量無視）
            //質量無視にすれば慣性の動きもなくなるので、惰性で滑る事も無くなる）
            //rigidbody.AddForce(transform.forward * Time.deltaTime * enemyBasic.EnemySpeed, ForceMode.VelocityChange);
            //障害物判定＆＆等速の場合はrigidbody.velocity =にする。
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
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 10.0f);
                //rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed * MoveTime);
            }
            else if (LeftMove == true)
                {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 10.0f);
                //rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed * MoveTime);
            }
		}

        // ターゲット（プレイヤー）との距離がSearch値以内なら
        //if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
        else if (Vector3.Distance(enemyBasic.battleManager.Player.transform.position, transform.position) <= enemyBasic.Search) 
		{
                //ターゲットの方を徐々に向く
                // Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
                // Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
                transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
                (enemyBasic.battleManager.Player.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemySpeed);
				//アニメーターをattackに変更（AnimatorのParametersと同じ名前にする）
			//SetTriggerを使う場合、戻りの部分のHasExitTimeに✔を入れておく。入れないと戻りの判定が出来なくて
			//アニメーションがattack後デフォルトアニメーションに戻らなくなる
            enemyBasic.animator.SetTrigger ("attack");
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
    IEnumerator DamageSetCoroutine ()
    {
        iTween.MoveTo(gameObject, iTween.Hash(
            // その場からKnockBackRange数値分後(-transform.forwardで後)に移動
            "position", transform.position - (transform.forward * KnockBackRange),
            // 無敵(ダメージ判定なし)時間設定（秒）
            "time", InvincibleTime,
            "easetype", iTween.EaseType.linear
        ));
        //Debug.Log("下がった" + KnockBackRange);
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

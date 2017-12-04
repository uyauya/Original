using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ゾンビ（うろうろ歩くザコキャラ）
public class Zombie1 : MonoBehaviour {

	// 継承元（protectedにする）のEnemyBasicをenemyBasicとする
	protected EnemyBasic enemyBasic;
	bool dead = false;
	public Vector3 localGravity;	// 重力(x,y,z)
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		// EnemyBasicスクリプトのデータを最初に呼び出しenemyBasicとする
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		//enemyBasic.Initialize ();
		rb = this.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}

	void FixedUpdate () {
		setLocalGravity ();
	}

	void setLocalGravity(){
		rb.AddForce (localGravity, ForceMode.Acceleration);
	}

	// Update is called once per frame
	void Update () {
		// Animator の dead が true なら Update 処理を抜ける
		if( enemyBasic.animator.GetBool("dead") == true ) return;
		// オブジェクトの場所取りをする
		Vector3 Pog = this.gameObject.transform.position;
		// Y軸（高さ）を発生位置から0.01上で固定
		gameObject.transform.position = new Vector3(Pog.x , 0.01f, Pog.z);
		// 横回転設定（Y軸を固定）
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		enemyBasic.timer += Time.deltaTime;
		// ターゲット（プレイヤ）と自分の距離がTargetRange以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			// EnemySpeed × 時間で移動
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
		}

		// ターゲット（プレイヤー）との距離が0.5以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemySpeed);

			enemyBasic.animator.SetTrigger ("attack");
			//Debug.Log ("hit");
		}


	}
}

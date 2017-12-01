using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallBoss02 : MonoBehaviour {

	public int TargetPosition;
	protected EnemyBasic enemyBasic;
	bool dead = false;

	void Start () {
		// EnemyBasic接続用
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		//if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) > TargetPosition) {
		//	return;
		//}
	}


	void Update () {
		enemyBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 30) {

			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * 5);
			transform.position += transform.forward * Time.deltaTime * 1;	
		}

		// ターゲット（プレイヤー）との距離が0.5以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 1) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * 5);
			enemyBasic.animator.SetBool ("attack", true);
			//Debug.Log ("hit");
		}
		// Animator の dead が true なら Update 処理を抜ける
		if( enemyBasic.animator.GetBool("dead") == true ) return;
	}
		
}

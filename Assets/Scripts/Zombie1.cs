using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie1 : EnemyBasic {

	// Use this for initialization
	void Start () {
		base.Initialize ();

	}

	// Update is called once per frame
	void Update () {
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.01f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (target.transform.position, transform.position) <= TargetRange) {

			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(target.transform.position - transform.position), Time.deltaTime * EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * EnemySpeed;
		}

		// ターゲット（プレイヤー）との距離が0.5以内なら
		if (Vector3.Distance (target.transform.position, transform.position) <= Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(target.transform.position - transform.position), Time.deltaTime * EnemySpeed);

			animator.SetTrigger ("attack");
			//Debug.Log ("hit");
		}
		// Animator の dead が true なら Update 処理を抜ける
		if( animator.GetBool("dead") == true ) return;
	}
}

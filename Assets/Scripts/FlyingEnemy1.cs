using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 飛行キャラ
public class FlyingEnemy1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;

	void Start () {	
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
	}

	void Update () {	
		
		//敵の攻撃範囲を設定する
		if( Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 30 ){

			//ターゲットの方向を向く
			//transform.LookAt(target.transform);

			//スムーズにターゲットの方向を向く
			Quaternion targetRotation = Quaternion.LookRotation (enemyBasic.target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);

			//弾を発射する
			enemyBasic.shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if(enemyBasic.shotInterval > enemyBasic.shotIntervalMax){
				// その場から発射して攻撃待ち時間を０に戻す
				Instantiate(enemyBasic.shot, transform.position, transform.rotation);
				enemyBasic.shotInterval = 0;
			}
		}


		enemyBasic.timer += Time.deltaTime;

		//経過時間に応じてレベルを上げる
		if (enemyBasic.timer < 5)
			enemyBasic.enemyLevel = 1;
		else if(enemyBasic.timer < 10)
			enemyBasic.enemyLevel = 2;
		else if(enemyBasic.timer < 15 )
			enemyBasic.enemyLevel = 3;
		else if(enemyBasic.timer >= 15 ){
			enemyBasic.enemyLevel = 4;
			//レベル４：攻撃間隔が短くなる
			enemyBasic.shotIntervalMax = 0.5F;
		}

		//レベル２：プレイヤーが一定範囲に近づいたら攻撃
		if(enemyBasic.enemyLevel >= 2) {
			// ターゲット（プレイヤー）との距離が30以内なら
			if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 30) {

				//ターゲットの方を徐々に向く
				// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
				// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
					(enemyBasic.target.transform.position - transform.position), Time.deltaTime * 5);

				//一定間隔でショット
				enemyBasic.shotInterval += Time.deltaTime;
				// 次の攻撃待ち時間が一定以上になれば
				if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {

					Instantiate (enemyBasic.shot, transform.position, transform.rotation);
					enemyBasic.shotInterval = 0;
				}
			}else{

				//レベル３：距離に関係なくプレイヤーに自分から近づく
				if(enemyBasic.enemyLevel >= 3){
					transform.rotation = Quaternion.Slerp 
						(transform.rotation, Quaternion.LookRotation (enemyBasic.target.transform.position - transform.position), 
							Time.deltaTime * 5);
					transform.position += transform.forward * Time.deltaTime * 20;
				}
			}
		}
	}
}
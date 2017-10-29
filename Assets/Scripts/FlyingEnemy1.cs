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
		
			// ターゲット（プレイヤー）との距離が30以内なら
			if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 5) {
				//ターゲットの方を徐々に向く
				// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
				// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
					(enemyBasic.target.transform.position - transform.position), Time.deltaTime * 5);
				transform.position += transform.forward * Time.deltaTime * 20;
				//一定間隔でショット
				enemyBasic.shotInterval += Time.deltaTime;
				// 次の攻撃待ち時間が一定以上になれば
				if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
					Instantiate (enemyBasic.shot, transform.position, transform.rotation);
					enemyBasic.shotInterval = 0;
				}
					
			}
		}
}
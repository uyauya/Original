using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

// 定期的にジャンプする
public class JumpEnemy1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;//継承元（protectedにする）のEnemyBasicをenemyBasicとする
	bool dead = false;				//死亡判定
	bool damageSet;					//被ダメージ処理、一時的に移動不可(下記参照)
	public float DamageTime = 0.5f;	//ダメージ処理(硬直)時間
	bool freezeSet;					//フリーズ処理、一時的に移動不可
	public float FreezeTime = 1.0f;	//フリーズ処理(硬直)時間
	public float LastEnemySpeed;	//ダメージ、フリーズ処理する前の敵の基本スピード

	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
	}
		
	void Update () {
		damageSet = GetComponent<EnemyBasic> ().DamageSet;
		freezeSet = GetComponent<EnemyBasic> ().FreezeSet;
		//時間計測開始
		enemyBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		// ターゲット（プレイヤ）と自分の距離がTargetRange値以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			// enemySpeed × 時間でプレイヤに向かって直線的に移動
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
		}
		//timeElapsed時間計開始。
		enemyBasic.timeElapsed += Time.deltaTime;
		//timeElapsedがtimeOut以上になったらJumpForce（ジャンプ）
		if (enemyBasic.timeElapsed >= enemyBasic.timeOut) {
			transform.position += transform.up * Time.deltaTime * enemyBasic.JumpForce;
			//時間計測終了(リセット)
			enemyBasic.timeElapsed = 0.0f;
		}
		// ターゲット（プレイヤー）との距離が0.5以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemySpeed);
			//上記同様の処理でジャンプしながら突っ込む
			transform.position += transform.forward * Time.deltaTime * 1;
			if (enemyBasic.timeElapsed >= enemyBasic.timeOut) {
				transform.position += transform.up * Time.deltaTime * enemyBasic.JumpForce;
				enemyBasic.timeElapsed = 0.0f;
			}
			//animator.SetBool ("attack", true);
			//Debug.Log ("hit");
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

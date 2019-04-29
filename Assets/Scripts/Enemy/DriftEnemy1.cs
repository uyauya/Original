using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//フヨフヨ漂いながら移動する（収縮する）、ヒットさせるとしぼんでいく
public class DriftEnemy1 : MonoBehaviour {
	protected EnemyBasic enemyBasic;
    Rigidbody rigidbody;
    public float Minimum = 140.0f;		// 最小（元の）サイズ
	public float Magspeed = 0.001f;		// 拡大スピード
	public float Magnification = 1.0f;	// 拡大率
	public Vector3 BasicPoint;			// 出現時の座標（地上からの高さを決める）
	bool damageSet;					 	//被ダメージ処理、一時的に移動不可(下記参照)
	public float DamageTime = 0.5f;	 	//ダメージ処理(硬直)時間
	bool freezeSet;					 	//フリーズ処理、一時的に移動不可
	public float FreezeTime = 1.0f;	 	//フリーズ処理(硬直)時間
	public float LastEnemySpeed;	 	//ダメージ、フリーズ処理する前の敵の基本スピード

	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
        rigidbody = GetComponent<Rigidbody>();
        BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z);
	}

	void Update () {
		damageSet = GetComponent<EnemyBasic> ().DamageSet;
		freezeSet = GetComponent<EnemyBasic> ().FreezeSet;
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
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
            rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        }
		//enemyBasic.timeElapsed += Time.deltaTime;
		//if (enemyBasic.timeElapsed >= enemyBasic.timeOut) {
		//	transform.position += transform.up * Time.deltaTime * enemyBasic.JumpForce;
		//	enemyBasic.timeElapsed = 0.0f;
		//}

		// ターゲット（プレイヤー）との距離がSearch以内なら
		//if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 5.0f) {
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//Debug.Log ("検出");
			//if (Vector3.Distance (Player.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
			//一定間隔でショット
			/*enemyBasic.shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
				Instantiate (enemyBasic.shot, transform.position, transform.rotation);
				enemyBasic.shotInterval = 0;
			}*/			
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

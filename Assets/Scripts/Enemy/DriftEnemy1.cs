using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//フヨフヨ漂いながら移動する（収縮する）、ヒットさせるとしぼんでいく
public class DriftEnemy1 : MonoBehaviour {
	protected EnemyBasic enemyBasic;
	public float Minimum = 140.0f;			// 最小（元の）サイズ
	public float Magspeed = 0.001f;			// 拡大スピード
	public float Magnification = 1.0f;		// 拡大率
	public Vector3 BasicPoint;

	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z);
	}

	void Update () {
		// 拡大、縮小繰り返す
		this.transform.localScale = new Vector3(this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification,
			this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification, this.Minimum 
			+ Mathf.Sin(Time.time * this.Magspeed) * this.Magnification);
		//Debug.Log (transform.localScale.x);
		//Debug.Log (this.Minimum + Mathf.Sin(Time.time * this.Magspeed) * this.Magnification);
		enemyBasic.timer += Time.deltaTime;
		// 上下移動
		this.transform.position = new Vector3 (this.transform.position.x, this.BasicPoint.y + Mathf.Sin (Time.time), 
			this.transform.position.z);
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {

			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
		}
		enemyBasic.timeElapsed += Time.deltaTime;
		if (enemyBasic.timeElapsed >= enemyBasic.timeOut) {
			transform.position += transform.up * Time.deltaTime * enemyBasic.JumpForce;
			enemyBasic.timeElapsed = 0.0f;
		}

		// ターゲット（プレイヤー）との距離が5以内なら
		//if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 5.0f) {
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//Debug.Log ("検出");
			//if (Vector3.Distance (Player.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * 5);
			transform.position += transform.forward * Time.deltaTime * 5;
			//一定間隔でショット
			/*enemyBasic.shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
				Instantiate (enemyBasic.shot, transform.position, transform.rotation);
				enemyBasic.shotInterval = 0;
			}*/			
		}
		/*if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			/*transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemySpeed);
			transform.position += transform.forward * Time.deltaTime * 1;
			if (enemyBasic.timeElapsed >= enemyBasic.timeOut) {
				transform.position += transform.up * Time.deltaTime * enemyBasic.JumpForce;
				enemyBasic.timeElapsed = 0.0f;
			}

			//animator.SetBool ("attack", true);
			//Debug.Log ("hit");
		}*/
		// Animator の dead が true なら Update 処理を抜ける
		//if( animator.GetBool("dead") == true ) return;
	}

}

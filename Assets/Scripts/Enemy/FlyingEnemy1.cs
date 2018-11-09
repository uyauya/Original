using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor; 

//中心部の周りをグルグル飛び回る敵
public class FlyingEnemy1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;
	bool dead = false;				 // 死亡判定
	public Vector3 BasicPoint;		 // 出現時の座標（地上からの高さを決める）
	public  float angle = 30f;		 // 回転するための角度
	public GameObject target;		 // 回転するための中心部（軸）
	private Vector3 targetPos;		 // 軸の場所
	public float Hight;				 // 出現位置の高さ	
	public float Distance;			 // 中心部からの距離
	bool damageSet;					 //被ダメージ処理、一時的に移動不可(下記参照)
	public float DamageTime = 0.5f;	 //ダメージ処理(硬直)時間
	bool freezeSet;					 //フリーズ処理、一時的に移動不可
	public float FreezeTime = 1.0f;	 //フリーズ処理(硬直)時間
	public float LastEnemySpeed;	 //ダメージ、フリーズ処理する前の敵の基本スピード

	void Start () {	
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		//出現の高さ調整。Hight値の高さとする
		BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + Hight, this.transform.position.z);
	}

	void Update () {
		damageSet = GetComponent<EnemyBasic> ().DamageSet;
		freezeSet = GetComponent<EnemyBasic> ().FreezeSet;
		Transform target = GameObject.FindWithTag("Enemy").transform;
		//オブジェクト配置場所の前方×2の場所をターゲット（軸）とする
		targetPos = target.position;
		transform.position = target.transform.position + (target.transform.forward * Distance);
		transform.position += transform.forward;
		//ターゲットを中心に（回る中心の座標、軸、速度）で回す
		transform.RotateAround (target.transform.position + (target.transform.forward * 2), Vector3.up, 1);
		//軸と回すキャラクタの高低差の設定
		this.transform.position = new Vector3 (this.transform.position.x, this.BasicPoint.y +1, this.transform.position.z);
		enemyBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			// enemySpeed × 時間でプレイヤに向かって直線的に移動
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;

			Vector3 axis = transform.TransformDirection(Vector3.up);
			transform.RotateAround(targetPos, axis ,angle * Time.deltaTime);
		}
		
		// ターゲット（プレイヤー）との距離がenemyBasic.Search以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
			//一定間隔でショット
			enemyBasic.shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
				Instantiate (enemyBasic.shot, transform.position, transform.rotation);
				enemyBasic.shotInterval = 0;
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
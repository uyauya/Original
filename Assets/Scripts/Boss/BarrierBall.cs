using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierBall : MonoBehaviour {

	protected EnemyBasic enemyBasic;
	bool dead = false;
	public Vector3 BasicPoint;		// 出現時の座標（地上からの高さを決める）
	public  float angle = 30f;
	private Vector3 targetPos;		// 軸の場所
	public GameObject target;		// 回転するための中心部（軸）
	public float Hight;

	void Start () {	
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + Hight, this.transform.position.z);
	}

	void Update () {
		target = GameObject.FindWithTag("Enemy");
		transform.RotateAround (target.transform.position, Vector3.up, 1);
		this.transform.position = new Vector3 (this.transform.position.x, this.BasicPoint.y +1, this.transform.position.z);

		enemyBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
			Vector3 axis = transform.TransformDirection(Vector3.up);
			transform.RotateAround(targetPos, axis ,angle * Time.deltaTime);
		}

		// ターゲット（プレイヤー）との距離が1以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 1) {
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
	}
}

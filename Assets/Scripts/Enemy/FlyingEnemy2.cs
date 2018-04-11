using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//空中の一定範囲を漂い、プレイヤが一定範囲に近づいたら攻撃。離れたら攻撃をやめて元の場所に戻る。
//遠距離攻撃あり。
public class FlyingEnemy2 : MonoBehaviour {
	protected EnemyBasic enemyBasic;
	bool dead = false;
	public Vector3 BasicPoint;		// 出現時の座標（地上からの高さを決める）
	//public  float angle = 30f;
	private Vector3 targetPos;		// 軸の場所
	public GameObject target;		// 回転するための中心部（軸）

	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z);
		Transform target = GameObject.FindWithTag("Player").transform;
		targetPos = target.position;
	}

	void Update () {
		transform.position = target.transform.position + (target.transform.forward * 2);
		//オブジェクト配置場所の前方×2の場所をターゲット（軸）とする
		transform.position += transform.forward;
		//ターゲットを中心に（回る中心の座標、軸、速度）で回す
		transform.RotateAround (target.transform.position + (target.transform.forward * 2), Vector3.up, 1);
		//軸と回すキャラクタの高低差の設定
		this.transform.position = new Vector3 (this.transform.position.x, this.BasicPoint.y +1, this.transform.position.z);
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
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			//Debug.Log ("検出");
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
			//一定間隔でショット
			//animator.SetBool ("attack", true);
			enemyBasic.shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
				Instantiate (enemyBasic.shot, transform.position, transform.rotation);
				enemyBasic.shotInterval = 0;
			}		
			//Animator の dead が true なら Update 処理を抜ける
			//if( animator.GetBool("dead") == true ) return;
		}
	}

}


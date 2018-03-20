using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss03 : MonoBehaviour {

	public GameObject Boss02shot;				// 弾
	public float ShotInterval;					// ショット間隔
	public GameObject exprosion;	
	int enemyLevel = 0;
	Bullet01 b1;
	public GameObject Boss02muzzle;				// ショットの発射口
	public int TargetPosition;
	public float TargetSpeed;
	public float MoveSpeed;						
	protected EnemyBasic enemyBasic;
	bool dead = false;


	void Start () {
		// EnemyBasic接続用
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		//enemyBasic.Initialize ();
		//if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) > TargetPosition) {
		//	return;
		//}
	}


	void Update () {
		if( enemyBasic.armorPoint <= 0f)
		{
			return;	// 敵がすでにやられている場合は何もしない
		}
		// Animator の dead が true なら Update 処理を抜ける
		if( enemyBasic.animator.GetBool("dead") == true ) return;
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.0f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		enemyBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= TargetPosition) {

			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
			transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
		}
		//一定間隔でショット
		enemyBasic.shotInterval += Time.deltaTime;

		if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
			enemyBasic.animator.SetTrigger ("attack");
			GameObject bossshot = GameObject.Instantiate (Boss02shot, Boss02muzzle.transform.position,Quaternion.identity)as GameObject;
			enemyBasic.shotInterval = ShotInterval;
		}

	}
}

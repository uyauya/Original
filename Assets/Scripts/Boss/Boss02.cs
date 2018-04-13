using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss02 : MonoBehaviour {

	public GameObject Boss02shot;				// 弾
	public float ShotInterval;					// ショット間隔
	public GameObject exprosion;	
	int enemyLevel = 0;
	Bullet01 b1;
	public GameObject Boss02muzzle;				// ショットの発射口
	public int TargetPosition;
	public float TargetSpeed;
	public float MoveSpeed;						
	protected BossBasic bossBasic;
	bool dead = false;
	public GameObject BossLifeBar;


	void Start () {
		// EnemyBasic接続用
		bossBasic = gameObject.GetComponent<BossBasic> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
		//enemyBasic.Initialize ();
		//if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) > TargetPosition) {
		//	return;
		//}
	}


	void Update () {
		if( bossBasic.armorPoint <= 0f)
		{
			return;	// 敵がすでにやられている場合は何もしない
		}
		// Animator の dead が true なら Update 処理を抜ける
		if( bossBasic.animator.GetBool("dead") == true ) return;
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.0f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		bossBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (bossBasic.target.transform.position, transform.position) <= TargetPosition) {
			
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(bossBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
			transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
		}
		//一定間隔でショット
		bossBasic.shotInterval += Time.deltaTime;

		if (bossBasic.shotInterval > bossBasic.shotIntervalMax) {
			bossBasic.animator.SetTrigger ("attack");
			GameObject bossshot = GameObject.Instantiate (Boss02shot, Boss02muzzle.transform.position,Quaternion.identity)as GameObject;
			bossBasic.shotInterval = ShotInterval;
		}

	}
}

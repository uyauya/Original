using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss03 : MonoBehaviour {

	public GameObject Boss03shot;				// 弾
	public float ShotInterval;					// ショット間隔
	public GameObject exprosion;	
	int enemyLevel = 0;
	public GameObject Boss03muzzle;				// ショットの発射口
	public int TargetPosition;					// ターゲットの場所検知
	public float TargetSpeed;					// 振り向きの速度
	public float MoveSpeed;						// 進む速度
	protected BossBasic bossBasic;				// BossBasic接続用
	bool dead = false;
	public GameObject BossLifeBar;


	void Start () {
		// BossBasic接続用
		bossBasic = gameObject.GetComponent<BossBasic> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
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
		//ターゲット（プレイヤ）の場所とボスの場所の距離がTargetPosition数値以内なら
		if (Vector3.Distance (bossBasic.target.transform.position, transform.position) <= TargetPosition) {
			//ターゲットの方をTargetSpeedでに向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(bossBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
			//ターゲットに向かってMoveSpeed数値で進む
			transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
		}
		// ショット間隔計算
		bossBasic.shotInterval += Time.deltaTime;
		// shotIntervalMax数値以上になったらショット
		if (bossBasic.shotInterval > bossBasic.shotIntervalMax) {
			bossBasic.animator.SetTrigger ("attack");
			GameObject bossshot = GameObject.Instantiate (Boss03shot, Boss03muzzle.transform.position,Quaternion.identity)as GameObject;
			// 再びショット間隔計算開始
			bossBasic.shotInterval = ShotInterval;
		}

	}
}


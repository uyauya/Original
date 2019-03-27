using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Boss02 : MonoBehaviour {

	public GameObject Boss02shot;				// 弾
	public GameObject Boss02shot2;				// 弾
	public float ShotInterval01;				// ショット間隔
	public float ShotInterval02;				// ショット間隔
	public GameObject exprosion;	
	int enemyLevel = 0;
	//public GameObject Boss02muzzle;			// ショットの発射口
    public Transform Boss02muzzle;              // 弾発射元（銃口）
    public int TargetPosition;					// ターゲットの場所検知
	public float TargetSpeed;					// 振り向きの速度
	public float MoveSpeed;						// 進む速度
	protected BossBasic bossBasic;				// BossBasic接続用
	bool dead = false;
	public static GameObject BossLifeBar;


	void Start () {
		// BossBasic接続用
		bossBasic = gameObject.GetComponent<BossBasic> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
	}


	void Update () {
		if( bossBasic.armorPoint <= 0f)
		{
			BossLifeBar.SetActive(false);
			return;	// 敵がすでにやられている場合は何もしない
		}
		// Animator の dead が true なら Update 処理を抜ける
		if( bossBasic.animator.GetBool("dead") == true ) return;
		Vector3 Pog = this.gameObject.transform.position;
		//地上に固定
		gameObject.transform.position = new Vector3(Pog.x , 0.0f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		//縦に傾かないよう固定
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
		bossBasic.shotInterval01 += Time.deltaTime;
		// shotIntervalMax数値以上になったらショット
		if (bossBasic.shotInterval01 > bossBasic.shotInterval01Max) {
			bossBasic.animator.SetTrigger ("attack");
			//GameObject boss02shot = GameObject.Instantiate (Boss02shot, Boss02muzzle.transform.position,Quaternion.identity)as GameObject;
            GameObject boss02shot = GameObject.Instantiate(Boss02shot) as GameObject;
            boss02shot.transform.position = Boss02muzzle.position;
            // 再びショット間隔計算開始
            bossBasic.shotInterval01 = ShotInterval01;
		}

		if(BossShot02Range.isHitDesision == true) {
			bossBasic.shotInterval02 += Time.deltaTime;
			// shotIntervalMax数値以上になったらショット
			if (bossBasic.shotInterval02 > bossBasic.shotInterval02Max) {
				bossBasic.animator.SetTrigger ("attack");
                //GameObject boss02shot2 = GameObject.Instantiate (Boss02shot2, Boss02muzzle.transform.position,Quaternion.identity)as GameObject;
                GameObject boss02shot2 = GameObject.Instantiate(Boss02shot2) as GameObject;
                boss02shot2.transform.position = Boss02muzzle.position;
                // 再びショット間隔計算開始
                bossBasic.shotInterval02 = ShotInterval02;
			}
		}

		if(BossShot02Range.isHitDesision == false) {
			return;
		}
	}


}

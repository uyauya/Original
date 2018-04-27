using UnityEngine;
using System.Collections.Generic;
using System.Collections;

// 弾幕系ボス（HPが減るとショットパターン変化）
public class Boss05 : MonoBehaviour {

	public GameObject exprosion;	
	public GameObject Boss03muzzle;	// ショットの発射口
	public int TargetPosition;		// ターゲットの場所検知
	public float TargetSpeed;		// 振り向きの速度
	public float MoveSpeed;			// 進む速度
	protected BossBasic bossBasic;	// BossBasic接続用
	bool dead = false;
	public GameObject BossLifeBar;
	public GameObject FirstForm;	// ショット第1パターン			
	public GameObject SecondForm;	// ショット第2パターン	
	public GameObject ThirdForm;	// ショット第3パターン	
	public GameObject FourthForm;	// ショット第4パターン	
	public GameObject FifthForm;	// ショット第5パターン	


	void Start () {
		// BossBasic接続用
		bossBasic = gameObject.GetComponent<BossBasic> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
	}


	void Update () {
		float PercentageAp = (float)bossBasic.armorPoint / bossBasic.armorPointMax;
		// ボスHPが80％以上ある場合は
		if (PercentageAp >= 0.8f) 
		{
			ShotForm1();
		}
		else if (PercentageAp >= 0.6) 
		{
			ShotForm2();
		}
		else if (PercentageAp >= 0.4) 
		{
			ShotForm3();
		}
		else if (PercentageAp >= 0.2) 
		{
			ShotForm4();
		}
		else
		{
			ShotForm5();
		}

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
	}

	void ShotForm1()
	{
		//ショット第1パターン作動
		FirstForm.SetActive (true);
	}
	void ShotForm2()
	{
		//ショットパターン切替（1を不可、2を可）
		FirstForm.SetActive (false);
		SecondForm.SetActive (true);
	}
	void ShotForm3()
	{
		SecondForm.SetActive (false);
		ThirdForm.SetActive (true);
	}
	void ShotForm4()
	{
		ThirdForm.SetActive (false);
		FourthForm.SetActive (true);
	}
	void ShotForm5()
	{
		FourthForm.SetActive (false);
		FifthForm.SetActive (true);
	}
}



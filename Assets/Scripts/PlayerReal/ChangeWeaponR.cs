using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 武器チェンジ
// 選択した武器を使用可にして、他を不可にする仕様
public class ChangeWeaponR : MonoBehaviour {

	private int type = 0;			//武器識別用No.
	private int num = 5;			//武器の種類数
	PlayerShootR   pshoot1;
	PlayerShoot02R　pshoot2;
	PlayerShoot03R  pshoot3;
	PlayerShoot04R  pshoot4;
	MultiWayShootR mshoot;
	public RawImage weponImage1;
	public RawImage weponImage2;
	public RawImage weponImage3;
	public RawImage weponImage4;
	public RawImage weponImage5;
	public Color myWhite = new Color(1, 1, 1, 1);
	public Color myBlue = new Color(0.1f, 0.03f, 1, 1);
	public Color myYellow = new Color (0.81f,0.99f,0,1);
	public Color myGreen  = new Color (0.48f,0.97f,0.08f,1);
	public Color myRed	  = new Color (1,0.16f,0.16f,1);
	public BattleManager battleManager;


	// Use this for initialization
	void Start () {
		weponImage1 = GameObject.Find ("Weapon1").GetComponent<RawImage> ();
		weponImage2 = GameObject.Find ("Weapon2").GetComponent<RawImage> ();
		weponImage3 = GameObject.Find ("Weapon3").GetComponent<RawImage> ();
		weponImage4 = GameObject.Find ("Weapon4").GetComponent<RawImage> ();
		weponImage5 = GameObject.Find ("Weapon5").GetComponent<RawImage> ();
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		pshoot1 = battleManager.Player.GetComponent<PlayerShootR>();
		pshoot2 = battleManager.Player.GetComponent<PlayerShoot02R>();
		pshoot3 = battleManager.Player.GetComponent<PlayerShoot03R>();
		pshoot4 = battleManager.Player.GetComponent<PlayerShoot04R>();
		mshoot = battleManager.Player.GetComponent<MultiWayShootR>();

		pshoot1.enabled = true;
		pshoot2.enabled = false;
		pshoot3.enabled = false;
		pshoot4.enabled = false;
		mshoot.enabled = false;
		weponImage2.enabled = false;
		weponImage3.enabled = false;
		weponImage4.enabled = false;
		weponImage5.enabled = false;

	}
		
	void Update () 
	{
		if (Input.GetButtonUp ("Fire2")) { 
			changeWeapon (); 	
			SoundManager2.Instance.Play(5,gameObject);}
		if (DataManager.Level >= PlayerLevel.PSoot02Level) {
			weponImage2.enabled = true;
		}
		if (DataManager.Level >= PlayerLevel.PSoot03Level) {
			weponImage3.enabled = true;
		}
		if (DataManager.Level >= PlayerLevel.PSoot03Level) {
			weponImage4.enabled = true;
		}
		if (DataManager.Level >= PlayerLevel.PSoot03Level) {
			weponImage5.enabled = true;
		}
	}


	private void changeWeapon ()
	{
		pshoot1 = battleManager.Player.GetComponent<PlayerShootR>();
		pshoot2 = battleManager.Player.GetComponent<PlayerShoot02R>();
		pshoot3 = battleManager.Player.GetComponent<PlayerShoot03R>();
		pshoot4 = battleManager.Player.GetComponent<PlayerShoot04R>();
		mshoot = battleManager.Player.GetComponent<MultiWayShootR>();
		type = (type + 1) % num;
		if (type == 0) 
		{
			weponImage1.color = myWhite;
			weponImage2.color = Color.white;
			weponImage3.color = Color.white;
			weponImage4.color = Color.white;
			weponImage5.color = Color.white;

			pshoot1.enabled = true;
			pshoot2.enabled = false;
			pshoot3.enabled = false;
			pshoot4.enabled = false;
			mshoot.enabled = false;
		}
		if (type == 1) 
		{
			weponImage1.color = Color.white;
			weponImage2.color = myBlue;
			weponImage3.color = Color.white;
			weponImage4.color = Color.white;
			weponImage5.color = Color.white;
			pshoot1.enabled = false;
			pshoot2.enabled = true;
			pshoot3.enabled = false;
			pshoot4.enabled = false;
			mshoot.enabled = false;
		}
		if (type == 2) 
		{
			weponImage1.color = Color.white;
			weponImage2.color = Color.white;
			weponImage3.color = myYellow;
			weponImage4.color = Color.white;
			weponImage5.color = Color.white;
			pshoot1.enabled = false;
			pshoot2.enabled = false;
			pshoot3.enabled = true;
			pshoot4.enabled = false;
			mshoot.enabled = false;
		}
		if (type == 3) 
		{
			weponImage1.color = Color.white;
			weponImage2.color = Color.white;
			weponImage3.color = Color.white;
			weponImage4.color = myGreen;
			weponImage5.color = Color.white;
			pshoot1.enabled = false;
			pshoot2.enabled = false;
			pshoot3.enabled = false;
			pshoot4.enabled = true;
			mshoot.enabled = false;
		}
		if (type == 4) 
		{
			weponImage1.color = Color.white;
			weponImage2.color = Color.white;
			weponImage3.color = Color.white;
			weponImage4.color = Color.white;
			weponImage5.color = myRed;
			pshoot1.enabled = false;
			pshoot2.enabled = false;
			pshoot3.enabled = false;
			pshoot4.enabled = false;
			mshoot.enabled = true;
		}
	}
}


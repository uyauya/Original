using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 武器チェンジ
// 選択した武器を使用可にして、他を不可にする仕様
public class ChangeWeapon : MonoBehaviour {
	
	private int type = 0;			//武器識別用No.
	private int num = 5;			//武器の種類数
	PlayerShoot   pshoot1;
	PlayerShoot02　pshoot2;
	PlayerShoot03  pshoot3;
	PlayerShoot04  pshoot4;
	MultiWayShoot mshoot;
	public RawImage weponImage1;
	public RawImage weponImage2;
	public RawImage weponImage3;
	public RawImage weponImage4;
	public RawImage weponImage5;
	public Color myWhite;			//RGBA(255,255,255,255)
	public Color myBlue;			//RGBA(026,008,255,255)
	public Color myYellow;			//RGBA(207,253,000,255)
	public Color myGreen;			//RGBA(123,248,022,255)
	public Color myRed;				//RGBA(255,041,041,255)

	// Use this for initialization
	void Start () {
		// Weapon1という名前のオブジェクトのRawImageを（このスクリプト内では）weponImage1と呼ぶことにする
		weponImage1 = GameObject.Find ("Weapon1").GetComponent<RawImage> ();
		weponImage2 = GameObject.Find ("Weapon2").GetComponent<RawImage> ();
		weponImage3 = GameObject.Find ("Weapon3").GetComponent<RawImage> ();
		weponImage4 = GameObject.Find ("Weapon4").GetComponent<RawImage> ();
		weponImage5 = GameObject.Find ("Weapon5").GetComponent<RawImage> ();
		// Playerタグが付いているオブジェクトのPlayerShootスクリプトを（このスクリプト内では）pshoot1と呼ぶことにする
		pshoot1 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot> ();
		pshoot2 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot02> ();
		pshoot3 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot03> ();
		pshoot4 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot04> ();
		mshoot  = GameObject.FindWithTag("Player").GetComponent<MultiWayShoot> ();
		// 最初に使用できるのはpshoot1にする。
		pshoot1.enabled = true;
		pshoot2.enabled = false;
		pshoot3.enabled = false;
		pshoot4.enabled = false;
		mshoot.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Fire2キーで下記changeWeaponを起動。その際SoundManager2の「5」に入れた音を鳴らす。
		// Fire2キーはEdit→ProjectSetting→Inputでキー設定変更可能。
		if (Input.GetButtonUp ("Fire2")) { changeWeapon (); 	//武器交換
			SoundManager2.Instance.Play(5,gameObject);}
	}


	private void changeWeapon ()
	{
		//「値+1」を武器個数(num)で割り、余りをtypeに入れて選択武器とする
		type = (type + 1) % num;
		// 選択された武器には色を付けて他は白に。
		// 選択された武器は使用可（該当スクリプトをtrueにする)にして他は不可に。
		if (type == 0) {
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
		if (type == 1) {
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
		if (type == 2) {
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
		if (type == 3) {
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
		if (type == 4) {
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

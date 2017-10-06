using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeWeapon : MonoBehaviour {


	private int type = 0;		//武器識別用No.
	private int num = 5;		//武器の種類数
	PlayerShoot   pshoot1;
	PlayerShoot02　pshoot2;
	PlayerShoot03  pshoot3;
	PlayerShoot04  pshoot4;
	MultiWayShoot mshoot;

	// Use this for initialization
	void Start () {
		pshoot1 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot> ();
		pshoot2 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot02> ();
		pshoot3 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot03> ();
		pshoot4 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot04> ();
		mshoot  = GameObject.FindWithTag("Player").GetComponent<MultiWayShoot> ();
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonUp ("Fire1")) { useWeapon (); }		//武器使用
		if (Input.GetButtonUp ("Fire2")) { changeWeapon (); }	//武器交換
	}

	//「値+1」を武器個数で割り、余りをtypeに入れて選択武器とする
	public void changeWeapon ()
	{
		type = (type + 1) % num;
	}
		
	private void useWeapon ()
	{
		if (type == 0)
		{
			return pshoot1;
		}
		if (type == 1)
		{
			return pshoot2;
		}
		if (type == 2)
		{
			return pshoot3;
		}
		if (type == 3)
		{
			return pshoot4;
		}
		if (type == 4)
		{
			return mshoot;
		}
}

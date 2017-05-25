﻿using UnityEngine;
using System.Collections;


public class Guard : MonoBehaviour
{
	public GameObject guardText; 

	private Vector3 velocity = Vector3.zero;
	private Vector3 input = Vector3.zero;

	public bool guard = false;          //　ガードしているか
	public bool push = false;           //　最初に移動ボタンを押したかどうか
	public float nextButtonDownTime;    //　次に移動ボタンが押されるまでの時間
	private float nowTime = 0f;         //　最初に移動ボタンが押されてからの経過時間

	public float limitAngle;            //　最初に押した方向との違いの限度角度
	private Vector2 direction = Vector2.zero;           //　移動キーの押した方向


	void Start()
	{

	}


	void Update()
	{
		velocity = Vector3.zero;

		//　ガードしていないとき
		if (!guard)
		{
			//　移動キーを押した
			if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")))
			{
				//　最初に1回押していない時は押した事にする
				if (!push)
				{
					push = true;
					//　最初に移動キーを押した時にその方向ベクトルを取得
					direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
					nowTime = 0f;
					//　2回目のボタンだったら1→２までの制限時間内だったらガード
				}
				else
				{
					//　2回目に移動キーを押した時の方向ベクトルを取得
					var nowDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

					//　押した方向がリミットの角度を越えていない　かつ　制限時間内に移動キーが押されていればガード
					if (Vector2.Angle(nowDirection, direction) < limitAngle
					&& nowTime <= nextButtonDownTime)
					{
						guard = true;
					}
				}
			}
			//　ガード時にキーを押すのをやめたらガードをやめる
		}
		else
		{
			if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical"))
			{
				guard = false;
				push = false;
			}
		}
		//　最初の移動キーを押していれば時間計測
		if (push)
		{
			//　時間計測
			nowTime += Time.deltaTime;

			if (nowTime > nextButtonDownTime)
			{
				push = false;
			}
		}

		input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		if (input.magnitude >= 0.1f)
		{
			if (guard)
			{
				guardText.SetActive(true);
			}
			else
			{
				guardText.SetActive(false);
			}
		}
	}
}
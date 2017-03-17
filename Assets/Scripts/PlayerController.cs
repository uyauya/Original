﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

	private Animator animator;
	// 移動時に加える力
	public float force = 30.0f;
	//public float Speed = 3.0F;
	public float jumpSpeed = 25.0F;
	public float gravity = 9.8F;
	private Vector3 moveDirection = Vector3.zero;
	int boostPoint;
	public int boostPointMax = 1000;
	public Image gaugeImage;
	Vector3 moveSpeed;
	//ブースト時の最大速度
	private int JumpCount;
	bool isBoost;

	bool onFloor = true;

	Vector3 targetSpeed = Vector3.zero;      //目標速度
	Vector3 addSpeed = Vector3.zero;        //加算速度

	void Start()
	{
		animator = GetComponent<Animator>();
		boostPoint = boostPointMax;
		moveSpeed = Vector3.zero;
		isBoost = false;
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
	}


	void Update()
	{

		//ブーストボタンが押されていればフラグを立てブーストポイントを消費
		if (Input.GetButton("Boost") && boostPoint > 10)
		{
			boostPoint -= 10;
			isBoost = true;
		}
		else
		{
			isBoost = false;
		}

		//通常時とブースト時で変化
		if (isBoost)
		{
			// ブースト時
			if (force < 45.0f) {
				force += Time.deltaTime;
			}
			//ブーストキーが押されたらにパラメータを切り替える
			animator.SetBool("Boost", Input.GetButton("Boost"));
		}
		else
		{
			force = 30.0f;
			animator.SetBool("Boost", Input.GetButton("Boost"));
		}



		//モーションを切り替える
		if (Input.GetAxis("Horizontal") > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

		}
		else if (Input.GetAxis("Vertical") > 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

		}
		else if (Input.GetAxis("Vertical") < 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

		}
		else
		{
			animator.SetBool("Move", false);
		}

		//ジャンプキーによる上昇
		if (Input.GetButtonDown("Jump") == true && JumpCount < 2 ) {
			JumpCount++;
			// ジャンプの上昇力を設定
			Vector3 v = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity = new Vector3( v.x, 4, v.z );
	
			//ジャンプモーションに切り替える
			animator.SetBool("Jump", true);

		} else if (Input.GetButton ("Jump") && (Input.GetButton ("Boost") && boostPoint > 20)) {
			// ジャンプの最大値より上に上昇しない（一定以上なら上昇ゼロ）
			animator.SetBool("BoostUp", Input.GetButton ("Jump"));
			if (transform.position.y > 120)
				moveDirection.y = 0;
			// ジャンプの最大値までは上昇
			moveDirection.y += gravity * Time.deltaTime;
			boostPoint -= 10;
			//ジャンプモーションに切り替える
			animator.SetBool("BoostUp", Input.GetButton("Jump"));
		} else {
			// それ以外の場合は重力にそって落下
			if( onFloor == false ) {
				moveDirection.y -= 0.05f * Time.deltaTime;
				if( moveDirection.y <= -1 ) moveDirection.y = -1;
			}
		}
		// ブーストやジャンプが入力されていなければブースとポイントが徐々に回復
		if (!Input.GetButton ("Boost"))
			boostPoint += 5;
		// ブーストポイントが最大以上にはならない
		boostPoint = Mathf.Clamp (boostPoint, 0, boostPointMax);

		//移動速度に合わせてモーションブラーの値を変える（MainCameraにCameraMotionBlurスクリプトを追加)
		//MainCameraのInspectorのCameraMotionBlurのExcludeLayersでプレイヤーと敵を選択して
		//プレイヤーと的にはモーションブラーがかからないようにする
		//float motionBlurValue = Mathf.Max (Mathf.Abs (moveSpeed.x), Mathf.Abs (moveSpeed.z)) / 20;
		//motionBlurValue = Mathf.Clamp (motionBlurValue, 0, 5);
		//Camera.main.GetComponent<CameraMotionBlur> ().velocityScale = motionBlurValue;

		// 座標を移動させる
		//Debug.LogFormat("MoveDir: {0}", moveDirection );
		//this.transform.position = this.transform.position + moveDirection;
		Debug.Log((float)boostPoint / boostPointMax);
		//ブーストゲージの伸縮
		// ゲージの最大以上には上がらない
		gaugeImage.transform.localScale = new Vector3 ((float)boostPoint / boostPointMax, 1, 1);
		//gaugeImage.transform.localScale = new Vector3(0.5f,1,1);

	}

	// アイテム２タグの物に接触したらブーストポイント回復
	private void OnCollisionEnter (Collision collider)
	{
		if (collider.gameObject.tag == "Item2") {
			animator.SetTrigger ("ItemGet");
			boostPoint += 500;
			// ブーストポイントが最大以上にはならない
			boostPoint = Mathf.Clamp (boostPoint, 0, boostPointMax);
		}
		if( collider.gameObject.tag == "Floor" ) {
			Debug.Log("Floor");
			JumpCount = 0;
			moveDirection.y = 0;
			Vector3 v = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity = new Vector3( v.x, 0, v.z );
			onFloor = true;
			animator.SetBool("Jump", false);
		}
	}

	private void OnCollisionStay(Collision collisionInfo) {
	}
}

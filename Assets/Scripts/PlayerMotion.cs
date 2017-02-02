using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;

public class PlayerMotion : MonoBehaviour {
		[SerializeField]
		private Animator _animator;
		[SerializeField]
		private CharacterController _controller;
		[SerializeField,Range (0, 1)]
		private float _moveSpeed;

		// Use this for initialization
		void Start ()
		{

		}

		// Update is called once per frame
		void Update ()
		{
			//===キャラクターのアニメーション部分===//
			if (Input.GetAxis ("Horizontal") > 0) {
				_animator.SetInteger ("Horizontal", 1);
			} else if (Input.GetAxis ("Horizontal") < 0) {	
				_animator.SetInteger ("Horizontal", -1);
			} else {
				_animator.SetInteger ("Horizontal", 0);
			}
			if (Input.GetAxis ("Vertical") > 0) {
				_animator.SetInteger ("Vertical", 1);
			} else if (Input.GetAxis ("Vertical") < 0) {
				_animator.SetInteger ("Vertical", -1);
			} else {
				_animator.SetInteger ("Vertical", 0);
			}
			//=======================//

			//ジャンプモーションに切り替える
			_animator.SetBool ("Jump", Input.GetButton ("Jump"));

			//ブーストキーが押されたらにパラメータを切り替える
			_animator.SetBool ("Boost", Input.GetButton ("Boost"));

			//===キャラクターの回転部分===//
			//目標の回転角(Y値)
			/*float targetRotateY = 0;
			if (Input.GetAxis ("Horizontal") != 0) {
				targetRotateY = Input.GetAxis ("Horizontal") * 90f;
			} else if (Input.GetAxis ("Vertical") < 0) {
				targetRotateY = 180f;
			}
			//プレイヤーを回転させる
			transform.Rotate (0, targetRotateY - transform.localEulerAngles.y, 0);

			//=======================//

			//===キャラクターの移動===//
			Vector3 moveVector = new Vector3 (
				Input.GetAxis ("Horizontal") * _moveSpeed,
				0,
				Input.GetAxis ("Vertical") * _moveSpeed);
			_controller.Move (moveVector);*/
			//=======================//
		}
	}

using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {

	/*private	CharacterController charaCon;		// キャラクターコンポーネント用の変数	
	private Vector3		move = Vector3.zero;	// キャラ移動量.	
	private float		speed = 5.0f;			// 移動速度	
	private float		jumpPower = 10.0f;		// 跳躍力.	
	private const float	GRAVITY = 9.8f;			// 重力	
	private float		rotationSpeed = 180.0f;	// プレイヤーの回転速度	

	void Start(){		charaCon = GetComponent< CharacterController >();	
	}	

	void Update () {		
		// 移動量の取得		
		float y = move.y;		
		move = new Vector3(Input.GetAxis("Horizontal") , 0.0f , Input.GetAxis("Vertical"));		// 左右上下のキー入力を取得し、移動量に代入.		
		Vector3 playerDir = move;	// 移動方向を取得.		
		move *= speed;				// 移動速度を乗算.		

		// 重力／ジャンプ処理	
		move.y += y;		
		if(charaCon.isGrounded){					// 地面に設置していたら			
			if(Input.GetKeyDown(KeyCode.Space)){	// ジャンプ処理.				
				move.y = jumpPower;			
			}		
		}		
		move.y -=  GRAVITY * Time.deltaTime;	// 重力を代入.		
		// プレイヤーの向き変更		
		if(playerDir.magnitude > 0.1f){			
			Quaternion q = Quaternion.LookRotation(playerDir);			// 向きたい方角をQuaternionn型に直す .			transform.rotation = Quaternion.RotateTowards(transform.rotation , q , rotationSpeed * Time.deltaTime);	// 向きを q に向けてじわ～っと変化させる.		
		}		
		// ▼▼▼移動処理▼▼▼		
		charaCon.Move(move * Time.deltaTime);	// プレイヤー移動.	
	}*/
}

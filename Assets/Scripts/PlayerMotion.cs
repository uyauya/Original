using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour {
	// Animatorをこれ以降animatorと略す
	private Animator animator;
	public Transform target;

	void Start () {
		//animatorを使えるようにする為にAnimatorをゲットコンポーネント
		animator = GetComponent<Animator> ();
	}

	void Update () {
	
		//モーションを切り替える
		// Horizontalはプラスが右でマイナスが左、Verticalはプラスが前でマイナスが後
		// 右に押したらプラス１以上になるので、右に設定したモーションに切り替わる
		if(Input.GetAxis ("Horizontal") > 0){
			target.transform.Rotate(0, Input.GetAxis("Horizontal") * 6, 0);
			animator.SetInteger("Horizontal",1);
		}else if(Input.GetAxis ("Horizontal") < 0){	
			target.transform.Rotate(0, Input.GetAxis("Horizontal") * 6, 0);
			animator.SetInteger("Horizontal",-1);
		}else{
			animator.SetInteger("Horizontal",0);
		}
		if(Input.GetAxis ("Vertical") > 0){
			animator.SetInteger("Vertical",1);
		}else if(Input.GetAxis ("Vertical") < 0){
			animator.SetInteger("Vertical",-1);
		}else{
			animator.SetInteger("Vertical",0);
		}
		
		//ジャンプモーションに切り替える
		animator.SetBool("Jump", Input.GetButton ("Jump"));
		
		//ブーストキーが押されたらにパラメータを切り替える
		animator.SetBool("Boost",Input.GetButton ("Boost"));
	}
}

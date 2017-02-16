using System.Collections;
using UnityEngine;
using System.Collections;

public class PlayerMotion : MonoBehaviour {

	private Animator animator;

	// Use this for initialization
	void Start () {

		animator = GetComponent<Animator> ();
	}

	// Update is called once per frame
	void Update () {

		//モーションを切り替える
		if (Input.GetAxis ("Horizontal") > 0) {
			transform.rotation = Quaternion.Euler (0, 90, 0);
			animator.SetBool ("Move", true);
			gameObject.GetComponent<Rigidbody> ().AddForce (transform.forward * 30);
		} else if (Input.GetAxis ("Horizontal") < 0) {
			transform.rotation = Quaternion.Euler (0, -90, 0);
			animator.SetBool ("Move", true);
			gameObject.GetComponent<Rigidbody> ().AddForce (transform.forward * 30);

		} else if(Input.GetAxis ("Vertical") > 0){
			transform.rotation = Quaternion.Euler(0, 0, 0);
			animator.SetBool("Move",true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 30);

		}else if(Input.GetAxis ("Vertical") < 0){
			transform.rotation = Quaternion.Euler(0, -180, 0);
			animator.SetBool("Move",true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 30);

		}else{
			animator.SetBool("Move",false);
		}

		//ジャンプモーションに切り替える
		animator.SetBool("Jump", Input.GetButton ("Jump"));

		//ブーストキーが押されたらにパラメータを切り替える
		animator.SetBool("Boost",Input.GetButton ("Boost"));
	}
}

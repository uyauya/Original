using UnityEngine;
using System.Collections;

public class BlockForce : MonoBehaviour {

	Rigidbody rigidbody;
	
	void Start () {
		rigidbody = gameObject.GetComponent<Rigidbody>();
		// 空中で発生しても固定して落ちないようにする
		if (rigidbody) {
			rigidbody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
	
	public void Move(float time) {
		Invoke("_Move", time);
	}
	
	void _Move() {
		// rigidbodyがぶつかったら固定解除
		if (rigidbody) {
			rigidbody.constraints = RigidbodyConstraints.None;
			// 解除された時グラグラ揺らす
			rigidbody.AddTorque(new Vector3(
				2f*Random.value-1f, 2f*Random.value-1f, 2f*Random.value-1f));
			Invoke("TimeOut", 10f);
		}
	}
	
	void TimeOut() {
		DestroyImmediate(gameObject);
	}
} 

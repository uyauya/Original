using UnityEngine;
using System.Collections;

public class BlockForce : MonoBehaviour {

	Rigidbody freezebody;
	
	void Start () {
		freezebody = gameObject.GetComponent<Rigidbody>();
		// 空中で発生しても固定して落ちないようにする
		if (GetComponent<Rigidbody>()) {
			freezebody.constraints = RigidbodyConstraints.FreezeAll;
		}
	}
	
	public void Move(float time) {
		Invoke("_Move", time);
	}
	
	void _Move() {
		// rigidbodyがぶつかったら固定解除
		if (freezebody) {
			freezebody.constraints = RigidbodyConstraints.None;
			// 解除された時グラグラ揺らす
			freezebody.AddTorque(new Vector3(
				2f*Random.value-1f, 2f*Random.value-1f, 2f*Random.value-1f));
			Invoke("TimeOut", 10f);
		}
	}
	
	void TimeOut() {
		DestroyImmediate(gameObject);
	}
} 

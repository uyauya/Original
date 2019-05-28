using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボスを倒した後発生。上から回転、点滅しながらゆっくり落ちてくる
public class Star : MonoBehaviour {
	public float Interval = 1.0f;	// 点滅周期
	public Vector3 localGravity;	// 重力(x,y,z)
	private Rigidbody rb;
	public float Force = 3f;

	void Start () 
	{		
		rb = this.GetComponent<Rigidbody>();
		rb.useGravity = false;
		StartCoroutine ("Blink");
		rb.AddForce (transform.up * Force);
	}

	void FixedUpdate () 
	{
		transform.Rotate (new Vector3 (0, 5, 0));
		setLocalGravity ();
	}

	void setLocalGravity(){
		// 重力変更設定
		rb.AddForce (localGravity, ForceMode.Acceleration);
	}

	IEnumerator Blink()
	{
		// 点滅していなかったら点滅
		gameObject.GetComponent<MeshRenderer>().enabled = !gameObject.GetComponent <MeshRenderer>().enabled;
		// 次に点滅するまでの待ち時間
		yield return new WaitForSeconds (Interval);
	}

	// 衝突判定
	void OnCollisionEnter (Collision col)
	{
		//Playerタグの付いたオブジェクトと衝突したら消滅
		if (col.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// スフィア用
public class Sphere : MonoBehaviour {

	public float SphereHeight = 0.4f;	//出現時のスフィアの高さ

	void Start () {
		//その場でSphereHeightの高さで発生
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , SphereHeight, Pog.z);
	}

	// 衝突判定
	void OnCollisionEnter (Collision col)
	{
		//Playerタグの付いたオブジェクトと衝突したら消滅(取得して消える)
		if (col.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}

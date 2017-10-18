using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sphere : MonoBehaviour {

	// 衝突判定
	void OnCollisionEnter (Collision col)
	{
		//Playerタグの付いたオブジェクトと衝突したら消滅
		if (col.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}

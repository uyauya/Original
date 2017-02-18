using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenSphere : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		//Playerと衝突した時
		if (col.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}

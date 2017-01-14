using UnityEngine;
using System.Collections;

public class RedSphere : MonoBehaviour {

	void OnCollisionEnter (Collision col)
	{
		//Playerと衝突した時
		if (col.gameObject.tag == "Player") {
			Destroy(gameObject);
		}
	}
}

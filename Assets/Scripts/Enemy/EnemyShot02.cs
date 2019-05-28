using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot02 : MonoBehaviour {

	//public GameObject explosion;
	public float DestroyTime = 2.0f;

	// Use this for initialization
	void Start () {

		//現後一定時間で自動的に消滅させる
		Destroy(gameObject, DestroyTime);
	}

	// Update is called once per frame
	void Update () {

		//弾を前進させる
		transform.position += transform.forward * Time.deltaTime * 100;
	}

	private void OnCollisionEnter(Collision collider) {

		//プレイヤーと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player") {
			Destroy (gameObject);
			//Instantiate(explosion, transform.position, transform.rotation);
		}
	}
}

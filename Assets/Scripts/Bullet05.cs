using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet05 : MonoBehaviour {

	public GameObject explosion;
	public float damage;
	public float BulletSpeed;
	Enemy enemy;
	MultiWayShoot multiwayshoot;
	private Rigidbody rb;
	private Vector3 forward;
	public float DestroyTime = 3;

	void Start () {
		rb = this.GetComponent<Rigidbody>();
		multiwayshoot = GameObject.FindWithTag("Player").GetComponent<MultiWayShoot> ();
		transform.rotation = multiwayshoot.transform.rotation;
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}	
	private void OnCollisionEnter(Collision collider) {

		//地形とぶつかったら消滅させる
		if (collider.gameObject.tag == "Floor") {			
			Destroy (gameObject);
			// ぶつかった場所に爆発を設定
			//Instantiate (explosion, transform.position, transform.rotation);
		}	
		//敵と衝突したら消滅させる
		if (collider.gameObject.tag == "Enemy"||collider.gameObject.tag == "Wall") {
			//collider.gameObject.SendMessage ("damage");
			Destroy (gameObject);
		}
		//衝突時に爆発エフェクトを表示する
		//Instantiate(explosion, transform.position, transform.rotation);
	}
}


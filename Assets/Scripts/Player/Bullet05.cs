﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet05 : MonoBehaviour {

	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	Enemy enemy;
	MultiWayShoot multiwayshoot;		// 発射元
	private Rigidbody rb;
	private Vector3 forward;
	public float DestroyTime = 3;		// 発射されてから消滅するまでの時間

	void Start () {
		rb = this.GetComponent<Rigidbody>();
		multiwayshoot = GameObject.FindWithTag("Player").GetComponent<MultiWayShoot> ();
		//transform.rotation = multiwayshoot.transform.rotation;
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
		//transform.Translate(Vector3.forward * Time.deltaTime);
	}	

	private void OnCollisionEnter(Collision collider) {
		//衝突時に爆発エフェクトを表示する
		Instantiate(explosion, transform.position, transform.rotation);
		//地形とぶつかったら消滅させる
		/*if (collider.gameObject.tag == "Floor") {		
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}	
		//敵と衝突したら消滅させる
		if (collider.gameObject.tag == "Enemy"||collider.gameObject.tag == "Wall") {*/
		Destroy (gameObject);
	}
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantFire : MonoBehaviour
{
	//public GameObject explosion;
	public float DestroyTime = 2.0F;
	public float FireSpeed = 1.0F;
	private GiantEnemy giantEnemy;            // 発射元

	// Start is called before the first frame update
	void Start()
	{
		giantEnemy = GameObject.FindWithTag("Enemy").GetComponent<GiantEnemy>();
		//transform.rotation = driftEnemy1.transform.rotation;
		Destroy(gameObject, DestroyTime);
	}

	// Update is called once per frame
	void Update()
	{
		transform.position += transform.forward * Time.deltaTime * FireSpeed;
	}

	private void OnCollisionEnter(Collision collider)
	{

		//プレイヤータグの付いたオブジェクトと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player")
		{
			Destroy(gameObject);

		}
		else if (collider.gameObject.tag == "Shot")
		{
			Destroy(gameObject);
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Shot2 : MonoBehaviour {
	
	public GameObject explosion;
	public float DestroyTime = 2.0F;
	public float ShotSpeed = 1.0F;
	private Boss02 boss02shot02;			// 発射元

	// Use this for initialization
	void Start () {
		boss02shot02 = GameObject.FindWithTag("Enemy").GetComponent<Boss02> ();
		transform.rotation = boss02shot02.transform.rotation;
		Destroy(gameObject, DestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
			transform.position += transform.forward * Time.deltaTime * ShotSpeed;
	}

	private void OnCollisionEnter(Collision collider) {

		//プレイヤータグの付いたオブジェクトと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player") 
		{
			Destroy (gameObject);

		} else if (collider.gameObject.tag == "Shot") 
		{
			Destroy (gameObject);
		}
	}
}

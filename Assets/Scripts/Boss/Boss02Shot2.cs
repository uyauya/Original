using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss02Shot2 : MonoBehaviour {
	
	public GameObject explosion;
	public float DestroyTime = 2.0F;
	public float ShotSpeed = 1.0F;

	// Use this for initialization
	void Start () {
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

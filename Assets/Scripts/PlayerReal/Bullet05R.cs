using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bullet05R : MonoBehaviour {

	public GameObject explosion;		
	public float damage;				
	public float BulletSpeed;			
	public float DecreaseDamage = 0.5f;	
	public float LowestDamage = 20.0f;	
	Enemy enemy;
	MultiWayShootR multiwayshootR;		
	private Rigidbody rb;
	private Vector3 forward;
	public float DestroyTime = 3;		

	void Start () {
		rb = this.GetComponent<Rigidbody>();
		multiwayshootR = GameObject.FindWithTag("Player").GetComponent<MultiWayShootR> ();
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
		damage = damage - (DecreaseDamage * Time.deltaTime);
		if (damage <= LowestDamage)
			damage = LowestDamage;
	}	

	private void OnCollisionEnter(Collision collider) {
		Instantiate(explosion, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}


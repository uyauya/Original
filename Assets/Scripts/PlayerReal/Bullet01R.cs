using UnityEngine;
using System.Collections;
using UnityEditor;

public class Bullet01R : MonoBehaviour {

	public GameObject explosion;		
	public float damage;				
	public float BulletSpeed = 3.0f;	
	public float DecreaseDamage = 0.5f;	
	public float LowestDamage = 20.0f;	
	Enemy enemy;
	PlayerShootR PlshootR;			　
	public float DestroyTime = 3;	　　

	public void Initialize () {
		PlshootR = GameObject.FindWithTag("Player").GetComponent<PlayerShootR> ();
		transform.rotation = PlshootR.transform.rotation;
		Destroy (gameObject, DestroyTime);
	}

	void Update () {
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
		damage = damage - (DecreaseDamage * Time.deltaTime);
		if (damage <= LowestDamage)
			damage = LowestDamage;
	}	

	private void OnCollisionEnter(Collision collider) {
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Floor") {
			Instantiate (explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}


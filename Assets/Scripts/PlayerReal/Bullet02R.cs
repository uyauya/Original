
using UnityEngine;
using System.Collections;
using UnityEditor;

public class Bullet02R : MonoBehaviour {

	public GameObject explosion;		
	public float damage;			
	public float BulletSpeed;			
	public float DecreaseDamage = 0.9f;	
	public float LowestDamage = 20.0f;	
	PlayerShoot02R Plshoot02R;			
	private  GameObject Enemy;
	public float DestroyTime = 1;		

	void Start () 
	{
		Plshoot02R = GameObject.FindWithTag("Player").GetComponent<PlayerShoot02R> ();
		transform.rotation = Plshoot02R.transform.rotation;
		Destroy (gameObject, DestroyTime);

	}	

	void Update ()
	{
		if (Enemy == null) {
			GameObject[] allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			if (allEnemies != null && allEnemies.Length != 0) {
				Enemy = allEnemies [UnityEngine.Random.Range (0, allEnemies.Length)];
			} else {
				return;
			}
		}
		float speed = BulletSpeed;
		float step = Time.deltaTime * speed;
		transform.position = Vector3.MoveTowards (transform.position, Enemy.transform.position, step);
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



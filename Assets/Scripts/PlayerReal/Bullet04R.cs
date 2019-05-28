using UnityEngine;
using System.Collections;
using UnityEditor;

public class Bullet04R : MonoBehaviour {

	public GameObject explosion;		
	public float damage;				
	public float BulletSpeed;			
	Enemy enemy;
	PlayerShoot04R Plshoot04R;			
	public float DestroyTime = 9;		
	public float XPower;				
	public float YPower;
	public float ZPower;

	void Start () {
		Plshoot04R = GameObject.FindWithTag("Player").GetComponent<PlayerShoot04R> ();
		transform.rotation = Plshoot04R.transform.rotation;
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}
		
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(XPower, YPower, ZPower));
	}

}



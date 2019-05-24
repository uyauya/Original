
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttack : MonoBehaviour
{
	//public GameObject HitFlash;		
	public float damage;				
	public float WeaponSpeed = 5;			
	Enemy enemy;
	CloseCombat closeCombat;		
	private Rigidbody rb;
	private Vector3 forward;
	public float DestroyTime = 0.5f;		


	void Start () {
		rb = this.GetComponent<Rigidbody>();
		closeCombat = GameObject.FindWithTag("Player").GetComponent<CloseCombat> ();
		transform.rotation = closeCombat.transform.rotation;
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		transform.position += transform.forward * Time.deltaTime * WeaponSpeed;
		damage = damage;
	}	

	private void OnCollisionEnter(Collision collider) {
		//Instantiate(HitFlash, transform.position, transform.rotation);
		Destroy (gameObject);
	}
}

using UnityEngine;
using System.Collections;

public class Bullet04 : MonoBehaviour {

	public GameObject explosion;
	public float damage;
	Enemy enemy;
	PlayerShoot Plshoot;
	
	void Start () {
		Plshoot = GameObject.Find ("Utc_sum_humanoid").GetComponent<PlayerShoot> ();
		//現後一定時間で自動的に消滅させる
		Destroy (gameObject, 9);
	}	
	void Update () {		
		//弾を前進させる
		transform.position += transform.forward * Time.deltaTime * 0.1F;
	}	

}


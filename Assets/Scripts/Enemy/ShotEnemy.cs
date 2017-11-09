using UnityEngine;
using System.Collections;

public class ShotEnemy : MonoBehaviour {

	public GameObject explosion;
	
	// Use this for initialization
	void Start () {
	
		//現後一定時間で自動的に消滅させる
		Destroy(gameObject, 2.0F);
	}
	
	// Update is called once per frame
	void Update () {
	
		//弾を前進させる
		transform.position += transform.forward * Time.deltaTime * 100;
	}
	
	private void OnCollisionEnter(Collision collider) {

		//プレイヤーと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player") {
			
			Destroy (gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}
}

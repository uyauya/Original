using UnityEngine;
using System.Collections;

public class Bullet04 : MonoBehaviour {

	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	Enemy enemy;
	PlayerShoot04 Plshoot04;			// 発射元
	public float DestroyTime = 9;		// 発射されてから消滅するまでの時間

	void Start () {
		Plshoot04 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot04> ();
		transform.rotation = Plshoot04.transform.rotation;
		//現後一定時間で自動的に消滅させる
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		//弾を前進させる
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}	

}


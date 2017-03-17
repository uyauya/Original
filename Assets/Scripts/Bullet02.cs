using UnityEngine;
using System.Collections;

public class Bullet02 : MonoBehaviour {
	
	public GameObject explosion;
	public float damage;
	PlayerShoot02 Plshoot02;
	private  GameObject Enemy;

	void Start () {
		Plshoot02 = GameObject.Find ("Utc_sum_humanoid").GetComponent<PlayerShoot02> ();
		//現後一定時間で自動的に消滅させる
		Destroy (gameObject, 3);
	}	

	void Update ()

	{

		//ホーミング対象がnull(何もない)であれば

		if (Enemy == null) {

			//Enemyというタグがつけられたゲームオブジェクトを配列で取得

			GameObject[] allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");

			//allEnemiesがnullじゃない かつ 要素数が0でなければ

			if (allEnemies != null && allEnemies.Length != 0) {



				Enemy = allEnemies [UnityEngine.Random.Range (0, allEnemies.Length)];

			} else {

				//何もしない

				return;

			}

		}

		float speed = 6.0f;

		float step = Time.deltaTime * speed;

		transform.position = Vector3.MoveTowards (transform.position, Enemy.transform.position, step);

	}

	private void OnCollisionEnter(Collision collider) {

		//地形とぶつかったら消滅させる
		if (collider.gameObject.tag == "Floor") {	
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}	
		//敵と衝突したら消滅させる
		if (collider.gameObject.tag == "Enemy") {
			//collider.gameObject.SendMessage ("damage");
			Destroy (gameObject);
			//	Debug.Log ("当たらない");
		}
		//衝突時に爆発エフェクトを表示する
		//Instantiate(explosion, transform.position, transform.rotation);
	}
}


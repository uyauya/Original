using UnityEngine;
using System.Collections;

// PlayerShoot02から発射するホーミング弾のShotsオブジェクト用
public class Bullet02 : MonoBehaviour {
	
	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	PlayerShoot02 Plshoot02;			// 発射元
	private  GameObject Enemy;
	public float DestroyTime = 1;		// 発射されてから消滅するまでの時間

	void Start () 
	{
		// Playerタグが付いているオブジェクトに付いているPlayerShoot02をPlshoot02と呼ぶことにする
		Plshoot02 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot02> ();
		// 発射される向きをPlshoot02（のmuzzle）の向きと連動させる
		transform.rotation = Plshoot02.transform.rotation;
		// 現後一定時間(DestroyTime)で自動的に消滅させる
		Destroy (gameObject, DestroyTime);
	}	

	void Update ()
	{
		// Enemy変数がnull(何もない)であれば
		if (Enemy == null) {
			//Enemyというタグがつけられたゲームオブジェクトを(Enemyが複数いるため)配列で取得
			GameObject[] allEnemies = GameObject.FindGameObjectsWithTag ("Enemy");
			//allEnemiesがnullじゃない かつ 要素数が0でなければ(Enemyが一つでもいれば)
			if (allEnemies != null && allEnemies.Length != 0) {
				// その中からランダムでターゲットを決める
				Enemy = allEnemies [UnityEngine.Random.Range (0, allEnemies.Length)];
			// そもそもEnemyのタグ付いたものがなければ
			} else {
				//何もしない
				return;
			}
		}
		// 弾のスピード
		float speed = BulletSpeed;
		// 今の位置から敵の距離を測って敵に向かって移動
		float step = Time.deltaTime * speed;
		transform.position = Vector3.MoveTowards (transform.position, Enemy.transform.position, step);

	}

	private void OnCollisionEnter(Collision collider) {
		//衝突時に爆発エフェクトを表示する
		Instantiate(explosion, transform.position, transform.rotation);
		//地形とぶつかったら消滅させる
		/*if (collider.gameObject.tag == "Floor") {		
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}	
		//敵と衝突したら消滅させる
		if (collider.gameObject.tag == "Enemy"||collider.gameObject.tag == "Wall") {*/
			Destroy (gameObject);
		}

	//}
}



using UnityEngine;
using System.Collections;
using UnityEditor;

// PlayerShoot02から発射するホーミング弾のShotsオブジェクト用
public class Bullet02 : MonoBehaviour {
	
	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	public float DecreaseDamage = 0.9f;	// 時間とともに減少していく攻撃値
	public float LowestDamage = 20.0f;	// 最低限与えるダメージ
	PlayerShoot02 Plshoot02;			// 発射元
	private  GameObject Enemy;
	public float DestroyTime = 1;		// 発射されてから消滅するまでの時間

	/*[CustomEditor(typeof(Bullet02))]
	public class Bullet02 : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			Bullet02 B02 = target as Bullet02;
			B02.BulletSpeed = EditorGUILayout.FloatField( "弾の速さ", B02.BulletSpeed);
			B02.DecreaseDamage = EditorGUILayout.FloatField( "攻撃減少値", B02.DecreaseDamage);
			B02.LowestDamage = EditorGUILayout.FloatField( "最低攻撃値", B02.LowestDamage);
			B02.DestroyTime = EditorGUILayout.FloatField( "弾消滅までの時間", B02.DestroyTime);
		}
	}*/

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
			// Enemyのタグ付いたものがなければ
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
		//damage--;
		damage = damage - (DecreaseDamage * Time.deltaTime);
		if (damage <= LowestDamage)
			damage = LowestDamage;
		//Debug.Log (damage);
	}

	private void OnCollisionEnter(Collision collider) {
		//衝突時に爆発エフェクトを表示する(敵か壁か床に当たったら)
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Wall" 
			|| collider.gameObject.tag == "Floor"|| collider.gameObject.tag == "Boss") {
			Instantiate (explosion, transform.position, transform.rotation);
			//着弾後、弾消滅
			Destroy (gameObject);
		}
	}

}


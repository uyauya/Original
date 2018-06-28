using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Bullet05 : MonoBehaviour {

	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	public float DecreaseDamage = 0.5f;	// 時間とともに減少していく攻撃値
	public float LowestDamage = 20.0f;	// 最低限与えるダメージ
	Enemy enemy;
	MultiWayShoot multiwayshoot;		// 発射元
	private Rigidbody rb;
	private Vector3 forward;
	public float DestroyTime = 3;		// 発射されてから消滅するまでの時間

	/*[CustomEditor(typeof(Bullet01))]
	public class Bullet01 : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			Bullet01 B05 = target as Bullet05;
			B05.BulletSpeed = EditorGUILayout.FloatField( "弾の速さ", B05.BulletSpeed);
			B05.DecreaseDamage = EditorGUILayout.FloatField( "攻撃減少値", B05.DecreaseDamage);
			B05.LowestDamage = EditorGUILayout.FloatField( "最低攻撃値", B05.LowestDamage);
			B05.DestroyTime = EditorGUILayout.FloatField( "弾消滅までの時間", B05.DestroyTime);
		}
	}*/

	void Start () {
		rb = this.GetComponent<Rigidbody>();
		multiwayshoot = GameObject.FindWithTag("Player").GetComponent<MultiWayShoot> ();
		//transform.rotation = multiwayshoot.transform.rotation;
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
		//transform.Translate(Vector3.forward * Time.deltaTime);
		damage = damage - (DecreaseDamage * Time.deltaTime);
		if (damage <= LowestDamage)
			damage = LowestDamage;
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
}


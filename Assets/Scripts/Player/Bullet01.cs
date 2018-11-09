using UnityEngine;
using System.Collections;
using UnityEditor;

// PlayerShootから発射するチャージ弾のShotオブジェクト用
// Shot(Bullet01A)、ShotB(Bullet01B)、ShotC(Bullet01C)継承元スクリプト
public class Bullet01 : MonoBehaviour {

	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed = 3.0f;	// 弾のスピード
	public float DecreaseDamage = 0.5f;	// 時間とともに減少していく攻撃値
	public float LowestDamage = 20.0f;	// 最低限与えるダメージ
	Enemy enemy;
	PlayerShoot Plshoot;			　　// 発射元
	public float DestroyTime = 3;	　　// 弾が（生成されてから）消滅するまでの時間

	public void Initialize () {
		// Playerタグの付いているオブジェクトのlayerShootのスクリプトをPlshootとする
		Plshoot = GameObject.FindWithTag("Player").GetComponent<PlayerShoot> ();
		// 弾発射の向きはPlshootの向きとする
		transform.rotation = Plshoot.transform.rotation;
		//（生成して）DestroyTimeで弾消滅
		Destroy (gameObject, DestroyTime);
		//}
		// 以下ビームっぽい演出の作り方
		// ShotオブジェクトにAddComponentでTrailRenderer追加
		// SimpleParticlePackをダウンロードしてMaterial追加
		// ShotのMeshRendererのチェックを外す
	}

	/*[CustomEditor(typeof(Bullet01))]
	public class Bullet01 : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			Bullet01 B01 = target as Bullet01;
			B01.BulletSpeed = EditorGUILayout.FloatField( "弾の速さ", B01.BulletSpeed);
			B01.DecreaseDamage = EditorGUILayout.FloatField( "攻撃減少値", B01.DecreaseDamage);
			B01.LowestDamage = EditorGUILayout.FloatField( "最低攻撃値", B01.LowestDamage);
			B01.DestroyTime = EditorGUILayout.FloatField( "弾消滅までの時間", B01.DestroyTime);
		}
	}*/

	void Update () {
		// 弾を前進させる（前に時間場×弾の速度）
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
		//時間経過と共に攻撃値をDecreaseDamage分減らす
		damage = damage - (DecreaseDamage * Time.deltaTime);
		//攻撃値がLowestDamageより低かったらLowestDamageにする(最低攻撃値)
		if (damage <= LowestDamage)
			damage = LowestDamage;
		//Debug.Log (damage);
		// 弾を発射した瞬間、プレイヤーと弾が衝突してしまうので、メインメニューからEdit→ProjectSettings
		// →Tags and Layers →PlayerとShotを追加。プレイヤーのレイヤーをPlayer,Bullet01のレイヤーをShotにし、
		// プレイヤーを選択してEdit→ProjectSettings→Physicsにし、InspectorでPhysicsManagerを開く
		// PlayerとShotが交差している所のチェックを外せば接触判定しない
		// 設定変えた時ChangeLayerの表示が出たらYes
	}	

	private void OnCollisionEnter(Collision collider) {
		//衝突時に爆発エフェクトを表示する(敵か壁か床に当たったら)
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Floor") {
			Instantiate (explosion, transform.position, transform.rotation);
			//着弾後、弾消滅
			Destroy (gameObject);
		}
	}
}

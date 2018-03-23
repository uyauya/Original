using UnityEngine;
using System.Collections;

// PlayerShootから発射するチャージ弾のShotオブジェクト用
// Shot(Bullet01A)、ShotB(Bullet01B)、ShotC(Bullet01C)継承元スクリプト
public class Bullet01 : MonoBehaviour {

	public GameObject explosion;	// 着弾時のエフェクト
	public float damage;			// 弾の威力
	public float BulletSpeed;		// 弾のスピード
	Enemy enemy;
	PlayerShoot Plshoot;			// 発射元
	public float DestroyTime = 3;	// 弾が（生成されてから）消滅するまでの時間

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

	void Update () {
		// 弾を前進させる（前に時間場×弾の速度）
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;

		//damage--;
		damage = damage - (0.5f * Time.deltaTime);
		if (damage <= 20)
			damage = 20;
		//Debug.Log (damage);
		// 弾を発射した瞬間、プレイヤーと弾が衝突してしまうので、メインメニューからEdit→ProjectSettings
		// →Tags and Layers →PlayerとShotを追加。プレイヤーのレイヤーをPlayer,Bullet01のレイヤーをShotにし、
		// プレイヤーを選択してEdit→ProjectSettings→Physicsにし、InspectorでPhysicsManagerを開く
		// PlayerとShotが交差している所のチェックを外せば接触判定しない
		// 設定変えた時ChangeLayerの表示が出たらYes
	}	

	private void OnCollisionEnter(Collision collider) {
		//衝突時に爆発エフェクトを表示する
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "Wall" || collider.gameObject.tag == "Floor") {
			Instantiate (explosion, transform.position, transform.rotation);	
			Destroy (gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

// 銃としてPlayerShootスクリプト、弾をBullet01スクリプトとして作る
public class Bullet01 : MonoBehaviour {

	public GameObject explosion;
	public float damage;
	Enemy enemy;
	PlayerShoot Plshoot;

	void Start () {
		// Utc_sum_humanoid（プレイヤーの名前）のオブジェクトを見つけて
		// PlayerShootのスクリプトを見つけて以後Plshootと略す
		Plshoot = GameObject.Find ("Utc_sum_humanoid").GetComponent<PlayerShoot> ();
		//（発射して）三秒後に消滅
		Destroy (gameObject, 3);
		// 以下ビームっぽい演出の作り方
		// ShotオブジェクトにAddComponentでTrailRenderer追加
		// SimpleParticlePackをダウンロードしてMaterial追加
		// ShotのMeshRendererのチェックを外す
	}	
	void Update () {

		//弾を前進させる
		transform.position += transform.forward * Time.deltaTime * 100;
		// 弾を発射した瞬間、プレイヤーと弾が衝突してしまうので、メインメニューからEdit→ProjectSettings
		// →Tags and Layers →PlayerとShotを追加。プレイヤーのレイヤーをPlayer,Bullet01のレイヤーをShotにし、
		// プレイヤーを選択してEdit→ProjectSettings→Physicsにし、InspectorでPhysicsManagerを開く
		// PlayerとShotが交差している所のチェックを外せば接触判定しない
		// 設定変えた時ChangeLayerの表示が出たらYes
	}	
	private void OnCollisionEnter(Collision collider) {

		//地形とぶつかったら消滅させる
		if (collider.gameObject.name == "Terrain") {		
			Destroy (gameObject);
			// ぶつかった場所に爆発を設定
			Instantiate (explosion, transform.position, transform.rotation);
		}	
		//敵と衝突したら消滅させる
		if (collider.gameObject.tag == "Enemy") {
			//collider.gameObject.SendMessage ("damage");
			Destroy (gameObject);
		}
		//衝突時に爆発エフェクトを表示する
		//Instantiate(explosion, transform.position, transform.rotation);
	}
}


using UnityEngine;
using System.Collections;

// 銃としてPlayerShootスクリプト、弾をBullet01スクリプトとして作る
public class Bullet01 : MonoBehaviour {

	public GameObject explosion;	// 着弾時のエフェクト
	public float damage;
	public float BulletSpeed;		// 弾移動速度
	Enemy enemy;
	PlayerShoot Plshoot;
	public float DestroyTime = 3;	// 弾が（生成されてから）消滅するまでの時間

	void Start () {
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
		// 弾を前進させる
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
		// 弾を発射した瞬間、プレイヤーと弾が衝突してしまうので、メインメニューからEdit→ProjectSettings
		// →Tags and Layers →PlayerとShotを追加。プレイヤーのレイヤーをPlayer,Bullet01のレイヤーをShotにし、
		// プレイヤーを選択してEdit→ProjectSettings→Physicsにし、InspectorでPhysicsManagerを開く
		// PlayerとShotが交差している所のチェックを外せば接触判定しない
		// 設定変えた時ChangeLayerの表示が出たらYes
	}	

	private void OnCollisionEnter(Collision collider) {
		//着弾時に爆発エフェクトを表示する
		Instantiate(explosion, transform.position, transform.rotation);
		// 弾消滅
		Destroy (gameObject);
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;	

// PlayerShoot03から発射するボムのShotssオブジェクト用
public class Bullet03 : MonoBehaviour {

	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	PlayerShoot03 Plshoot03;			// 発射元
	public GameObject prefab_HitEffect2;
	public int bombDamage = 2000;		// ボムの攻撃値
	public float DestroyTime = 1;		// 発射されてから消滅するまでの時間

	void Start () {
		Plshoot03 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot03> ();
		transform.rotation = Plshoot03.transform.rotation;
		StartCoroutine ("bom");
		//Enemy = GetComponent<Enemy>();
	}	

	// ボム設定（StartCoroutine("bom")の内容）
	IEnumerator bom(){		
			
		GameObject effect = Instantiate(prefab_HitEffect2 , transform.position , Quaternion.identity) as GameObject;	// ボムエフェクト発生
		Destroy(effect , DestroyTime);		// ボムエフェクトを、2秒後に消滅させる
		BomUpdate();
		BomAttack();				// ボムによる攻撃処理
		yield return new WaitForSeconds(2.0f);		// 2.0秒、処理を待機.
		//Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
		Destroy(gameObject);	
	}

	// ボム攻撃範囲設定
	private void BomAttack(){
		// 自分自身を中心に、半径50.0以内にいるColliderを探し、配列に格納
		Collider[] targets = Physics.OverlapSphere (transform.position, 50.0f);
		foreach (Collider col in targets) {		// targets配列を順番に処理 (その時に仮名をobjとする)
			if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<EnemyBasic>() != null) {			// タグ名がEnemyなら
				EnemyBasic enemyinsta = col.gameObject.GetComponent<EnemyBasic>();
				if (enemyinsta != null) {
				enemyinsta.Damaged(bombDamage);	// ダメージを与える
				}
			}
			if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<BossBasic>() != null) {			// タグ名がEnemyなら
				BossBasic enemyinsta = col.gameObject.GetComponent<BossBasic>();
				if (enemyinsta != null) {
					enemyinsta.Damaged(bombDamage);	// ダメージを与える
				}
			}
		}
	}

	void Update(){
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}

	void BomUpdate(){
		// プレイヤーからの距離を計測
		float distance = Vector3.Distance (GetComponent<Collider> ().transform.position, transform.position);
		//発光をプレイヤーからの距離に応じて９から下げていく
		//MainCameraにScreenOverlayを追加してScreenOverlayManager作成して追加
		//ScreenOverlayManager.intensity += Mathf.Clamp (9 - (distance / 200), 0, 1);
		// 振動を加える（MainCameraにCameraVibrationManagerを追加）
		//CamVibrationManager.vibration += Mathf.Clamp (0.5F - (distance / 200), 0, 0.5F);
		//弾のShaderをCustom/clearScreenにすれば透明に。
		//iTwwenでx,y,zの大きさをtime内に徐々に大きくする
		iTween.ScaleTo(gameObject, iTween.Hash("x",9, "y",9, "z",9, "time",5, "easetype",iTween.EaseType.linear));
	}
}

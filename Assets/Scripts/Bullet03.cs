using UnityEngine;
using System.Collections;

public class Bullet03 : MonoBehaviour {

	public GameObject explosion;
	public float damage;
	public float BulletSpeed;
	PlayerShoot03 Plshoot03;
	public GameObject prefab_HitEffect2;
	public int bombDamage = 2000;		// ボムの攻撃値
	public float DestroyTime = 1;

	void Start () {
		Plshoot03 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot03> ();
		transform.rotation = Plshoot03.transform.rotation;
		StartCoroutine("bom");	
		//Enemy = GetComponent<Enemy>();
	}	

	// ボム設定（StartCoroutine("bom")の内容）
	IEnumerator bom(){		
		yield return new WaitForSeconds(0.0f);		// 2.0秒、処理を待機.	
		GameObject effect = Instantiate(prefab_HitEffect2 , transform.position , Quaternion.identity) as GameObject;	// ボムエフェクト発生
		Destroy(effect , DestroyTime);		// ボムエフェクトを、2秒後に消滅させる
		BomAttack();				// ボムによる攻撃処理
		Destroy(gameObject);	
	}

	// ボム攻撃範囲設定
	private void BomAttack(){
		Debug.Log ("BOM");
		// 自分自身を中心に、半径50.0以内にいるColliderを探し、配列に格納
		Collider[] targets = Physics.OverlapSphere (transform.position, 50.0f);
		foreach (Collider col in targets) {		// targets配列を順番に処理 (その時に仮名をobjとする)
			if (col.gameObject.tag == "Enemy") {			// タグ名がEnemyなら
			Enemy enemyinsta = col.gameObject.GetComponent<Enemy>();
			Zombie zombieinsta = col.gameObject.GetComponent<Zombie>();
				if (!enemyinsta == null) {
				enemyinsta.Damaged(bombDamage);	// ダメージを与える
				} else if (enemyinsta == null && !zombieinsta == null ) {
				zombieinsta.Damaged(bombDamage);	// ダメージを与える
				}
			}
			// プレイヤーからの距離を計測
			float distance = Vector3.Distance(GetComponent<Collider>().transform.position, transform.position);
			//発光をプレイヤーからの距離に応じて９から下げていく
			//MainCameraにScreenOverlayを追加してScreenOverlayManager作成して追加
			ScreenOverlayManager.intensity += Mathf.Clamp(9 - (distance / 200), 0, 1);
			// 振動を加える（MainCameraにCameraVibrationManagerを追加）
			CamVibrationManager.vibration += Mathf.Clamp(0.5F - (distance / 200), 0, 0.5F);
		}
	}

	void Update(){
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}
}

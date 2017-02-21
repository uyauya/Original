using UnityEngine;
using System.Collections;

public class Bullet03 : MonoBehaviour {

	public GameObject explosion;
	public float damage;
	PlayerShoot03 Plshoot03;
	public GameObject prefab_HitEffect2;
	public int bombDamage = 2000;		// ボムの攻撃値
	

	void Start () {
		Plshoot03 = GameObject.Find ("Utc_sum_humanoid").GetComponent<PlayerShoot03> ();
		StartCoroutine("bom");	
		//Enemy = GetComponent<Enemy>();
	}	

	// ボム設定（StartCoroutine("bom")の内容）
	IEnumerator bom(){		
		yield return new WaitForSeconds(0.0f);		// 2.0秒、処理を待機.	
		GameObject effect = Instantiate(prefab_HitEffect2 , transform.position , Quaternion.identity) as GameObject;	// ボムエフェクト発生
		Destroy(effect , 2.0f);		// ボムエフェクトを、2秒後に消滅させる
		BomAttack();				// ボムによる攻撃処理
		Destroy(gameObject);	
	}

	// ボム攻撃範囲設定
	private void BomAttack(){
		// 自分自身を中心に、半径50.0以内にいるColliderを探し、配列に格納
		Collider[] targets = Physics.OverlapSphere (transform.position, 50.0f);
		foreach (Collider col in targets) {		// targets配列を順番に処理 (その時に仮名をobjとする)
			if (col.gameObject.tag == "Enemy") {			// タグ名がEnemyなら
			Enemy enemyinsta = col.gameObject.GetComponent<Enemy>();
				//nullだったら
				if (enemyinsta == null) {
					//この先をスキップして次の処理を行う
					continue;
				}
				enemyinsta.Damaged(bombDamage);	// ダメージを与える
			// プレイヤーからの距離を計測
			float distance = Vector3.Distance(GetComponent<Collider>().transform.position, transform.position);
			//発光をプレイヤーからの距離に応じて９から下げていく
			//MainCameraにScreenOverlayを追加してScreenOverlayManager作成して追加
			ScreenOverlayManager.intensity += Mathf.Clamp(9 - (distance / 200), 0, 1);
			// 振動を加える（MainCameraにCameraVibrationManagerを追加）
			CamVibrationManager.vibration += Mathf.Clamp(0.5F - (distance / 200), 0, 0.5F);
		}
	}
				//Debug.Log (damage);
				//Destroy (GetComponent<Collider>().gameObject);
	}

	void Update(){
		transform.position += transform.forward * Time.deltaTime * 1;

	}
}

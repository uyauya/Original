using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class LockOn : MonoBehaviour {

	GameObject target = null;
	bool isSearch;
	public Image lockOnImage;
	public GameObject enemyAp;
	public Image gaugeImage;
	public Text textDistance;

	void Start () {
		// 基本設定、最初はfalseにしておく
		lockOnImage = GameObject.Find ("LockOnCursor").GetComponent<Image> ();
		enemyAp = GameObject.Find ("EnemyApBase").GetComponent<GameObject> ();
		gaugeImage = GameObject.Find ("EnemyApGauge").GetComponent<Image> ();
		textDistance = GameObject.Find ("TextDistance").GetComponent<Text> ();
		isSearch = false;
		lockOnImage.enabled = false;
		enemyAp.SetActive (false);
	}
	

	void Update () {
		if (Input.GetButtonDown ("Lock")) {
			//ロックオンモード切替
			isSearch = !isSearch;
			if(!isSearch)
				//ロックを解除する
				target = null;
			else
				//一番近いターゲットを取得する
				target = FindClosestEnemy();				
		}

		if(target != null) {
			//距離が離れたらロックを解除する
			if (Vector3.Distance (target.transform.position, transform.position) > 100){
				target = null;
			}
		}
		bool isLocked = false;
		//ターゲットがいたらロックオンカーソルを表示する
		if (target != null) {
			isLocked = true;	
			lockOnImage.transform.rotation = Quaternion.identity;
			//ターゲットの表示位置にロックオンカーソルを合わせる
			lockOnImage.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
			//敵の体力をゲージに反映させる
			Enemy targetScript = target.GetComponent<Enemy>();
			gaugeImage.transform.localScale = new Vector3( (float) targetScript.armorPoint / targetScript.armorPointMax, 1, 1);
			//敵との距離を表示する
			textDistance.text = Vector3.Distance (target.transform.position, transform.position).ToString();
		} else {
			//ロックオンモード時はカーソルを回転する
			lockOnImage.transform.Rotate (0, 0, Time.deltaTime * 200);
		}
		lockOnImage.enabled = isSearch;
		//敵の体力ゲージの表示を切り替え可能にする
		enemyAp.SetActive (isLocked);
	}
	//一番近い敵を探して取得
	GameObject FindClosestEnemy() {
		GameObject[] gos;
		gos = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject closest = null;
		float distance = Mathf.Infinity;
		Vector3 position = transform.position;

		foreach (GameObject go in gos) {
			Vector3 diff = go.transform.position - position;
			float curDistance = diff.sqrMagnitude;

			if (curDistance < distance) {
				closest = go;
				distance = curDistance;
			}
		}
		if (closest != null) {	
			//一番近くの敵がロックオン範囲外ならロックしない
			if (Vector3.Distance (closest.transform.position, transform.position) > 100)
				closest = null;
		}
		return closest;
	}
}

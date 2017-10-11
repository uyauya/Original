using UnityEngine;
using System.Collections;

public class Enemy02Move : MonoBehaviour {

	GameObject target;
	public GameObject shot;
	float shotInterval = 0;
	float shotIntervalMax = 1.0F;	
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 300F; 
	float damage;	
	float timer = 0;	
	int enemyLevel = 0;
	Bullet01 b1;
	
	void Start () {
		//ターゲットを取得
		target = GameObject.Find("PlayerTarget");
		armorPoint = armorPointMax;
	}
	
	void OnCollisionEnter(Collision collider) {
		//Debug.Log (collider);

		if (collider.gameObject.tag == "Shot") {
			//b1 = GameObject.FindGameObjectWithTag ("Shot").GetComponent<Bullet01> ();

			//プレイヤーの弾からダメージを取得する
			damage = collider.gameObject.GetComponent<Bullet01>().damage;
			//Debug.Log ("あああ");
			//プレイヤーの弾と衝突したらダメージ
			//armorPoint -= damage;
			armorPoint -= damage;
			//Debug.Log(armorPoint);

			//体力が0以下になったら消滅する
			if (armorPoint <= 0){
				Destroy (gameObject);
				Instantiate(exprosion, transform.position, transform.rotation);

				//リザルト用のスコアを加算する
				//BattleManager.score ++;
			}

		}
	}
}



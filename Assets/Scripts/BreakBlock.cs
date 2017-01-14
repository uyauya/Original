using UnityEngine;
using System.Collections;

public class BreakBlock : MonoBehaviour {

	GameObject target;
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 100F; 
	float damage;	
	float timer = 0;
	RedSphere Red;
	public GameObject RedSphere;

	void Start () {

		//ターゲットを取得
		target = GameObject.Find("PlayerTarget");
		armorPoint = armorPointMax;
	}

	private void OnCollisionEnter(Collision collider) {
	
		if (collider.gameObject.tag == "Shot") {

			//プレイヤーの弾と衝突したら消滅する
			//Destroy (gameObject);
			Instantiate(exprosion, transform.position, transform.rotation);

			//プレイヤーの弾と衝突したらダメージ
			//armorPoint -= damage;
			armorPoint -= collider.gameObject.GetComponent<Bullet01>().damage;

			//体力が0以下になったら消滅する
			if (armorPoint <= 0){
				Destroy (gameObject);
				Instantiate(exprosion, transform.position, transform.rotation);
				if(Random.Range(0, 4) == 0){
					Instantiate(Red, transform.position, transform.rotation);
				}
				//リザルト用のスコアを加算する
				BattleManager.score ++;
			}
		}
	}
}



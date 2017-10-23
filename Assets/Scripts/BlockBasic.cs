using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBasic : MonoBehaviour {
	GameObject target;
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 100F;
	PlayerLevel playerLevel;
	public BattleManager battleManager;
	float damage;	
	public int EnemyScore = 100;					// ブロックを壊した時の得点
	float timer = 0;
	public GameObject RedSphere;
	public GameObject BlueSphere;
	public GameObject GreenSphere;

	public void Initialize () {
		//ターゲットを取得
		target = GameObject.Find ("PlayerTarget");
		armorPoint = armorPointMax;
		//上で宣言したplayerLevelとはPlayerタグが付いているオブジェクトに付いているPlayerLevelスクリプトのことを言っている。
		playerLevel = GameObject.FindWithTag ("Player").GetComponent<PlayerLevel> ();
	}

	// Use this for initialization
	void Start () {
		// BattleManagerオブジェクトのBattleManagerスクリプトをbattleManagerと呼ぶことにする
		battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnCollisionEnter(Collision collider) {

		if (collider.gameObject.tag == "Shot") {
			//Instantiate(exprosion, transform.position, transform.rotation);
			armorPoint -= collider.gameObject.GetComponent<Bullet01> ().damage;
		} else if (collider.gameObject.tag == "Shot2") {
			//Instantiate(exprosion, transform.position, transform.rotation);
			armorPoint -= collider.gameObject.GetComponent<Bullet02> ().damage;
		} else if (collider.gameObject.tag == "Shot5") {
			//Instantiate(exprosion, transform.position, transform.rotation);
			armorPoint -= collider.gameObject.GetComponent<Bullet05> ().damage;
		}
		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			Destroy (gameObject);
			Instantiate(exprosion, transform.position, transform.rotation);
			battleManager.Score += EnemyScore;
			// ブロック消滅時、一定確率（0,16で16分の1）でアイテム出現
			if (Random.Range (0, 16) == 0) {
				Instantiate (RedSphere, transform.position, transform.rotation);
			} else if (Random.Range (0, 14) == 0) {
				Instantiate (BlueSphere, transform.position, transform.rotation);
			} else if (Random.Range (0, 2) == 0) {
				Instantiate (GreenSphere, transform.position, transform.rotation);
			}
			//リザルト用のスコアを加算する
			BattleManager.score ++;
		}
	}
}

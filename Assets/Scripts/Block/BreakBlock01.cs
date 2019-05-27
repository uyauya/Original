using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock01 : MonoBehaviour {

	GameObject target;
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 100F; 
	float damage;	
	float timer = 0;
	public GameObject RedSphere;
	public GameObject BlueSphere;
	public GameObject GreenSphere;
	public GameObject YellowSphere;
	public int bigAttack;
	public int EnemyScore = 1000;		// 敵を倒した時の得点
	public BattleManager battleManager;
	public int RedEncount = 16;						// RedSphere生成率の分母
	public int BlueEncount = 8;
	public int GreenEncount= 32;
	public int YellowEncount= 32;
    PlayerLevel playerLevel;

    void Start () {
		battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
		//ターゲットを取得
		target = GameObject.Find("PlayerTarget");
		armorPoint = armorPointMax;
        playerLevel = GameObject.FindWithTag("Player").GetComponent<PlayerLevel>();
    }

	void OnTriggerEnter(Collider collider) {
	//private void OnCollisionEnter(Collision collider) {

		if (collider.gameObject.tag == "Shot")
        {
            //Instantiate(exprosion, transform.position, transform.rotation);
            if(collider.gameObject.GetComponent<Bullet01>() != null)
            {
                armorPoint -= collider.gameObject.GetComponent<Bullet01>().damage;
            }
            else
            {
                armorPoint -= collider.gameObject.GetComponent<Bullet01R>().damage;
            }
        }
        else if (collider.gameObject.tag == "Shot2")
        {
            //Instantiate(exprosion, transform.position, transform.rotation);
            if (collider.gameObject.GetComponent<Bullet02>() != null)
            {
                armorPoint -= collider.gameObject.GetComponent<Bullet02>().damage;
            }
            else
            {
                armorPoint -= collider.gameObject.GetComponent<Bullet02R>().damage;
            }
        }
        else if (collider.gameObject.tag == "Shot5")
        {
            //Instantiate(exprosion, transform.position, transform.rotation);
            if(collider.gameObject.GetComponent<Bullet02>() != null)
            {
                armorPoint -= collider.gameObject.GetComponent<Bullet05>().damage;
            }
			else
			{
                armorPoint -= collider.gameObject.GetComponent<Bullet05R>().damage;
            }
            //} else if (collider.gameObject.tag == "Player" && isbig == true) {
            //	bigAttack = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().BigAttack;
            //	armorPoint -= bigAttack;
        }
		else if (collider.gameObject.tag == "Weapon")
		{
			if (collider.gameObject.GetComponent<WeaponAttack>() != null)
			{
				armorPoint -= collider.gameObject.GetComponent<WeaponAttack>().damage;
			}
		}
			//体力が0以下になったら消滅する
			if (armorPoint <= 0){
				Destroy (gameObject);
				Instantiate(exprosion, transform.position, transform.rotation);
				// ブロック消滅時、一定確率（0,16で16分の1）でアイテム出現
				if (Random.Range (0, 2) == 0) {
					Instantiate (RedSphere, transform.position, transform.rotation);
				} else if (Random.Range (0, 2) == 0) {
					Instantiate (BlueSphere, transform.position, transform.rotation);
				} else if (Random.Range (0, 16) == 0) {
					Instantiate (GreenSphere, transform.position, transform.rotation);
				} else if (Random.Range (0, 8) == 0) {
					Instantiate (YellowSphere, transform.position, transform.rotation);
				}
				//リザルト用のスコアを加算する
				battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
				DataManager.Score += EnemyScore;
                playerLevel.LevelUp();
        }
	}
	void OnCollisionEnter(Collision collider) {
		bool isbig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
		if (collider.gameObject.tag == "Player" && isbig == true) {
			bigAttack = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().BigAttack;
			armorPoint -= bigAttack;
			}	
			//体力が0以下になったら消滅する
			if (armorPoint <= 0){
				Destroy (gameObject);
				Instantiate(exprosion, transform.position, transform.rotation);
				// ブロック消滅時、一定確率（0,16で16分の1）でアイテム出現
				if (Random.Range (0, RedEncount) == 0) {
					Instantiate (RedSphere, transform.position, transform.rotation);
				} else if (Random.Range (0, BlueEncount) == 0) {
					Instantiate (BlueSphere, transform.position, transform.rotation);
				} else if (Random.Range (0, GreenEncount) == 0) {
					Instantiate (GreenSphere, transform.position, transform.rotation);
				} else if (Random.Range (0, YellowEncount) == 0) {
					Instantiate (YellowSphere, transform.position, transform.rotation);
				}
				//リザルト用のスコアを加算する
				battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
				DataManager.Score += EnemyScore;
                playerLevel.LevelUp();
        }
		}
	}

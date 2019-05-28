using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator2 : MonoBehaviour
{
	public GameObject[]	PrefabEnemy;
	public float[] ApperanceRate;				//敵の出現割合
	public float CreateTime;
	Rigidbody rigidbody;
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

	// Start is called before the first frame update
    void Start()
    {
		battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
		target = GameObject.Find("PlayerTarget");
		armorPoint = armorPointMax;
		playerLevel = GameObject.FindWithTag("Player").GetComponent<PlayerLevel>();
		rigidbody = GetComponent<Rigidbody>();
		StartCoroutine("EnemyCreate" , CreateTime);
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	// 敵出現用のコルーチン
	IEnumerator EnemyCreate(float time){
		while(true){
			//敵が複数出現の場合は0.0～1.0で割合を決める
			//(0.1,0.9)や(0.4,0.6)など合計が1.0になるよう割り振る
			float num = Random.Range (0.0f, 1.0f);
			//敵が1種類の場合は何もしない（割り振らないので1.0のまま）
			if (ApperanceRate.Length == 0) {
				;//何もしない
			} else if (num < ApperanceRate [0]) {
				GameObject.Instantiate (PrefabEnemy[0]);
			} else if (num < ApperanceRate [1]) {
				GameObject.Instantiate (PrefabEnemy[1]);
			} 
			transform.position += transform.forward;
			yield return new WaitForSeconds(time);		// time秒、処理を待機.
		}
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
	void OnCollisionEnter(Collision collider) 
	{
		bool isbig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
		if (collider.gameObject.tag == "Player" && isbig == true) {
			bigAttack = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().BigAttack;
			armorPoint -= bigAttack;
		}	
		//体力が0以下になったら消滅する
		if (armorPoint <= 0)
		{
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

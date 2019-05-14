using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;	
using UnityEditor;

public class Bullet03R : MonoBehaviour {

	public GameObject explosion;		
	public float damage;				
	public float BulletSpeed;			
	PlayerShoot03R Plshoot03R;			
	public GameObject prefab_HitEffect2;
	public int BombDamage = 2000;		
	public float DestroyTime = 1;		
	public float RadDistance = 50.0f;	

	void Start () {
		Plshoot03R = GameObject.FindWithTag("Player").GetComponent<PlayerShoot03R> ();
		transform.rotation = Plshoot03R.transform.rotation;
		StartCoroutine ("bom");
	}	
		
	IEnumerator bom(){	
		GameObject effect = Instantiate(prefab_HitEffect2 , transform.position , Quaternion.identity) as GameObject;	
		Destroy(effect , DestroyTime);			
		BomUpdate();
		BomAttack();							
		yield return new WaitForSeconds(2.0f);	
		//Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
		Destroy(gameObject);	
		//プレイヤーの動けるようにする
		//PlayerController.isStop = false;
	}
		
	private void BomAttack(){
		Collider[] targets = Physics.OverlapSphere (transform.position, RadDistance);
		foreach (Collider col in targets) {	
			if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<EnemyBasic>() != null) {
				EnemyBasic enemyinsta = col.gameObject.GetComponent<EnemyBasic>();
				if (enemyinsta != null) {
					enemyinsta.Damaged(BombDamage);	
				}
			}
			if (col.gameObject.tag == "Enemy" && col.gameObject.GetComponent<BossBasic>() != null) {
				BossBasic enemyinsta = col.gameObject.GetComponent<BossBasic>();
				if (enemyinsta != null) {
					enemyinsta.Damaged(BombDamage);	
				}
			}
		}
	}

	void Update(){
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}

	void BomUpdate(){
		float distance = Vector3.Distance (GetComponent<Collider> ().transform.position, transform.position);
		iTween.ScaleTo(gameObject, iTween.Hash("x",9, "y",9, "z",9, "time",5, "easetype",iTween.EaseType.linear));
	}
}


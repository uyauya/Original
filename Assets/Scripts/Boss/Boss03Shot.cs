using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Shot : MonoBehaviour {

	public GameObject Boss03muzzle;
	protected EnemyBasic enemyBasic;
	public float ShotTime = 0.5f;
	bool dead = false;
	public float Acceleration = 0.1f;

	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
	}

	void Update () {
		// Animator の dead が true なら Update 処理を抜ける
		if( enemyBasic.animator.GetBool("dead") == true ) return;
		// オブジェクトの場所取りをする
		transform.position = Boss03muzzle.transform.position + (Boss03muzzle.transform.forward * 2);
		transform.position += transform.forward;
		transform.RotateAround (Boss03muzzle.transform.position + (Boss03muzzle.transform.forward * 2), Vector3.up, 1);
		enemyBasic.timer += Time.deltaTime;

		// ターゲット（プレイヤー）との距離が0.5以内なら
		//if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
		if((Mathf.Abs( enemyBasic.target.transform.position.z - this.transform.position.z) < 2 && 
			Mathf.Abs( enemyBasic.target.transform.position.x - this.transform.position.x) < 2)) {
			StartCoroutine ("Boss03ShotCoroutine");
		}
	}

	IEnumerator Boss03ShotCoroutine (){
		this.transform.position += new Vector3(0,0,1) * Time.deltaTime * enemyBasic.EnemySpeed * ShotTime;
		Acceleration++;
		yield return new WaitForSeconds(1.0f);
		this.transform.position += new Vector3(0,0,-1) * Time.deltaTime * enemyBasic.EnemySpeed * ShotTime * 0.5f;
	}

}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffenceWall : MonoBehaviour {

	Enemy enemy;
	Diffence Dif;
	private ModelColorChange modelColorChange;
	private bool isInvincible;
	public float Destroytime;
	public float Speed;

	void Start () {
		Dif = GameObject.FindWithTag("Player").GetComponent<Diffence> ();
		transform.rotation = Dif.transform.rotation;
		Destroy (gameObject, Destroytime);	
	}	
	void Update () {
		
		transform.position += transform.forward * Time.deltaTime * Speed;

		}
			
	private void OnCollisionEnter(Collision collider) {

		//Enemyとぶつかった時にコルーチンを実行（下記IEnumerator参照）
		if (collider.gameObject.tag == "Enemy") {
			StartCoroutine ("DiffenceCoroutine");
		}
		//ShotEnemyとぶつかった時にコルーチンを実行（下記IEnumerator参照）
		else if (collider.gameObject.tag == "ShotEnemy") {
			StartCoroutine ("DiffenceCoroutine");
		}
	}

	// Itweenを使ってコルーチン作成（Itweenインストール必要あり）
	IEnumerator DiffenceCoroutine ()
	{
		//gameObject.layer = LayerMask.NameToLayer("Diffence");
		//while文を10回ループ
		int count = 1;
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", transform.position - (transform.forward * 0f),
			"time", 0.1f, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			modelColorChange.ColorChange(new Color (1,0,0,1));;
			yield return new WaitForSeconds(0.1f);
			modelColorChange.ColorChange(new Color (1,1,1,1));
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		isInvincible = false;
		gameObject.layer = LayerMask.NameToLayer("DiffenceWall");
	}
}

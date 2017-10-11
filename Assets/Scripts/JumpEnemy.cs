using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class JumpEnemy : MonoBehaviour {

	private Animator animator;		// 《Animator》コンポーネント用の変数
	GameObject target;
	public float armorPoint;
	public float armorPointMax = 100F;
	private bool isInvincible;			// 無敵処理（ダメージ受けた際に使用）
	public float InvincibleTime;		// 無敵時間
	float timer = 0;
	float damage;							// playerに与えるダメージ
	Bullet01 b1;
	public float timeOut;
	private float timeElapsed;
	private ModelColorChange modelColorChange;
	public float KnockBackRange;
	public void Damaged(float damagedPoint){
		this.armorPoint -= damagedPoint;	// Playerから受けたダメージの設定
	}
	public float DestroyTime;
	public float DamageTime;
	public float JumpForce;
	public int TargetRange = 30;
	public float EnemySpeed = 1;
	public float EnemyRotate = 5;
	public float Search = 1;
	public GameObject DestroyEffect;



	void Start () {
		animator = GetComponent< Animator >();		// 《Animator》コンポーネントの取得
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		target = GameObject.Find("PlayerTarget");	//ターゲットを取得
		armorPoint = armorPointMax;
	}


	void Update () {
		timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (target.transform.position, transform.position) <= TargetRange) {

			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(target.transform.position - transform.position), Time.deltaTime * EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * EnemySpeed;
		}
		timeElapsed += Time.deltaTime;
		if (timeElapsed >= timeOut) {
			transform.position += transform.up * Time.deltaTime * JumpForce;
			timeElapsed = 0.0f;
		}
		// ターゲット（プレイヤー）との距離が0.5以内なら
		if (Vector3.Distance (target.transform.position, transform.position) <= Search) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(target.transform.position - transform.position), Time.deltaTime * EnemySpeed);
			transform.position += transform.forward * Time.deltaTime * 1;
			if (timeElapsed >= timeOut) {
				transform.position += transform.up * Time.deltaTime * JumpForce;
				timeElapsed = 0.0f;
			}
			//animator.SetBool ("attack", true);
			//Debug.Log ("hit");
		}
		// Animator の dead が true なら Update 処理を抜ける
		//if( animator.GetBool("dead") == true ) return;
	}


	void OnCollisionEnter(Collision collider) {		//Debug.Log (collider);

		if (collider.gameObject.tag == "Shot") {
			Debug.Log (collider.gameObject.name);
			damage = collider.gameObject.GetComponent<Bullet01> ().damage;
			StartCoroutine ("DamageCoroutine");
			Destroy (gameObject, DestroyTime);	
			//animator.SetBool ("damaged", true);		// 《Animator》の変数deadを true に変更.
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot2") {
			damage = collider.gameObject.GetComponent<Bullet02> ().damage;
			StartCoroutine ("DamageCoroutine");
			//animator.SetBool ("damaged", true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject, DestroyTime);	
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot3") {
			damage = collider.gameObject.GetComponent<Bullet03> ().damage;
			StartCoroutine ("DamageCoroutine");
			//animator.SetBool ("damaged", true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject, DestroyTime);	
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot5") {
			damage = collider.gameObject.GetComponent<Bullet05> ().damage;
			StartCoroutine ("DamageCoroutine");
			//animator.SetBool ("damaged", true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject, DestroyTime);	
			armorPoint -= damage;
		}
		//animator.SetBool("damaged" , true);		// 《Animator》の変数deadを true に変更.
		//armorPoint -= damage;


		//体力が0以下になったら消滅する
		if (armorPoint <= 0) {
			//animator.SetBool ("dead", true);		// 《Animator》の変数deadを true に変更.
			//Instantiate(DestroyEffect, transform.position, transform.rotation);
			Destroy (gameObject, DestroyTime);	
			//Debug.Log("消滅");	
			//リザルト用のスコアを加算する
			//BattleManager.score++;
		}

	}

	// Itweenを使ってコルーチン作成（Itweenインストール必要あり）
	IEnumerator DamageCoroutine ()
	{
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
		//while文を10回ループ
		int count = 10;
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", transform.position - (transform.forward * KnockBackRange),
			"time", InvincibleTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			//透明にする
			//Debug.Log ("色変える");
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.05秒待つ
			//Debug.Log ("戻す");
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.05秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		isInvincible = false;
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Enemy");
		//iTweenのアニメーション

	}	
}
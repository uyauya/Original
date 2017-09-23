using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBasic : MonoBehaviour {
	public Animator animator;		// 《Animator》コンポーネント用の変数
	public GameObject target;
	public GameObject particle;
	public float armorPoint;
	public float armorPointMax; 
	public float timer;
	protected float damage;							// playerに与えるダメージ
	protected bool isInvincible;			// 無敵処理（ダメージ受けた際に使用）
	public float InvincibleTime;		// 無敵時間
	protected ModelColorChange modelColorChange;
	public float KnockBackRange;
	public void Damaged(float damagedPoint){
		this.armorPoint -= damagedPoint;	// Playerから受けたダメージの設定
	}
	public float DestroyTime;
	public float DamageTime;
	public int TargetRange;
	public float EnemySpeed;
	public float EnemyRotate;
	public float Search;
	public GameObject DestroyEffect;

	/*[CustomEditor(typeof(Zombie))]
	public class ZombieEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			Zombie En = target as Zombie;
			En.armorPointMax = EditorGUILayout.FloatField( "最大HP", En.armorPointMax);
			En.InvincibleTime = EditorGUILayout.FloatField( "無敵時間", En.InvincibleTime);
			En.KnockBackRange = EditorGUILayout.FloatField( "ノックバック距離", En.KnockBackRange);
			En.TargetRange = EditorGUILayout.IntField( "プレイヤー探索範囲", En.TargetRange);
			En.EnemySpeed  = EditorGUILayout.FloatField( "移動スピード", En.EnemySpeed );
			En.EnemyRotate = EditorGUILayout.FloatField( "振り向き速度", En.EnemyRotate);
			En.Search = EditorGUILayout.FloatField( "プレイヤーとの間合い", En.Search);
		}
	}*/

	public void Initialize () {
		animator = GetComponent< Animator >();		// 《Animator》コンポーネントの取得
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		//target = GameObject.Find("PlayerTarget");	//ターゲットを取得
		target = GameObject.FindWithTag("Player");
		armorPoint = armorPointMax;
	}

	void Update () {
		
	}


	void OnCollisionEnter(Collision collider) {
		//Debug.Log (collider);

		if (collider.gameObject.tag == "Shot") {
			damage = collider.gameObject.GetComponent<Bullet01> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);		// 《Animator》の変数deadを true に変更.
			//Destroy (gameObject, DamageTime);	
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot2") {
			damage = collider.gameObject.GetComponent<Bullet02> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);		// 《Animator》の変数deadを true に変更.
			//Destroy (gameObject, DamageTime);	
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot3") {
			damage = collider.gameObject.GetComponent<Bullet03> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);		// 《Animator》の変数deadを true に変更.
			//Destroy (gameObject, DamageTime);	
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot5") {
			damage = collider.gameObject.GetComponent<Bullet05> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);		// 《Animator》の変数deadを true に変更.
			//Destroy (gameObject, DestroyTime);	
			armorPoint -= damage;
		}

		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			//animator.SetBool("dead" , true);		// 《Animator》の変数deadを true に変更.
			Instantiate(DestroyEffect, transform.position, transform.rotation);
			Destroy (gameObject, DestroyTime);	
			//リザルト用のスコアを加算する
			BattleManager.score ++;
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

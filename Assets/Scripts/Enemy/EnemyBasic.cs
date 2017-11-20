using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 敵キャラクタ管理用
// ステータス欄等の共通項目を保持。キャラクタ独自の項目のみ別スクリプトに書き足す。
public class EnemyBasic : MonoBehaviour {
	public int enemyLevel = 0;
	public Animator animator;						// Animatorセット用
	public GameObject target;						// プレイヤー認識用
	public GameObject shot;
	public float shotInterval = 0;					// 攻撃間隔計測開始
	public float shotIntervalMax = 1.0F;			// 攻撃間隔（～秒ごとに攻撃）
	public GameObject exprosion;					// 爆発処理
	public GameObject particle;
	public float armorPoint = 100;						// HP現在値
	//public float armorPointMax = 100;				// 最大HP 
	public int TargetRange;							// プレイヤをターゲット認識する距離
	public float EnemySpeed;						// 移動スピード
	public float JumpForce;							// ジャンプ力
	public float EnemyRotate;						// 振り向き速度
	public float Search;							// プレイヤを探すサーチレンジ
	public float timer;
	protected float damage;							// playerからのダメージ判定
	public void Damaged(float damagedPoint){
		this.armorPoint -= damagedPoint;			// Playerから受けたダメージの値
	}
	public int EnemyAttack = 100;					// プレイヤに与えるダメージ
	protected bool isInvincible;					// 無敵処理（ダメージ受けた際に使用）
	public float InvincibleTime;					// 無敵時間
	protected ModelColorChange modelColorChange;	// 点滅処理
	public float KnockBackRange;					// 攻撃を受けた際のノックバックの距離
	public float DestroyTime;						// （HP0になった際の）消滅するまでの時間
	public GameObject DestroyEffect;				// 消滅時発生エフェクト
	public float timeElapsed;
	public float timeOut;
	public BattleManager battleManager;
	PlayerLevel playerLevel;
	public int EnemyScore = 1000;					// 敵を倒した時の得点
	public GameObject RedSphere;					// アーマーポイント回復用玉（アイテムタグ3）
	public GameObject BlueSphere;					// ブーストポイント回復用玉（アイテムタグ2）
	public GameObject GreenSphere;					// ボス面移行用玉（アイテムタグ3）
	public GameObject YellowSphere;					// プレイヤ巨大＆無敵化
	public int RedEncount = 16;						// RedSphere生成率の分母
	public int BlueEncount = 8;
	public int GreenEncount= 32;
	public int YellowEncount= 32;
	public int bigAttack;


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
		
	}

	void Start () {
		// BattleManagerオブジェクトのBattleManagerスクリプトをbattleManagerと呼ぶことにする
		//battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
		//bigAttack = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().BigAttack;
		// 《Animator》コンポーネントの取得
		animator = GetComponent< Animator >();		
		// 被ダメージ時の点滅処理
		modelColorChange = gameObject.GetComponent<ModelColorChange>();　
		// Playerタグが付いているオブジェクトをターゲットにする
		target = GameObject.FindWithTag ("Player");	
		// ゲーム開始時、アーマーポイントを最大にする
		//armorPoint = armorPointMax;	
		// Playerタグが付いているオブジェクトのPlayerLevelをplayerLevelと呼ぶ
		playerLevel = GameObject.FindWithTag ("Player").GetComponent<PlayerLevel> ();
		battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
	}


	void Update () {
		
	}


	void OnCollisionEnter(Collision collider) {
		// Shotタグが付いているオブジェクトに当たったら
		if (collider.gameObject.tag == "Shot") {
			// Bullet01スクリプトのdamageを受け取る
			damage = collider.gameObject.GetComponent<Bullet01> ().damage;
			// ダメージコルーチン（下記参照）
			StartCoroutine ("DamageCoroutine");
			// 敵Animatorダメージ判定時に"damaged"をtrueへ
			animator.SetBool("damaged" , true);
			// 敵アーマーポイントからBullet01スクリプトのdamage値を差し引く
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot2") {
			damage = collider.gameObject.GetComponent<Bullet02> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot3") {
			damage = collider.gameObject.GetComponent<Bullet03> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Shot5") {
			damage = collider.gameObject.GetComponent<Bullet05> ().damage;
			StartCoroutine ("DamageCoroutine");
			animator.SetBool("damaged" , true);
			armorPoint -= damage;
		} else if (collider.gameObject.tag == "Player") {
			bigAttack = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().BigAttack;
			armorPoint -= bigAttack;
			//Debug.Log (bigAttack);
		}

		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			Debug.Log ("敵"+gameObject.name);
			// 敵消滅用エフェクト発生
			Instantiate(DestroyEffect, transform.position, transform.rotation);
			// バトルマネージャーにスコア（EnemyScoreで設定）を加算する
			battleManager = GameObject.Find ("BattleManager").GetComponent<BattleManager> ();
			battleManager.Score += EnemyScore;
			// プレイヤのレベルアップ判定(PlayerLevel参照)
			playerLevel.LevelUp ();
			// 敵消滅
			Destroy (gameObject, DestroyTime);	
			//Instantiate(exprosion, transform.position, transform.rotation);
			// ブロック消滅時、一定確率（0,RedEncountでRedEncount分の1）でアイテム出現
			if (Random.Range (0, RedEncount) == 0) {
				Instantiate (RedSphere, transform.position, transform.rotation);
			} else if (Random.Range (0, BlueEncount) == 0) {
				Instantiate (BlueSphere, transform.position, transform.rotation);
			} else if (Random.Range (0, GreenEncount) == 0) {
				Instantiate (GreenSphere, transform.position, transform.rotation);
			} else if (Random.Range (0, YellowEncount) == 0) {
				Instantiate (YellowSphere, transform.position, transform.rotation);
			}
		}

	}

	// Itweenを使ってコルーチン作成（Itweenインストール必要あり）
	IEnumerator DamageCoroutine ()
	{
		//レイヤーをPlayerDamageに変更
		//gameObject.layer = LayerMask.NameToLayer("EnemyDamage");
		//while文を10回ループ
		int count = 10;
		iTween.MoveTo(gameObject, iTween.Hash(
			// その場からKnockBackRange数値分後(-transform.forwardで後)に移動
			"position", transform.position - (transform.forward * KnockBackRange),
			// 無敵(ダメージ判定なし)時間設定（秒）
			"time", InvincibleTime, 
			"easetype", iTween.EaseType.linear
		));
		// 無敵(ダメージ判定なし)にして
		isInvincible = true;
		while (count > 0){
			//透明にする(ModelColorChange参照)
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.05秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.05秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		// 無敵解除
		isInvincible = false;
		//レイヤーをPlayerに戻す
		//gameObject.layer = LayerMask.NameToLayer("Enemy");
		//iTweenのアニメーション
	}	
}

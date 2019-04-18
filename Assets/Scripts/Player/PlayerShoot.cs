using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	//シーンをまたいで使用する際に使用
using UnityEditor;					//Editor画面を変更する際に使用

// 銃としてPlayerShootスクリプト、弾をBullet01スクリプトとして作る
public class PlayerShoot : MonoBehaviour {
	public GameObject Bullet01;					// 弾（Shotオブジェクトのスクリプト）
	public GameObject Bullet01B;				// 弾（Shotオブジェクトのスクリプト）
	public GameObject Bullet01C;				// 弾（Shotオブジェクトのスクリプト）
	public GameObject UBullet01;					// 弾（Shotオブジェクトのスクリプト）
	public GameObject UBullet01B;				// 弾（Shotオブジェクトのスクリプト）
	public GameObject UBullet01C;				// 弾（Shotオブジェクトのスクリプト）
	private GameObject bullet01;
	public Transform muzzle;					// 弾発射元（銃口）
	public GameObject muzzleFlash;				// 発射する時のフラッシュ（現在未使用）
	public GameObject ErekiSmoke;				// チャージ用エフェクトのパーティクル
	public float interval;
	public float shotInterval;					// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;			// キー押してから離すまでの（チャージ）時間
	private float triggerDownTimeStart = 0F;	// キー押した時間
	private float triggerDownTimeEnd = 0F;		// キー離した時間
	public float Attack = 200;					// ショットの攻撃値
	public float attackPoint;					// プレイヤの攻撃値（ショットする際に付け足す。PlayerController参照）
	private float power = 0;					// 溜めによる攻撃力追加値
	public float damage;						// Bullet1に受け渡す弾自体の攻撃力
	public float ChargeTime;					// チャージ時間
	public float ChargeTime1 = 1.0f;			// チャージ時間
	public float ChargeTime2 = 3.0f;			// チャージ時間
	public float AddAttackRate = 2.5f;          // 追加攻撃比率
	private float NormalSize = 1.0F;
	public float BigSize;
	private Animator animator;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;						// ブーストポイント（PlayerController参照）
	Bullet01 bullet01_script;					// 弾Shot
	public GameObject effectPrefab;				// チャージ用エフェクトの格納場所
	public GameObject effectObject;
	public int BpDown = 50;						// 発射時の消費ブーストポイント
	public bool isCharging = false;				// チャージ中かどうかの判定（開始時はチャージしていないのでfalse）		
	private AudioSource[] audioSources;
	public int PlayerNo;						//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	private Pause pause;						// ポーズ中かどうか（Pause参照）
	public bool isBig;							// 巨大化しているかどうか
	public static bool isShoot = false;			//ショットを撃っている状態かどうか
    //public BattleManager battleManager;

    /*[CustomEditor(typeof(PlayerShoot))]
	public class PlayerShootEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			PlayerShoot Ps = target as PlayerShoot;
			Ps.shotIntervalMax = EditorGUILayout.FloatField( "ショット間隔", Ps.shotIntervalMax);
			Ps.Attack		   = EditorGUILayout.FloatField( "攻撃力", Ps.Attack);
			Ps.ChargeTime1	   = EditorGUILayout.FloatField( "溜め時間1", Ps.ChargeTime1);
			Ps.ChargeTime2	   = EditorGUILayout.FloatField( "溜め時間2", Ps.ChargeTime2);
			Ps.AddAttackRate   = EditorGUILayout.FloatField( "追加攻撃比率", Ps.AddAttackRate);
			Ps.BiggerTime	   = EditorGUILayout.FloatField( "拡大率", Ps.BiggerTime);
			Ps.BpDown		   = EditorGUILayout.FloatField( "ゲージ消費量", Ps.BpDown);
		}
	}*/

    void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSources = gameObject.GetComponents<AudioSource>(); 			// 音源が複数の場合はGetComponents（複数形）になる
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();			// ポーズ中かどうか判定用
		attackPoint = DataManager.AttackPoint;

	}

	void Update () {
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
            //プレイヤが巨大化中だったらショット不可(PlayerAp参照)
            isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
            //isBig = battleManager.Player.GetComponent<PlayerAp>().isBig;
            //巨大化中でなかったら
            if (isBig == false) {	
				// Fire1（標準ではCtrlキー)を押した時
				if (Input.GetButtonDown ("Fire1")) {
					//チャージ開始（チャージ時間計測開始）
					triggerDownTimeStart = Time.time;
					// チャージ開始のフラグを立てる
					isCharging = true;
					//muzzleオブジェクトにエフェクトを生成
					effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
					//パーティクルのErekiSmokeを赤色で発生させる
					effectObject.transform.Find ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.red;
					// muzzleはプレイヤーの子で付いているのでSetParent (muzzle)で設定（オブジェクト生成の場合は必要なし）
					effectObject.transform.SetParent (muzzle);
				}
				// Fire1を押し続けている間
				if (Input.GetButton ("Fire1")) {
					isShoot = true;
					//押している時間がChargeTime1以上であり、ChargeTime2以下なら
					if (Time.time - triggerDownTimeStart >= ChargeTime1 && Time.time - triggerDownTimeStart <= ChargeTime2) {
						//ParticleSystemを赤に、ErekiSmokeの色を赤から白に変更
						effectObject.GetComponent<ParticleSystem> ().startColor = Color.red;
						effectObject.transform.Find ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.white;
						//ChargeTime2を超えたなら
					} else if (Time.time - triggerDownTimeStart > ChargeTime2) {
						//ParticleSystemを青に、ErekiSmokeの色を白から黄に変更
						effectObject.GetComponent<ParticleSystem> ().startColor = Color.blue;
						effectObject.transform.Find ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.yellow;
					}
					// スケールを大きくする.
					//effectObject.transform.localScale *= BiggerTime;
					//effectObject.GetComponent<ParticleSystem>().startSize = 1.0f;
				}
				// Fire1を離した時
				if (Input.GetButtonUp ("Fire1")) {
                    isShoot = false;
                    //チャージ時間計測終了
                    triggerDownTimeEnd = Time.time;
					// チャージ開始のフラグを消す
					isCharging = false;
					//エフェクトを削除
					Destroy (effectObject);
					// キーを離した状態から押し始めたじかんの差分を計測して
					ChargeTime = triggerDownTimeEnd - triggerDownTimeStart;
					// ショットの攻撃値にプレイヤ攻撃値×溜め攻撃力率×溜め時間を加えてdamageとする
					damage = Attack + attackPoint * AddAttackRate * ChargeTime;
					//Debug.Log (damage);
					// もしboostPoint 数値がBpDown以上なら
					if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
						// Bullet01をmuzzleの位置、方向に合わせて生成
						//bullet01 = GameObject.Instantiate (Bullet01, muzzle.position, Quaternion.identity)as GameObject;
						// Bulletnを発動（下記参照）
						Bullet ();
						// BpDown数値消費
						GetComponent<PlayerController> ().boostPoint -= BpDown;
					}
					// Shotのアニメーションに切り替え
					// ショットのように作動したら自動的にニュートラルに戻る場合はTriggerの方がよい
					animator.SetTrigger ("Shot");　	
					// 一定以上間が空いたらインターバル終了(ショットの時間間隔）
					if (time >= interval) {    
						time = 0f;
					}
				}
				//マズルフラッシュを表示する
				//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
			}
		}
	}
	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet() 
	{
		//ショット溜め時間がChargeTime1以下だったら
		if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 1)|| (DataManager.PlayerNo == 2)) 
		{
			if (ChargeTime <= ChargeTime1) 
			{
				// Bullet01(小弾)を生成
				GameObject bulletObject = GameObject.Instantiate (Bullet01)as GameObject;
				// Bullet01の生成位置をmuzzleに設定(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
				bulletObject.transform.position = muzzle.position;
				// Bullet01オブジェクトにダメージ計算を渡す(上記参照)
				bulletObject.GetComponent<Bullet01> ().damage = this.damage;
				//ショット溜め時間がChargeTime1を超えChargeTime2以下だったら
			} else if (ChargeTime1 < ChargeTime && ChargeTime  <= ChargeTime2) 
			{
				// Bullet01B(中弾)を生成
				GameObject bulletObject = GameObject.Instantiate (Bullet01B)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01B> ().damage = this.damage;
			//それ以外(溜め時間がChargeTime2を超えた)なら
			} else 
			{
				// Bullet01C(大弾)を生成
				GameObject bulletObject = GameObject.Instantiate (Bullet01C)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01C> ().damage = this.damage;
			}
		}
		else if (DataManager.PlayerNo == 3)
		{
			if (ChargeTime <= ChargeTime1) 
			{	
				// Bullet01(小弾)を生成
				GameObject bulletObject = GameObject.Instantiate (UBullet01)as GameObject;
				// Bullet01の生成位置をmuzzleに設定(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
				bulletObject.transform.position = muzzle.position;
				// Bullet01オブジェクトにダメージ計算を渡す(上記参照)
				bulletObject.GetComponent<Bullet01> ().damage = this.damage;
					//ショット溜め時間がChargeTime1を超えChargeTime2以下だったら
			} else if (ChargeTime1 < ChargeTime && ChargeTime  <= ChargeTime2) 
			{
				// Bullet01B(中弾)を生成
				GameObject bulletObject = GameObject.Instantiate (UBullet01B)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01B> ().damage = this.damage;
				//それ以外(溜め時間がChargeTime2を超えた)なら
			} else 
			{
				// Bullet01C(大弾)を生成
				GameObject bulletObject = GameObject.Instantiate (UBullet01C)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01C> ().damage = this.damage;
			}
		}
		//キャラクタ別にSoundManager（声担当）とSoundManager2（効果音担当）から音を鳴らす
		if ((PlayerNo == 0)|| (PlayerNo == 3)){	// こはく
		SoundManager.Instance.Play(0,gameObject);
		SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
		}
		if (PlayerNo == 1) {	// ゆうこ
		SoundManager.Instance.Play(1,gameObject);	
		SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
		}
		if (PlayerNo == 2) {	// みさき
		SoundManager.Instance.Play(2,gameObject);		
		SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
		}
	}

	public void KickEvent (){	//ショット時のアニメーション
		Debug.Log("kick");
	}


}


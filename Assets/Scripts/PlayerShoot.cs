using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 銃としてPlayerShootスクリプト、弾をBullet01スクリプトとして作る
public class PlayerShoot : MonoBehaviour {
	public GameObject Bullet01;					// 弾（Shotオブジェクトのスクリプト）
	private GameObject bullet01;
	public Transform muzzle;					// 弾発射元（銃口）
	public GameObject muzzleFlash;				// 発射する時のフラッシュ（現在未使用）
	public GameObject ErekiSmoke;				// チャージ用エフェクトのパーティクル
	public float interval;
	public float shotInterval;					// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;			// チャージ時間
	private float triggerDownTimeStart = 0F;	// チャージ開始時間
	private float triggerDownTimeEnd = 0F;		// チャージ終了時間
	public float Attack;						// プレイヤの攻撃値（ショットする際に付け足す）
	private float power = 0;
	public float damage;
	private float chargeTime;
	private float NormalSize = 1.0F;
	public float BigSize;
	public float BiggerTime;
	private Animator animator;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet01 bullet01_script;
	public GameObject effectPrefab;				// チャージ用エフェクトの格納場所
	public GameObject effectObject;
	public int BpDown;							// 発射時の消費ブーストポイント
	public bool isCharging = false;				
	private AudioSource[] audioSources;
	public int PlayerNo;						//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	private Pause pause;						// ポーズ中かどうか（Pause参照）

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSources = gameObject.GetComponents<AudioSource>(); // 音源が複数の場合はGetComponents（複数形）になる
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
	}

	void Update () {

	if(pause.isPause == false) {
		// Fire1（標準ではCtrlキー)を押された瞬間.
		if (Input.GetButtonDown ("Fire1")) {
			// Fire1を押してチャージ開始.
			triggerDownTimeStart = Time.time;
			// チャージ開始のフラグを立てる
			isCharging = true;
			//エフェクトを生成
			effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
			effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.red;　// チャージエフェクト用
			// muzzleはプレイヤーの子で付いているのでSetParent (muzzle)で設定（オブジェクト生成の場合は必要なし）
			effectObject.transform.SetParent (muzzle);
		} 
		if (Input.GetButton ("Fire1")) {
			if (Time.time - triggerDownTimeStart >= 1.0f && Time.time - triggerDownTimeStart<= 3.0f) {
				effectObject.GetComponent<ParticleSystem> ().startColor = Color.red;
				effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.yellow;
			} else if (Time.time - triggerDownTimeStart > 3.0f) {
				effectObject.GetComponent<ParticleSystem> ().startColor = Color.blue;
				effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.white;
			}
				// スケールを大きくする.
				effectObject.transform.localScale *= BiggerTime;
				effectObject.GetComponent<ParticleSystem>().startSize = 1.0f;
			// キーを離すことによりチャージ終了
		}
		if (Input.GetButtonUp ("Fire1")) {
			triggerDownTimeEnd = Time.time;
			// チャージ開始のフラグを消す
			isCharging = false;
			//エフェクトを削除
			Destroy (effectObject);
			// キーを離した状態から押し始めたじかんの差分を計測して
			float chargeTime  = triggerDownTimeEnd - triggerDownTimeStart;
			// ダメージを初期値＋時間に攻撃値を掛けた数値を計算
			damage = Attack + Attack * 2.5f * chargeTime;
			// Shotのアニメーションに切り替え
			if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
				bullet01 = GameObject.Instantiate (Bullet01, muzzle.position, Quaternion.identity)as GameObject;
				// Bulletnを設定（下記参照）
				Bullet ();
				GetComponent<PlayerController> ().boostPoint -= BpDown;
			}
			animator.SetTrigger ("Shot");　	// ショットのように作動したら自動的にニュートラルに戻る場合はTriggerの方がよい
			// 一定以上間が空いたらチャージタイムのリセット
			if (time >= interval) {    
				time = 0f;
			}
		}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		}
	}
	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet() {
		// ショットの時間間隔
		//if (Time.time - shotInterval > shotIntervalMax) {
			//shotInterval = Time.time;
			// Bulletのゲームオブジェクトを生成してbulletObjectとする
			GameObject bulletObject = GameObject.Instantiate (Bullet01)as GameObject;
			bulletObject.transform.localScale *= chargeTime;
			//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
			bulletObject.transform.position = muzzle.position;
			//Debug.Log (PlayerNo);
			if (PlayerNo == 0) {
			SoundManager.Instance.Play(0,gameObject);
			SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
			SoundManager.Instance.Play(1,gameObject);
			SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
			SoundManager.Instance.Play(2,gameObject);
			SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
			}
			// bulletObjectのオブジェクトにダメージ計算を渡す
			bulletObject.GetComponent<Bullet01> ().damage = this.damage;
	}

	public void KickEvent (){	//ショット時のアニメーション
		Debug.Log("kick");
	}


}


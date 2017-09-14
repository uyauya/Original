using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 銃としてPlayerShootスクリプト、弾をBullet01スクリプトとして作る
public class PlayerShoot : MonoBehaviour {
	public GameObject Bullet01;
	private GameObject bullet01;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public GameObject ErekiSmoke;
	public float interval;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	public float Attack;
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
	public GameObject effectPrefab;
	public GameObject effectObject;
	public int BpDown;
	public bool isCharging = false;
	private AudioSource[] audioSources;
	public int PlayerNo;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSources = gameObject.GetComponents<AudioSource>(); // 音源が複数の場合はGetComponents（複数形）になる
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.red;　// チャージエフェクト用
	}

	void Update () {
		
		// Fire1（標準ではCtrlキー)を押された瞬間.
		if (Input.GetButtonDown ("Fire1")) {
			// Fire1を押してチャージ開始.
			triggerDownTimeStart = Time.time;
			// チャージ開始のフラグを立てる
			isCharging = true;
			//エフェクトを生成
			effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
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
			Debug.Log (PlayerNo);
			if (PlayerNo == 0) {
			SoundManager.Instance.Play(0,gameObject);
			SoundManager.Instance.PlayDelayed (1, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
			SoundManager.Instance.Play(2,gameObject);
			SoundManager.Instance.PlayDelayed (3, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
			SoundManager.Instance.Play(4,gameObject);
			SoundManager.Instance.PlayDelayed (5, 0.2f, gameObject);
			}
			// bulletObjectのオブジェクトにダメージ計算を渡す
			bulletObject.GetComponent<Bullet01> ().damage = this.damage;
	}

	public void KickEvent (){	//ショット時のアニメーション
		Debug.Log("kick");
	}


}


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
	//public float speed;
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
	//private AudioSource audioSource;
	private AudioSource[] audioSources;
	//public float audioTimer = 0.2f;
	//private bool audioAction;
	public AudioClip audioClip01;
	public AudioClip audioClip02;
	public int PlayerNo;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		//audioSource = gameObject.GetComponent<AudioSource>();
		audioSources = gameObject.GetComponents<AudioSource>(); // 音源が複数の場合はGetComponents（複数形）になる
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.red;　// チャージエフェクト用
		//audioAction = false;
		//Debug.Log(DataManager.PlayerNo + "プレイヤシュート");
		//PlayerNo = DataManager.PlayerNo;
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
			//Debug.Log("配置");
			//bullet01 = GameObject.Instantiate (Bullet01, muzzle.position, Quaternion.identity)as GameObject;
		} 
		if (Input.GetButton ("Fire1")) {
			// Fire1を押してチャージ開始.
			// 2秒たったら.
			//Debug.Log("スイッチオン");
			//if (Time.time - triggerDownTimeStart >= NormalSize || effectObject.transform.localScale.x < BigSize ) {
			if (Time.time - triggerDownTimeStart >= 1.0f && Time.time - triggerDownTimeStart<= 3.0f) {
				effectObject.GetComponent<ParticleSystem> ().startColor = Color.red;
				//ErekiSmoke.GetComponent<ParticleSystem> ().startColor = Color.red;
				effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.yellow;
			} else if (Time.time - triggerDownTimeStart > 3.0f) {
				effectObject.GetComponent<ParticleSystem> ().startColor = Color.blue;
				//ErekiSmoke.GetComponent<ParticleSystem> ().startColor = Color.blue;
				effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.white;
			}
				// スケールを大きくする.
				effectObject.transform.localScale *= BiggerTime;
				//スケールを確認
				//Debug.Log(effectObject.transform.localScale);
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
			
				//bullet01.transform.localScale *= chargeTime;
				Bullet ();
				GetComponent<PlayerController> ().boostPoint -= BpDown;
			}
			animator.SetTrigger ("Shot");　	// ショットのように作動したら自動的にニュートラルに戻る場合はTriggerの方がよい
			//audioSources[0].PlayOneShot(audioClip01);
			//audioSources[1].clip = audioClip02;
			//audioSources[1].PlayDelayed(0.2f);
			//animator.SetBool("Shot",true);
			// 一定以上間が空いたらチャージタイムのリセット
			if (time >= interval) {    
				time = 0f;
				//animator.SetBool("Shot",false);

			}

			// Bulletnを設定（下記参照）
			//Bullet();
		}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		// audioSources[0]の一定時間後にaudioSources[1]を鳴らす。
		// STARTにaudioAction = false;を入れておく。
		/*if(audioSources[0].isPlaying==true){	// audioSources[0]が鳴れば
			audioAction = true;
		}
		if( audioAction == true){
			audioTimer-=Time.deltaTime;			// audio用時間計測開始
		}
		if(audioTimer <= 0.0f){					// 時間差があれば（一定以上経過したら）audioSources[1]を鳴らす
			audioSources[1].PlayOneShot(audioSources[1].clip);	// PlayOneShotで連射時音を重ねて発生可能にする
		}*/

	}

	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet() {
		// ショットの時間間隔
		//if (Time.time - shotInterval > shotIntervalMax) {
			//shotInterval = Time.time;
			// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		//if(GetComponent<PlayerController> ().boostPoint >= BpDown){
			GameObject bulletObject = GameObject.Instantiate (Bullet01)as GameObject;
			//Debug.Log("バレット");
			bulletObject.transform.localScale *= chargeTime;
			//Debug.Log("弾ビッグ");
			//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
			bulletObject.transform.position = muzzle.position;
			Debug.Log (PlayerNo);
			if (PlayerNo == 0) {
			SoundManager4.Instance.Play(0,gameObject);
			SoundManager4.Instance.PlayDelayed (1, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
			SoundManager4.Instance.Play(2,gameObject);
			SoundManager4.Instance.PlayDelayed (3, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
			SoundManager4.Instance.Play(4,gameObject);
			SoundManager4.Instance.PlayDelayed (5, 0.2f, gameObject);
			}
			//GetComponent<PlayerController> ().boostPoint -= BpDown;
			// bulletObjectのオブジェクトにダメージ計算を渡す
			bulletObject.GetComponent<Bullet01> ().damage = this.damage;
		//}
	}

	public void KickEvent (){	//ショット時のアニメーション
		Debug.Log("kick");
	}


}


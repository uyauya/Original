using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private AudioSource audioSource;
	//private AudioSource[] audioSources;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		//audioSources = gameObject.GetComponents<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.red;
	}

	void Update () {
		
		//Debug.Log(bullet01);

		// Fire1（標準ではCtrlキー)を押された瞬間.
		if (Input.GetButtonDown ("Fire1")) {
			// Fire1を押してチャージ開始.
			triggerDownTimeStart = Time.time;
			// チャージ開始のフラグを立てる
			isCharging = true;
			//エフェクトをInstantiate
			effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
			//Debug.Log("エフェクト");
			effectObject.transform.SetParent (muzzle);
			//Debug.Log("配置");
			// bullet01生成、Bullet01のゲームオブジェクトを生成.
			//bullet01 = GameObject.Instantiate (Bullet01, muzzle.position, Quaternion.identity)as GameObject;
		} 
		if (Input.GetButton ("Fire1")) {
			// Fire1を押してチャージ開始.
			// 2秒たったら.
			//Debug.Log("スイッチオン");
			//if (Time.time - triggerDownTimeStart >= NormalSize || effectObject.transform.localScale.x < BigSize ) {
			if (Time.time - triggerDownTimeStart >= 1.0f && Time.time - triggerDownTimeStart<= 3.0f) {
				effectObject.GetComponent<ParticleSystem> ().startColor = Color.red;
				//Debug.Log("赤赤");
				//ErekiSmoke.GetComponent<ParticleSystem> ().startColor = Color.red;
				effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.yellow;
				//Debug.Log("赤");
			} else if (Time.time - triggerDownTimeStart > 3.0f) {
				effectObject.GetComponent<ParticleSystem> ().startColor = Color.blue;
				//Debug.Log("青青");
				//ErekiSmoke.GetComponent<ParticleSystem> ().startColor = Color.blue;
				effectObject.transform.FindChild ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.white;
				//Debug.Log("青");
			}
				// スケールを大きくする.
				//Debug.Log("巨大化");
				effectObject.transform.localScale *= BiggerTime;
				//スケールを確認
				//Debug.Log(effectObject.transform.localScale);
				effectObject.GetComponent<ParticleSystem>().startSize = 1.0f;
			//}
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
			//Debug.Log (damage);
			// Shotのアニメーションに切り替え
			bullet01 = GameObject.Instantiate (Bullet01, muzzle.position, Quaternion.identity)as GameObject;
			bullet01.transform.localScale *= chargeTime;
			Bullet();
			animator.SetTrigger ("Shot");
			//animator.SetBool("Shot",true);
			// 一定以上間が空いたらチャージタイムのリセット
			if (time >= interval) {    
				time = 0f;
				//animator.SetBool("Shot",false);
			}

			// Bulletnoを設定（下記参照）
			//Bullet();
		}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);

		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
		//audioSources [0].Play ();
		//audioSources [1].Play ();
	}

	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet() {
		// ショットの時間間隔
		//if (Time.time - shotInterval > shotIntervalMax) {
			//shotInterval = Time.time;
			// Bullet01のゲームオブジェクトを生成してbulletObjectとする
			GameObject bulletObject = GameObject.Instantiate (Bullet01)as GameObject;
			//Debug.Log("バレット");
			bulletObject.transform.localScale *= chargeTime;
			//Debug.Log("弾ビッグ");
			//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
			bulletObject.transform.position = muzzle.position;


			// bulletObjectのオブジェクトにダメージ計算を渡す
			bulletObject.GetComponent<Bullet01> ().damage = this.damage;
		//}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}


}


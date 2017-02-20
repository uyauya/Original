using UnityEngine;
using System.Collections;

// 銃としてPlayerShootスクリプト、弾をBullet01スクリプトとして作る
public class PlayerShoot : MonoBehaviour {
	public GameObject Bullet01;
	private GameObject bullet01;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float speed = 1000F;
	public float interval = 0.5F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	private float Attack = 100;
	private float power = 0;
	public float damage;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet01 bullet01_script;
	public GameObject effectPrefab;
	public GameObject effectObject;

	public bool isCharging = false;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
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
			effectObject = Instantiate (effectPrefab, this.transform.position, Quaternion.identity);
			// bullet01生成、Bullet01のゲームオブジェクトを生成.
			bullet01 = GameObject.Instantiate (Bullet01, this.transform.position, Quaternion.identity)as GameObject;
		} else if (Input.GetButton ("Fire1")) {
			// Fire1を押してチャージ開始.
			// 2秒たったら.
			if (Time.time - triggerDownTimeStart >= 1.0f) {
				// スケールを大きくする.
				bullet01.transform.localScale *= 1.01f;
			}
			// キーを離すことによりチャージ終了
		} else if (Input.GetButtonUp ("Fire1")) {
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
			animator.SetTrigger ("Shot");
			//animator.SetBool("Shot",true);
			// 一定以上間が空いたらチャージタイムのリセット
			if (time >= interval) {    
				time = 0f;
				//animator.SetBool("Shot",false);
			}

			// Bulletnoを設定（下記参照）
			Bullet();
		}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);

		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
	}

	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet() {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject bulletObject = GameObject.Instantiate (Bullet01)as GameObject;
		//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
		bulletObject.transform.position = muzzle.position;
		// bulletObjectのオブジェクトにダメージ計算を渡す
		bulletObject.GetComponent<Bullet01> ().damage = this.damage;
	}


}


using UnityEngine;
using System.Collections;

public class MultiWayShoot : MonoBehaviour {

	public GameObject Bullet05;
	private GameObject bullet05;
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
	Bullet05 bullet05_script;
	public GameObject effectPrefab;
	public GameObject effectObject;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}

	void Update () {

		//Debug.Log(bullet01);

		// Fire1（標準ではCtrlキー)を押された瞬間.
		if (Input.GetButtonDown ("Fire5")) {
			// Fire1を押してチャージ開始.
			triggerDownTimeStart = Time.time;
			//エフェクトをInstantiate
			effectObject = Instantiate (effectPrefab, this.transform.position, Quaternion.identity);
			// bullet01生成、Bullet01のゲームオブジェクトを生成.
			bullet05 = GameObject.Instantiate (Bullet05, this.transform.position, Quaternion.identity)as GameObject;
		} else if (Input.GetButton ("Fire5")) {
			// Fire1を押してチャージ開始.
			// 2秒たったら.
			if (Time.time - triggerDownTimeStart >= 1.0f) {
				// スケールを大きくする.
				bullet05.transform.localScale *= 1.01f;
			}
			// キーを離すことによりチャージ終了
		} else if (Input.GetButtonUp ("Fire5")) {
			triggerDownTimeEnd = Time.time;
			//エフェクトを削除
			Destroy (effectObject);
			// キーを離した状態から押し始めたじかんの差分を計測して
			float chargeTime  = triggerDownTimeEnd - triggerDownTimeStart;
			// ダメージを初期値＋時間に攻撃値を掛けた数値を計算
			damage = Attack + Attack * 2.5f * chargeTime;
			//Debug.Log (damage);
			// Shotのアニメーションに切り替え
			animator.SetTrigger ("Shot");
			// 一定以上間が空いたらチャージタイムのリセット
			if (time >= interval) {    
				time = 0f;
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
		GameObject bulletObject = GameObject.Instantiate (Bullet05)as GameObject;
		//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
		bulletObject.transform.position = muzzle.position;
		// bulletObjectのオブジェクトにダメージ計算を渡す
		bulletObject.GetComponent<Bullet01> ().damage = this.damage;
	}


}
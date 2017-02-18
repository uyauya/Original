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
	void Bullet ()
	{
		// 15度間隔の散弾
		// -2,-1,0,1,2といった具合に5発を設定
		for (int i = -2; i < 5; i++) {
			//初期配置の半径(弾の生成元からの半径)
			float rad = 5f;
			//真ん中を中心に15度間隔に配置位置　
			Vector3 pos = new Vector3 (
				rad * Mathf.Sin (Mathf.Deg2Rad * (i * 15f)),
				0,
				rad * Mathf.Cos (Mathf.Deg2Rad * (i * 15f))
			);
			//生成
			GameObject bulletObject = Instantiate (Bullet05);
			//弾を配置
			bulletObject.transform.position = transform.TransformPoint (pos);
			//回転を設定（弾を拡散するよう回転させる）
			bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
			// bulletObjectのオブジェクトにダメージ計算を渡す
			bulletObject.GetComponent<Bullet05> ().damage = this.damage;
		}
	}

}
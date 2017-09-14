using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MultiWayShoot : MonoBehaviour {

	public GameObject Bullet05;
	private GameObject bullet05;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed = 1000F;
	public float interval = 0.5F;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	//private float time = 0F;
	//private float triggerDownTime = 0F;
	//private float triggerDownTimeStart = 0F;
	///private float triggerDownTimeEnd = 0F;
	private float Attack;
	//private float power = 0;
	public float damage = 2000;
	//private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet05 bullet05_script;
	public GameObject effectPrefab;
	public GameObject effectObject;
	public int BulletGap = 15;
	public float BulletRad = 5;
	public int BulletNumber = 5;
	public int FirstBullet = -2;
	public int BpDown;
	public int PlayerNo;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}

	void Update () {

		// Fire1（標準ではCtrlキー)を押された瞬間.
		if (Input.GetButtonDown ("Fire5")) {
			if(GetComponent<PlayerController> ().boostPoint >= BpDown)
			// Fire1を押してチャージ開始.
			//triggerDownTimeStart = Time.time;
			//エフェクトをInstantiate
			effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
			// bullet01生成、Bullet01のゲームオブジェクトを生成.
			//bullet05 = GameObject.Instantiate (Bullet05, muzzle.position, Quaternion.identity)as GameObject;
			Bullet ();
		//} else if (Input.GetButton ("Fire5")) {
			//if(GetComponent<PlayerController> ().boostPoint >= BpDown)
			// Fire1を押してチャージ開始.
			// 2秒たったら.
			//if (Time.time - triggerDownTimeStart >= 1.0f) {
				// スケールを大きくする.
				//bullet05.transform.localScale *= 1.00f;
				//Bullet ();
			//}
			// キーを離すことによりチャージ終了
		//} else if (Input.GetButtonUp ("Fire5")) {
			//triggerDownTimeEnd = Time.time;

			Bullet ();
			// 弾発射間隔設定
			//shotInterval += Time.deltaTime;

			//エフェクトを削除
			//Destroy (effectObject);
			// キーを離した状態から押し始めたじかんの差分を計測して
			//float chargeTime = triggerDownTimeEnd - triggerDownTimeStart;
			// ダメージを初期値＋時間に攻撃値を掛けた数値を計算
			//damage = Attack + Attack * 2.5f * chargeTime;
			damage = Attack;
			//Debug.Log (damage);
			// Shotのアニメーションに切り替え
			animator.SetTrigger ("Shot");
			// 一定以上間が空いたらチャージタイムのリセット
			//if (time >= interval) {    
			//	time = 0f;
			//}

			//boostPoint -= BpDown;
			//if (shotInterval > shotIntervalMax) {
				// Bulletnoを設定（下記参照）
				
				//マズルフラッシュを表示する
				//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
				//弾発射間隔リセット
				//shotInterval = 0;
			}
			//音を重ねて再生する
			//audioSource.PlayOneShot (audioSource.clip);
	}

	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet ()
	{
		// ショットの時間間隔
		if(Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			if(GetComponent<PlayerController> ().boostPoint >= BpDown)
			// 15度間隔の散弾
		// -2,-1,0,1,2といった具合に5発を設定
			for (int i = FirstBullet; i < BulletNumber; i++) {
				//初期配置の半径(弾の生成元からの半径)
				float rad = BulletRad;
				//真ん中を中心に15度間隔に配置位置　
				Vector3 pos = new Vector3 (
					             rad * Mathf.Sin (Mathf.Deg2Rad * (i * BulletGap)),
					             0,
					             rad * Mathf.Cos (Mathf.Deg2Rad * (i * BulletGap))
				             );
				//生成
				GameObject bulletObject = Instantiate (Bullet05);			

			//弾を配置
			bulletObject.transform.position = transform.TransformPoint (pos);
			//回転を設定（弾を拡散するよう回転させる）
			bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
			// 回転計算をした後に弾の座標を上に上げる
			bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);
			// bulletObjectのオブジェクトにダメージ計算を渡す
					if (PlayerNo == 0) {
						SoundManager.Instance.Play(24,gameObject);
						SoundManager.Instance.PlayDelayed (25, 0.2f, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play(26,gameObject);
						SoundManager.Instance.PlayDelayed (27, 0.2f, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play(28,gameObject);
						SoundManager.Instance.PlayDelayed (29, 0.2f, gameObject);
					}
				GetComponent<PlayerController> ().boostPoint -= BpDown;
				bulletObject.GetComponent<Bullet05> ().damage = this.damage;
			}
		}

	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}
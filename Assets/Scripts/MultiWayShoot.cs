using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// 多段同時ショット
public class MultiWayShoot : MonoBehaviour {

	public GameObject Bullet05;
	private GameObject bullet05;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float interval = 0.5F;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float Attack;
	public float attackPoint;					// プレイヤの攻撃値（ショットする際に付け足す。PlayerController参照）
	public float damage = 2000;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet05 bullet05_script;
	public int BulletGap = 15;
	public float BulletRad = 1;
	public int BulletNumber = 4;
	public int FirstBullet = -3;
	public int BpDown;
	public int PlayerNo;
	private Pause pause;
	private int timeCount;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		attackPoint = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint;
	}

	void Update () {
		if (pause.isPause == false) {
			//timeCount += 1;
			// Fire1（標準ではCtrlキー)を押された瞬間.
			shotInterval += Time.deltaTime;
			if (Input.GetButtonUp ("Fire1")) {
				if (GetComponent<PlayerController> ().boostPoint >= BpDown)
					//effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
				if (timeCount % 5 == 0) {
					Bullet ();
				}
				damage = Attack + attackPoint;
				animator.SetTrigger ("Shot");

				//マズルフラッシュを表示する
				//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
			}
			//音を重ねて再生する
			//audioSource.PlayOneShot (audioSource.clip);
		}
	}
	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet ()
	{
		// ショットの時間間隔
			//if(Time.time - shotInterval > shotIntervalMax) {
			//shotInterval = Time.time;
			if(GetComponent<PlayerController> ().boostPoint >= BpDown)
			// 15度間隔の散弾
			// -2,-1,0,1,2といった具合に5発を設定
			for (int i = FirstBullet; i < BulletNumber; i++) {
				//Debug.Log (i);
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
				//Debug.Log (Bullet05);
			//弾を配置
			bulletObject.transform.position = transform.TransformPoint (pos);
			//bulletObject.transform.position = muzzle.position;
			//回転を設定（弾を拡散するよう回転させる）
			bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
				//Debug.Log (transform.rotation);
			// 回転計算をした後に弾の座標を上に上げる
			bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);
				if (PlayerNo == 0) {
					SoundManager.Instance.Play(21,gameObject);
					SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.Play(22,gameObject);
					SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.Play(23,gameObject);
					SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
				}
					
				GetComponent<PlayerController> ().boostPoint -= BpDown;
				// bulletObjectのオブジェクトにダメージ計算を渡す
				bulletObject.GetComponent<Bullet05> ().damage = this.damage;
			}

	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}
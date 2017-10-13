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
	public float damage = 2000;
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
	private Pause pause;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
	}

	void Update () {
		if (pause.isPause == false) {
			// Fire1（標準ではCtrlキー)を押された瞬間.
			if (Input.GetButtonDown ("Fire1")) {
				if (GetComponent<PlayerController> ().boostPoint >= BpDown)
					effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
				Bullet ();
				damage = Attack;
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
						SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play(26,gameObject);
						SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play(28,gameObject);
						SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
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
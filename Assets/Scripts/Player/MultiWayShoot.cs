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
	public float shotInterval;					// ショットの時間間隔
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
	public int BulletGap = 15;					// 弾の発射角度
	public float BulletRad = 1;					// 弾の発射元（プレイヤから発射もとまでの距離）
	public int BulletNumber = 5;				// 弾生成（～まで）
	public int FirstBullet = -4;				// 弾生成（～から）
	public int BpDown;
	public int PlayerNo;
	private Pause pause;
	private int timeCount;
	public bool isBig;							// 巨大化しているかどうか

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		attackPoint = DataManager.AttackPoint;
	}

	void Update () {
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			if (isBig == false) {
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
	}

	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet ()
	{
		// ショットの時間間隔
			//if(Time.time - shotInterval > shotIntervalMax) {
			//shotInterval = Time.time;
			if(GetComponent<PlayerController> ().boostPoint >= BpDown)
			// 15度間隔の散弾
			// FirstBulletからBulletNumberまでの数を弾生成数（i）とする
			for (int i = FirstBullet; i < BulletNumber; i++) {
				//Debug.Log (i);
				//弾配置場所(弾の生成元からの半径)
				float rad = BulletRad;
				//真ん中（０）を中心に（角度）BulletGap間隔に配置。配置場所をposとする　
				Vector3 pos = new Vector3 (
					             rad * Mathf.Sin (Mathf.Deg2Rad * (i * BulletGap)),
					             0,
					             rad * Mathf.Cos (Mathf.Deg2Rad * (i * BulletGap))
				             );
				// Bullet05の付いたオブジェクトを生成
				GameObject bulletObject = Instantiate (Bullet05);			
			// 弾を（posの場所に）配置
			bulletObject.transform.position = transform.TransformPoint (pos);
			// 回転を設定（弾を拡散するよう回転させて振り分けて配置）
			bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
			// 回転計算をした後に弾の座標をnew Vector3(0,1,0)で上に上げる
			bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);
				if (PlayerNo == 0) { 
					//SoundManager.Instance.Play(21,gameObject);
					SoundManagerKohaku.Instance.Play(7,gameObject);
					SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
				}
				if (PlayerNo == 1) {
					//SoundManager.Instance.Play(22,gameObject);
					SoundManagerYuko.Instance.Play(7,gameObject);	
					SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
				}
				if (PlayerNo == 2) {
					//SoundManager.Instance.Play(23,gameObject);
					SoundManagerMisaki.Instance.Play(7,gameObject);	
					SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
				}
				// 	ブーストポイントをBpDown分消費
				GetComponent<PlayerController> ().boostPoint -= BpDown;
				// bulletObjectのオブジェクトにダメージ計算を渡す
				bulletObject.GetComponent<Bullet05> ().damage = this.damage;
			}

	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// 多段同時ショット
public class PlayerShoot05 : MonoBehaviour {

	public GameObject Bullet05;
	private GameObject bullet05;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float interval = 0.5F;
	public float shotInterval = 0.1F;			// ショットの時間間隔
	private float Attack;
	public float attackPoint;					// プレイヤの攻撃値（ショットする際に付け足す。PlayerController参照）
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
	private int timeCount;
	public int shootCount = 5;

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
			if (Input.GetButton ("Fire1")) {
				if (GetComponent<PlayerController> ().boostPoint >= BpDown)
					StartCoroutine (AutoShoot (shootCount));
				damage = Attack + attackPoint;
				animator.SetTrigger ("Shot");
				//マズルフラッシュを表示する
				//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
			}
		}
	}
		
	IEnumerator AutoShoot(int shootCount)
	{
		for (int j = 0; j < shootCount; j++)
		{
				Bullet ();
			yield return new WaitForSeconds(0.2f);
		}
		yield return new WaitForSeconds(shotInterval);
	}

	void Bullet ()
	{
		if(GetComponent<PlayerController> ().boostPoint >= BpDown)
			for (int i = FirstBullet; i < BulletNumber; i++) {
				float rad = BulletRad;
				Vector3 pos = new Vector3 (
					rad * Mathf.Sin (Mathf.Deg2Rad * (i * BulletGap)),
					0,
					rad * Mathf.Cos (Mathf.Deg2Rad * (i * BulletGap))
				);
				GameObject bulletObject = Instantiate (Bullet05);			
				bulletObject.transform.position = transform.TransformPoint (pos);
				bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
				bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);

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

	public void KickEvent (){
		Debug.Log("kick");
	}
}

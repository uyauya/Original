using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// ホーミングショット
public class PlayerShoot02 : MonoBehaviour {

	public GameObject Bullet02;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float Attack;
	public float damage = 500;
	public Image gaugeImage;
	public int boostPoint;
	public int attackPoint;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet02 bullet02_script;
	public int BpDown;
	public int PlayerNo;
	private Pause pause;
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
		float boostpoint = GetComponent<PlayerController> ().boostPoint;
		int Attackpoint = DataManager.AttackPoint;
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			if (isBig == false) {
				if (Input.GetButtonUp ("Fire1")) {
					if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
						damage = Attack += attackPoint;
						animator.SetTrigger ("Shots");
						GetComponent<PlayerController> ().boostPoint -= BpDown;
						Bullets ();
					}
					//マズルフラッシュを表示する
					//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
				}
			}
		}
	}

	void Bullets() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet02)as GameObject;
			bulletObject.transform.position = muzzle.position;
			if (PlayerNo == 0) {
				//SoundManager.Instance.Play(3,gameObject);
				SoundManagerKohaku.Instance.Play(1,gameObject);
				SoundManager2.Instance.PlayDelayed (1, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
				//SoundManager.Instance.Play(4,gameObject);
				SoundManagerYuko.Instance.Play(1,gameObject);	
				SoundManager2.Instance.PlayDelayed (1, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
				//SoundManager.Instance.Play(5,gameObject);
				SoundManagerMisaki.Instance.Play(1,gameObject);	
				SoundManager2.Instance.PlayDelayed (1, 0.2f, gameObject);
			}
			bulletObject.GetComponent<Bullet02> ().damage = this.damage;
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}

}

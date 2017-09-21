using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot03 : MonoBehaviour {
	
	public GameObject Bullet03;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float Attack;
	public float damage = 500;
	public Image gaugeImage;
	public int boostPoint;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet03 bullet03_script;
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
			if (Input.GetButtonUp ("Fire3")) {	
				damage = Attack;
				if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
					animator.SetTrigger ("Shotss");
					GetComponent<PlayerController> ().boostPoint -= BpDown;
					Bulletss ();
					//マズルフラッシュを表示する
					//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
				}
			}
		}
	}

	void Bulletss() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet03)as GameObject;
			bulletObject.transform.position = muzzle.position;
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(12,gameObject);
				SoundManager.Instance.PlayDelayed (13, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(14,gameObject);
				SoundManager.Instance.PlayDelayed (15, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(16,gameObject);
				SoundManager.Instance.PlayDelayed (17, 0.2f, gameObject);
			}
			bulletObject.GetComponent<Bullet03> ().damage = this.damage;
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet02 bullet02_script;
	public int BpDown;
	public int PlayerNo;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}

	void Update () {
		int boostpoint = GetComponent<PlayerController> ().boostPoint;
		if (Input.GetButtonUp ("Fire2")) {
			if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
				damage = Attack;
				animator.SetTrigger ("Shots");
				GetComponent<PlayerController> ().boostPoint -= BpDown;
				Bullets ();
			}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		}
	}

	void Bullets() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet02)as GameObject;
			bulletObject.transform.position = muzzle.position;
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(6,gameObject);
				SoundManager.Instance.PlayDelayed (7, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(8,gameObject);
				SoundManager.Instance.PlayDelayed (9, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(10,gameObject);
				SoundManager.Instance.PlayDelayed (11, 0.2f, gameObject);
			}
			bulletObject.GetComponent<Bullet02> ().damage = this.damage;
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}

}

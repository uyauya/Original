using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot02 : MonoBehaviour {

	public GameObject Bullet02;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed;
	public float interval;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	public float Attack;
	private float power = 0;
	public float damage;
	private float chargeTime;
	public Image gaugeImage;
	public int boostPoint;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet02 bullet02_script;
	public int BpDown;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}

	void Update () {
		int boostpoint = GetComponent<PlayerController> ().boostPoint;
		if (Input.GetButtonDown("Fire2")) {
			triggerDownTimeStart = Time.time;
			//Debug.Log (time);
		} else if (Input.GetButtonUp ("Fire2")) {
			triggerDownTimeEnd = Time.time;
			float chargeTime  = triggerDownTimeEnd - triggerDownTimeStart;
			//if (chargeTime >= 0.5f) {
			if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
			damage = Attack + Attack * 0.0f * chargeTime;
				//Debug.Log (damage);
				animator.SetTrigger ("Shots");
				if (time >= interval) {	
					time = 0f;
				}
				GetComponent<PlayerController> ().boostPoint -= BpDown;
				Bullets ();
			}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		}
		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
	}

	void Bullets() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet02)as GameObject;
			bulletObject.transform.position = muzzle.position;
			bulletObject.GetComponent<Bullet02> ().damage = this.damage;
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}

}

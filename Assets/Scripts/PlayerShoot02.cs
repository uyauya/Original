using UnityEngine;
using System.Collections;

public class PlayerShoot02 : MonoBehaviour {

	public GameObject Bullet02;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float speed = 600F;
	public float interval = 0.5F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	private float Attack = 1500;
	private float power = 0;
	public float damage;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet02 bullet02_script;

	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();


	}

	void Update () {
		if (Input.GetButton("Fire2")) {
			triggerDownTimeStart = Time.time;
			//Debug.Log (time);
		} else if (Input.GetButtonUp ("Fire2")) {
			triggerDownTimeEnd = Time.time;
			float chargeTime  = triggerDownTimeEnd - triggerDownTimeStart;
			damage = Attack + Attack * 0.0f * chargeTime;
			//Debug.Log (damage);
			animator.SetTrigger ("Shots");
			if (time >= interval) {	
				time = 0f;
			}
			Bullets();
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		}
		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
	}

	void Bullets() {
		GameObject bulletObject = GameObject.Instantiate (Bullet02)as GameObject;
		bulletObject.transform.position = muzzle.position;
		bulletObject.GetComponent<Bullet02> ().damage = this.damage;
	}


}

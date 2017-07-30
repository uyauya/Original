﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot03 : MonoBehaviour {
	
	public GameObject Bullet03;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed = 10F;
	public float interval = 0.5F;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	public float Attack = 500;
	private float power = 0;
	public float damage;
	private float chargeTime;
	public Image gaugeImage;
	public int boostPoint;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet03 bullet03_script;
	public int BpDown;
	public int PlayerNo;
	
	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (Input.GetButton("Fire3")) {
			triggerDownTimeStart = Time.time;
			//Debug.Log (time);
		} else if (Input.GetButtonUp ("Fire3")) {
			triggerDownTimeEnd = Time.time;
			float chargeTime  = triggerDownTimeEnd - triggerDownTimeStart;
			damage = Attack + Attack * 0.0f * chargeTime;
			//Debug.Log (damage);
			if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
			animator.SetTrigger ("Shotss");
			if (time >= interval) {	
				time = 0f;
			}
			GetComponent<PlayerController> ().boostPoint -= BpDown;
			Bulletss();
			//マズルフラッシュを表示する
			//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
			}
		}
		//音を重ねて再生する
		//audioSource.PlayOneShot(audioSource.clip);
	}
	

	void Bulletss() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet03)as GameObject;
			bulletObject.transform.position = muzzle.position;
			if (PlayerNo == 0) {
				SoundManager4.Instance.Play(12,gameObject);
				SoundManager4.Instance.PlayDelayed (13, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager4.Instance.Play(14,gameObject);
				SoundManager4.Instance.PlayDelayed (15, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager4.Instance.Play(16,gameObject);
				SoundManager4.Instance.PlayDelayed (17, 0.2f, gameObject);
			}
			bulletObject.GetComponent<Bullet03> ().damage = this.damage;
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}


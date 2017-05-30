using UnityEngine;
using System.Collections;

public class PlayerShoot03 : MonoBehaviour {
	
	public GameObject Bullet03;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed = 10F;
	public float interval = 0.5F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	public float Attack = 500;
	private float power = 0;
	public float damage;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet03 bullet03_script;
	
	void Start () {
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
			animator.SetTrigger ("Shotss");
			if (time >= interval) {	
				time = 0f;
			}
			Bulletss();
			//マズルフラッシュを表示する
			//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		}
		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
	}
	

	void Bulletss() {
		GameObject bulletObject = GameObject.Instantiate (Bullet03)as GameObject;
		bulletObject.transform.position = muzzle.position;
		bulletObject.GetComponent<Bullet03> ().damage = this.damage;
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}


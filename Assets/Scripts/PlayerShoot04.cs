using UnityEngine;
using System.Collections;

public class PlayerShoot04 : MonoBehaviour {

	public GameObject Bullet04;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float speed = 1000F;
	public float interval = 0.5F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	private float Attack = 100;
	private float power = 0;
	public float damage;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet01 bullet01_script;
	
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (Input.GetButton("Fire4")) {
			triggerDownTimeStart = Time.time;
			//Debug.Log (time);
		} else if (Input.GetButtonUp ("Fire4")) {
			triggerDownTimeEnd = Time.time;
			float chargeTime  = triggerDownTimeEnd - triggerDownTimeStart;
			damage = Attack + Attack * 0f * chargeTime;
			//Debug.Log (damage);
			animator.SetTrigger ("Shot");
			if (time >= interval) {	
				time = 0f;
			}
			Bullet();
		}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		
		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
	}
	
	void Bullet() {
		GameObject bulletObject = GameObject.Instantiate (Bullet04)as GameObject;
		bulletObject.transform.position = muzzle.position + transform.TransformDirection(Vector3.forward * 2);
		bulletObject.GetComponent<Bullet01> ().damage = this.damage;
	}
	
	
}

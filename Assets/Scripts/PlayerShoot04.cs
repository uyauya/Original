using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot04 : MonoBehaviour {

	public GameObject Bullet04;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed = 1000F;
	public float interval = 0.5F;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;
	private float triggerDownTimeStart = 0F;
	private float triggerDownTimeEnd = 0F;
	public float Attack = 100;
	private float power = 0;
	public float damage;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet01 bullet01_script;
	public int BpDown;
	
	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
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
			if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
				animator.SetTrigger ("Shot");
				if (time >= interval) {	
					time = 0f;
				}
				GetComponent<PlayerController> ().boostPoint -= BpDown;
				Bullet ();
			}
		}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		
		//音を重ねて再生する
		//audioSource.PlayOneShot(audioSource.clip);

	}
	
	void Bullet() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet04)as GameObject;
			bulletObject.transform.position = muzzle.position + transform.TransformDirection(Vector3.forward * 2) + new Vector3(0, -0.3f, 0);
			//bulletObject.GetComponent<Bullet01> ().damage = this.damage;
			SoundManager.Instance.Play(0,gameObject);
			SoundManager.Instance.PlayDelayed (1, 0.2f, gameObject);
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}

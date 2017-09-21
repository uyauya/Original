using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerShoot04 : MonoBehaviour {

	public GameObject Bullet04;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float speed = 1000F;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	public float Attack;
	public float damage = 100;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet01 bullet04_script;
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
			if (Input.GetButtonUp ("Fire4")) {
				damage = Attack;
				if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
					animator.SetTrigger ("Shot");
					GetComponent<PlayerController> ().boostPoint -= BpDown;
					Bullet ();
				}
			}
		}
	}
		//マズルフラッシュを表示する
		//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		
		//音を重ねて再生する
		//audioSource.PlayOneShot(audioSource.clip);

	//}
	
	void Bullet() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet04)as GameObject;
			bulletObject.transform.position = muzzle.position + transform.TransformDirection(Vector3.forward * 2) + new Vector3(0, -0.3f, 0);
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(18,gameObject);
				SoundManager.Instance.PlayDelayed (19, 0.2f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(20,gameObject);
				SoundManager.Instance.PlayDelayed (21, 0.2f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(22,gameObject);
				SoundManager.Instance.PlayDelayed (23, 0.2f, gameObject);
			}
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}

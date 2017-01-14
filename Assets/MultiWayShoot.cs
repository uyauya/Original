using UnityEngine;
using System.Collections;

public class MultiWayShoot : MonoBehaviour {

	public GameObject MultiWayBullet;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float speed = 10F;
	public float interval = 0.5F;
	private float time = 0F;
	private float Attack = 500;
	private float power = 0;
	public float damage;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	MultiWayBullet multiWayBullet_script;
	
	void Start () {
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
	}
	
	void Update () {
		if (Input.GetButton("Fire3")) {
			animator.SetTrigger ("Shotss");
			if (time >= interval) {	
				time = 0f;
			}
			MultiWayBullet();
			//マズルフラッシュを表示する
			//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
		}
		//音を重ねて再生する
		audioSource.PlayOneShot(audioSource.clip);
	}
	
	
	void Bulletss() {
		GameObject bulletObject = GameObject.Instantiate (MultiWayBullet)as GameObject;
		bulletObject.transform.position = muzzle.position;
		bulletObject.GetComponent<MultiWayBullet> ().damage = this.damage;
	}
}

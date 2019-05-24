using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// 足場兼壁
public class PlayerShoot04R : MonoBehaviour {

	public GameObject Bullet04R;
	public GameObject UBullet04R;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed = 1000F;
	//public float Attack;
	//public float damage = 100;
	public float shotInterval;			
	public float shotIntervalMax = 0.25F;
	private float chargeTime;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet01 bullet04_script;
	public int BpDown = 100;
	public int PlayerNo;
	private Pause pause;
	public bool isBig;							// 巨大化しているかどうか
	//public BattleManager battleManager;


	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		GameObject MuzzleFlash = GameObject.Find("MuzzleFlash");
		Transform muzzle = GameObject.FindWithTag ("Player").transform.Find("muzzle");
	}

	void Update () {
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			//isBig = battleManager.Player.GetComponent<PlayerAp>().isBig;
			if (isBig == false) {
				if (Input.GetButtonUp ("Fire1")) {
					if (DataManager.Level >= PlayerLevel.PSoot04Level){
						//damage = Attack;
						if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
							animator.SetTrigger ("Shot");
							GetComponent<PlayerController> ().boostPoint -= BpDown;
							Bullet ();
						}
					}
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
		if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 1)|| (DataManager.PlayerNo == 2)) 
		{
			if (Time.time - shotInterval > shotIntervalMax) {
				shotInterval = Time.time;
				GameObject bulletObject = GameObject.Instantiate (Bullet04R)as GameObject;
				bulletObject.transform.position = muzzle.position + transform.TransformDirection(Vector3.forward * 2) 
					+ new Vector3(0, -0.3f, 0);
			}
		}
		else if (DataManager.PlayerNo == 3)
		{
			if (Time.time - shotInterval > shotIntervalMax) {
				shotInterval = Time.time;
				GameObject bulletObject = GameObject.Instantiate (UBullet04R)as GameObject;
				bulletObject.transform.position = muzzle.position + transform.TransformDirection(Vector3.forward * 2) 
					+ new Vector3(0, -0.3f, 0);
			}
		}
		if((PlayerNo == 0) || (PlayerNo == 3)){
			SoundManager.Instance.Play(9,gameObject);
			SoundManager2.Instance.PlayDelayed (3, 0.2f, gameObject);
		}
		if (PlayerNo == 1) {
			SoundManager.Instance.Play(10,gameObject);
			SoundManager2.Instance.PlayDelayed (3, 0.2f, gameObject);
		}
		if (PlayerNo == 2) {
			SoundManager.Instance.Play(11,gameObject);	
			SoundManager2.Instance.PlayDelayed (3, 0.2f, gameObject);
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}


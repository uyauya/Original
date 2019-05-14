using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// ボム（シューティングなどの広範囲攻撃）
public class PlayerShoot03R : MonoBehaviour {

	public GameObject Bullet03R;
	public GameObject UBullet03R;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float Attack;
	public float damage = 1000;
	public Image gaugeImage;
	public int boostPoint;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet03 bullet03_script;
	public int BpDown = 1000;
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
		GameObject Bullet03 = GameObject.Find("Shotss");
		GameObject UBullet03 = GameObject.Find("UShotss");
		GameObject MuzzleFlash = GameObject.Find("MuzzleFlash");
		Transform muzzle = GameObject.FindWithTag ("Player").transform.Find("muzzle");
	}

	void Update () {
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			//isBig = battleManager.Player.GetComponent<PlayerAp>().isBig;
			if (isBig == false) {
				if (Input.GetButtonUp ("Fire1")) {
					if (DataManager.Level >= PlayerLevel.PSoot03Level){
						damage = Attack;
						if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
							animator.SetTrigger ("Shot");
							GetComponent<PlayerController> ().boostPoint -= BpDown;
							Bulletss ();
							//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
						}
					}
				}
			}
		}
	}

	void Bulletss() {
		if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 1)|| (DataManager.PlayerNo == 2)) 
		{
			if (Time.time - shotInterval > shotIntervalMax) {
				shotInterval = Time.time;
				GameObject bulletObject = GameObject.Instantiate (Bullet03R)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet03> ().damage = this.damage;
			}
		}
		else if (DataManager.PlayerNo == 0)
		{
			if (Time.time - shotInterval > shotIntervalMax) {
				shotInterval = Time.time;
				GameObject bulletObject = GameObject.Instantiate (UBullet03R)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet03> ().damage = this.damage;
			}
		}

		if ((PlayerNo == 0) || (PlayerNo == 3))
		{
			SoundManager.Instance.Play(6,gameObject);
			SoundManager2.Instance.PlayDelayed (2, 0.2f, gameObject);
		}
		if (PlayerNo == 1) {
			SoundManager.Instance.Play(7,gameObject);
			SoundManager2.Instance.PlayDelayed (2, 0.2f, gameObject);
		}
		if (PlayerNo == 2) {
			SoundManager.Instance.Play(8,gameObject);	
			SoundManager2.Instance.PlayDelayed (2, 0.2f, gameObject);
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}



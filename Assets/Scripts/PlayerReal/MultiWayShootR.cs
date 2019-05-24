using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// 多段同時ショット
public class MultiWayShootR : MonoBehaviour {

	public GameObject Bullet05R;
	public GameObject UBullet05R;
	private GameObject bullet05;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float interval = 0.5F;
	public float shotInterval;					
	public float shotIntervalMax = 0.25F;
	private float Attack;
	public float attackPoint;					
	public float damage = 300.0f;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet05 bullet05_script;
	public int BulletGap = 15;					
	public float BulletRad = 1;					
	public int BulletNumber = 5;				
	public int FirstBullet = -4;				
	public int BpDown = 200;
	public int PlayerNo;
	private Pause pause;
	private int timeCount;
	public bool isBig;							
	//public BattleManager battleManager;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		attackPoint = DataManager.AttackPoint;
		GameObject Bullet05 = GameObject.Find("ShotM");
		GameObject UBullet05 = GameObject.Find("UShotM");
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
				shotInterval += Time.deltaTime;
				if (Input.GetButtonUp ("Fire1")) {
					if (DataManager.Level >= PlayerLevel.PMSootLevel){
						if (GetComponent<PlayerController> ().boostPoint >= BpDown)
						if (timeCount % 5 == 0) {
							Bullet ();
						}
						damage = Attack + attackPoint;
						animator.SetTrigger ("Shot");
						//マズルフラッシュを表示する
						//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
					}
				}
				//音を重ねて再生する
				//audioSource.PlayOneShot (audioSource.clip);
			}
		}
	}
		
	void Bullet ()
	{
		if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 1)|| (DataManager.PlayerNo == 2)) 
		{
			if(GetComponent<PlayerController> ().boostPoint >= BpDown)
				for (int i = FirstBullet; i < BulletNumber; i++) {
					float rad = BulletRad;
					Vector3 pos = new Vector3 (
						rad * Mathf.Sin (Mathf.Deg2Rad * (i * BulletGap)),
						0,
						rad * Mathf.Cos (Mathf.Deg2Rad * (i * BulletGap))
					);
					GameObject bulletObject = Instantiate (Bullet05R);			
					bulletObject.transform.position = transform.TransformPoint (pos);
					bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
					bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);
					GetComponent<PlayerController> ().boostPoint -= BpDown;
					bulletObject.GetComponent<Bullet05R> ().damage = this.damage;
				}
		}
		else if(DataManager.PlayerNo == 3)
		{
			if(GetComponent<PlayerController> ().boostPoint >= BpDown)
				for (int i = FirstBullet; i < BulletNumber; i++) {
					float rad = BulletRad;
					Vector3 pos = new Vector3 (
						rad * Mathf.Sin (Mathf.Deg2Rad * (i * BulletGap)),
						0,
						rad * Mathf.Cos (Mathf.Deg2Rad * (i * BulletGap))
					);
					GameObject bulletObject = Instantiate (UBullet05R);			
					bulletObject.transform.position = transform.TransformPoint (pos);
					bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
					bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);
					GetComponent<PlayerController> ().boostPoint -= BpDown;
					bulletObject.GetComponent<Bullet05R> ().damage = this.damage;
				}
		}
		if ((PlayerNo == 0)|| (PlayerNo == 3)){
			SoundManager.Instance.Play(21,gameObject);
			SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
		}
		if (PlayerNo == 1) {
			SoundManager.Instance.Play(22,gameObject);	
			SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
		}
		if (PlayerNo == 2) {
			SoundManager.Instance.Play(23,gameObject);	
			SoundManager2.Instance.PlayDelayed (4, 0.2f, gameObject);
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}

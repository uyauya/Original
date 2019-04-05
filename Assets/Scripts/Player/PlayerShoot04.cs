using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// 足場兼壁
public class PlayerShoot04 : MonoBehaviour {

	public GameObject Bullet04;
	public Transform muzzle;
	public GameObject muzzleFlash;
	//public float speed = 1000F;
	//public float Attack;
	//public float damage = 100;
	public float shotInterval;			// ショットの時間間隔
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
	
	/*[CustomEditor(typeof(PlayerShoot04))]
	public class PlayerShoot04Editor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			PlayerShoot04 Ps04 = target as PlayerShoot03;
			Ps04.shotIntervalMax = EditorGUILayout.FloatField( "ショット間隔", Ps04.shotIntervalMax);
			//Ps04.Damage	   		 = EditorGUILayout.FloatField( "攻撃力", Ps04.Damage);
			Ps04.BpDown		     = EditorGUILayout.FloatField( "ゲージ消費量", Ps04.BpDown);
		}
	}*/

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
	}
	
	void Update () {
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
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
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet04)as GameObject;
			// 弾生成場所をmuzzleの前方２、下0.3の場所に生成する
			bulletObject.transform.position = muzzle.position + transform.TransformDirection(Vector3.forward * 2) 
				+ new Vector3(0, -0.3f, 0);
			if (PlayerNo == 0) {
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
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}

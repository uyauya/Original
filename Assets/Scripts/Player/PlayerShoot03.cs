using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// ボム（シューティングなどの広範囲攻撃）
public class PlayerShoot03 : MonoBehaviour {
	
	public GameObject Bullet03;
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
	
	/*[CustomEditor(typeof(PlayerShoot03))]
	public class PlayerShoot03Editor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			PlayerShoot03 Ps03 = target as PlayerShoot03;
			Ps03.shotIntervalMax = EditorGUILayout.FloatField( "ショット間隔", Ps03.shotIntervalMax);
			Ps03.Damage	   		 = EditorGUILayout.FloatField( "攻撃力", Ps03.Damage);
			Ps03.BpDown		     = EditorGUILayout.FloatField( "ゲージ消費量", Ps03.BpDown);
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
					if (DataManager.Level >= PlayerLevel.PSoot03Level){
					damage = Attack;
					if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
						animator.SetTrigger ("Shot");
						GetComponent<PlayerController> ().boostPoint -= BpDown;
						Bulletss ();
						//マズルフラッシュを表示する
						//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
						}
					}
				}
			}
		}
	}

	void Bulletss() {
		// ショットの時間間隔
		if (Time.time - shotInterval > shotIntervalMax) {
			shotInterval = Time.time;
			GameObject bulletObject = GameObject.Instantiate (Bullet03)as GameObject;
			bulletObject.transform.position = muzzle.position;
            //bulletObject.transform.position = Bomber.position;
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
			bulletObject.GetComponent<Bullet03> ().damage = this.damage;
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}


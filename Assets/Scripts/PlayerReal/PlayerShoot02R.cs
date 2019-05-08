using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// ホーミングショット
public class PlayerShoot02R : MonoBehaviour {

	public GameObject Bullet02;
	public GameObject UBullet02;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float shotInterval;			// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float Attack;
	public float damage = 200;
	public Image gaugeImage;
	public int boostPoint;
	public int attackPoint;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	Bullet02 bullet02_script;
	public int BpDown = 100;
	public int PlayerNo;
	private Pause pause;
	public bool isBig;							// 巨大化しているかどうか
	//public BattleManager battleManager;

	/*[CustomEditor(typeof(PlayerShoot02))]
	public class PlayerShoot02Editor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			PlayerShoot02 Ps02 = target as PlayerShoot02;
			Ps02.shotIntervalMax = EditorGUILayout.FloatField( "ショット間隔", Ps02.shotIntervalMax);
			Ps02.Damage	   		 = EditorGUILayout.FloatField( "攻撃力", Ps02.Damage);
			Ps02.BpDown		     = EditorGUILayout.FloatField( "ゲージ消費量", Ps02.BpDown);
		}
	}*/

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		attackPoint = DataManager.AttackPoint;
		GameObject Bullet02 = GameObject.Find("ShotssR");
		GameObject UBullet02 = GameObject.Find("UShotssR");
		GameObject MuzzleFlash = GameObject.Find("MuzzleFlash");
		Transform muzzle = GameObject.FindWithTag ("Player").transform.Find("muzzle");
	}

	void Update () {
		float boostpoint = GetComponent<PlayerController> ().boostPoint;
		int Attackpoint = DataManager.AttackPoint;
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			//isBig = battleManager.Player.GetComponent<PlayerAp>().isBig;
			if (isBig == false) {
				if (Input.GetButtonUp ("Fire1")) {
					if (DataManager.Level >= PlayerLevel.PSoot02Level)
					{
						//Debug.Log("レベル" + DataManager.Level);
						if (GetComponent<PlayerController>().boostPoint >= BpDown)
						{
							damage = Attack += attackPoint;
							animator.SetTrigger("Shot");
							GetComponent<PlayerController>().boostPoint -= BpDown;
							Bullets();
						}
					}
					//マズルフラッシュを表示する
					//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
				}
			}
		}
	}

	void Bullets() {

		if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 1)|| (DataManager.PlayerNo == 2)) 
		{
			// ショットの時間間隔
			if (Time.time - shotInterval > shotIntervalMax)
			{
				shotInterval = Time.time;
				GameObject bulletObject = GameObject.Instantiate (Bullet02)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet02> ().damage = this.damage;
			}
		}
		else if (DataManager.PlayerNo == 3)
		{
			// ショットの時間間隔
			if (Time.time - shotInterval > shotIntervalMax)
			{
				shotInterval = Time.time;
				GameObject bulletObject = GameObject.Instantiate (UBullet02)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet02> ().damage = this.damage;
			}
		}

		if ((PlayerNo == 0)|| (PlayerNo == 3))
		{
			SoundManager.Instance.Play(3,gameObject);
			//SoundManagerKohaku.Instance.Play(1,gameObject);
			SoundManager2.Instance.PlayDelayed (1, 0.2f, gameObject);
		}
		if (PlayerNo == 1) 
		{
			SoundManager.Instance.Play(4,gameObject);
			//SoundManagerYuko.Instance.Play(1,gameObject);	
			SoundManager2.Instance.PlayDelayed (1, 0.2f, gameObject);
		}
		if (PlayerNo == 2) 
		{
			SoundManager.Instance.Play(5,gameObject);
			//SoundManagerMisaki.Instance.Play(1,gameObject);	
			SoundManager2.Instance.PlayDelayed (1, 0.2f, gameObject);
		}
	}

	public void KickEvent (){
		Debug.Log("kick");
	}

}


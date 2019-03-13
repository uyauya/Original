using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEditor;

// 多段同時ショット
public class MultiWayShoot : MonoBehaviour {

	public GameObject Bullet05;
	private GameObject bullet05;
	public Transform muzzle;
	public GameObject muzzleFlash;
	public float interval = 0.5F;
	public float shotInterval;					// ショットの時間間隔
	public float shotIntervalMax = 0.25F;
	private float Attack;
	public float attackPoint;					// プレイヤの攻撃値（ショットする際に付け足す。PlayerController参照）
	public float damage = 2000;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;
	Bullet05 bullet05_script;
	public int BulletGap = 15;					// 弾の発射角度
	public float BulletRad = 1;					// 弾の発射元（プレイヤから発射もとまでの距離）
	public int BulletNumber = 5;				// 弾生成（～まで）
	public int FirstBullet = -4;				// 弾生成（～から）
	public int BpDown;
	public int PlayerNo;
	private Pause pause;
	private int timeCount;
	public bool isBig;							// 巨大化しているかどうか

	/*[CustomEditor(typeof(MultiWayShoot))]
	public class MultiWayShootEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			MultiWayShoot Mws = target as MultiWayShoot;
			Mws.shotIntervalMax = EditorGUILayout.FloatField( "ショット間隔", Mws.shotIntervalMax);
			Mws.Damage	   		= EditorGUILayout.FloatField( "攻撃力", Mws.Damage);
			Mws.BpDown		    = EditorGUILayout.FloatField( "ゲージ消費量", Mws.BpDown);
			Mws.BulletGap		= EditorGUILayout.IntField( "弾の発射角度", Mws.BulletGap);
			Mws.BulletRad		= EditorGUILayout.FloatField( "弾の発射元", Mws.BulletRad);
			Mws.FirstBullet		= EditorGUILayout.IntField(" 弾生成（～から）", Mws.FirstBullet);
			Mws.BulletNumbaer	= EditorGUILayout.IntField(" 弾生成（～まで）", Mws.BulletNumbaer);
		}
	}*/

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		attackPoint = DataManager.AttackPoint;
	}

	void Update () {
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
			if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			if (isBig == false) {
				//timeCount += 1;
				// Fire1（標準ではCtrlキー)を押された瞬間.
				shotInterval += Time.deltaTime;
				if (Input.GetButtonUp ("Fire1")) {
					if (GetComponent<PlayerController> ().boostPoint >= BpDown)
					//effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
				//経過時間を５で割って余りが0の時にBullet(下記参照）発動
				if (timeCount % 5 == 0) {
						Bullet ();
					}
					damage = Attack + attackPoint;
					animator.SetTrigger ("Shot");
					//マズルフラッシュを表示する
					//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
				}
				//音を重ねて再生する
				//audioSource.PlayOneShot (audioSource.clip);
			}
		}
	}

	// Bullet(弾丸)スクリプトに受け渡す為の処理
	void Bullet ()
	{
		if(GetComponent<PlayerController> ().boostPoint >= BpDown)
		// FirstBulletからBulletNumberまでの数を弾生成数（i）とする
		for (int i = FirstBullet; i < BulletNumber; i++) {
			//Debug.Log (i);
			//弾配置場所(弾の生成元からの半径)
			float rad = BulletRad;
			//真ん中（０）を中心にBulletGap（角度）間隔に配置。配置場所をposとする　
			Vector3 pos = new Vector3 (
					     rad * Mathf.Sin (Mathf.Deg2Rad * (i * BulletGap)),
					     0,
					     rad * Mathf.Cos (Mathf.Deg2Rad * (i * BulletGap))
				         );
			// Bullet05の付いたオブジェクトを生成
			GameObject bulletObject = Instantiate (Bullet05);			
			// 弾を（posの場所に）配置
			bulletObject.transform.position = transform.TransformPoint (pos);
			// 回転を設定（弾を拡散するよう回転させて振り分けて配置）
			bulletObject.transform.rotation = Quaternion.LookRotation (bulletObject.transform.position - transform.position);
			// 回転計算をした後に弾の座標をnew Vector3(0,1,0)で上に上げる
			bulletObject.transform.position = bulletObject.transform.position + new Vector3(0,1,0);
			if (PlayerNo == 0) { 
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
			// 	ブーストポイントをBpDown分消費
			GetComponent<PlayerController> ().boostPoint -= BpDown;
			// bulletObjectのオブジェクトにダメージ計算を渡す
			bulletObject.GetComponent<Bullet05> ().damage = this.damage;
			}

	}

	public void KickEvent (){
		Debug.Log("kick");
	}
}
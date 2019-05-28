using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// プレイヤー用ガード（レバー進行方向2回押しでガード壁出し）
// レバー進行方向2回押し＋Fire3でエネルギー吸収（回復）壁出し
public class DiffenceAbsorb : MonoBehaviour {

	public GameObject DiffenceWall;				// ガード用壁
	public GameObject AbsorbWall;				// エネルギー吸収用壁
	public Transform muzzle;					// 壁発射元
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	private Vector3 velocity = Vector3.zero;
	private Vector3 input = Vector3.zero;
	public bool diffence = false;        	  	//　ガードしているか
	public bool push = false;          		  	//　最初に移動ボタンを押したかどうか
	public float NextButtonDownTime = 0.2f;     //　次に移動ボタンが押されるまでの時間
	private float nowTime = 0f;         	  	//　最初に移動ボタンが押されてからの経過時間
	public float LimitAngle = 15.0f;            //　最初に押した方向との違いの限度角度
	private Vector2 direction = Vector2.zero;   //　移動キーの押した方向
	private Pause pause;
	public int PlayerNo;
	public int boostPoint;						// ブーストポイント
	public int BpUp = 100;						// エネルギー吸収した際の回復ポイント
	public Transform EffectPoint;				// 回復等エフェクト発生元の位置取り
	public GameObject BpHealPrefab;				// ブーストポイント回復エフェクト格納場所
	public GameObject BpHealObject;

	/*[CustomEditor(typeof(DiffenceAbsorb))]
	public class DiffenceAbsorbEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			DiffenceAbsorb Da = target as DiffenceAbsorb;
			Da.NextButtonDownTime = EditorGUILayout.FloatField( "次にボタンが押されるまでの時間", Da.NextButtonDownTime);
			Da.LimitAngle 		  = EditorGUILayout.FloatField( "入力角度誤差", Da.LimitAngle);
			Da.BpUp				  = EditorGUILayout.FloatField( "ブーストポイント回復値", Da.BpUp);
		}
	}*/

	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator>();			// Animatorを使う場合は設定する
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		GameObject DiffenceWall = GameObject.Find("DiffenceWall");	
		GameObject AbsorbWall = GameObject.Find("AbsorbWall");
		GameObject BpHealPrefab = GameObject.Find("Aura2");
		Transform muzzle = GameObject.FindWithTag ("Player").transform.Find("muzzle");
		Transform EffectPoint = GameObject.FindWithTag ("Player").transform.Find("EffectPoint");
	}


	void Update()

	{
		// レバーニュートラル設定
		velocity = Vector3.zero;
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
			//　ガードしていないとき
			if (!diffence) {
				//　移動キーを押した
				if ((Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical"))) {
					//　最初に1回押していない時は押した事にする（レバー押し1回目）
					if (!push) {
						push = true;
						//Debug.Log("カベ1");
						//　最初に移動キーを押した時にその方向ベクトルを取得
						direction = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
						nowTime = 0f;
						//　2回目のボタンだったら1→２までの制限時間内だったらガード
					} else if ((Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical"))) {
						//　2回目に移動キーを押した時の方向ベクトルを取得
						var nowDirection = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
						//Debug.LogFormat("Vector2.Angle:{0} LimitAngle:{1} Time.time:{2} nowTime:{3} nextButtonDownTime:{4}",Vector2.Angle(nowDirection, direction),limitAngle,Time.time,nowTime,nextButtonDownTime);
						//　押した方向がリミットの角度を越えていない　かつ　制限時間内に移動キーが押されていればガード
						if (Vector2.Angle (nowDirection, direction) < LimitAngle
						   && nowTime <= NextButtonDownTime) {							
							//Debug.LogFormat ("出る時：Vector2.Angle:{0} LimitAngle:{1} Time.time:{2} nowTime:{3} nextButtonDownTime:{4}", Vector2.Angle (nowDirection, direction), limitAngle, Time.time, nowTime, nextButtonDownTime);
							diffence = true;
							//Debug.Log("カベ2");
							if (PlayerNo == 0) {
								//SoundManagerKohaku.Instance.Play(0,gameObject);
							}
							if (PlayerNo == 1) {
								//SoundManagerYuko.Instance.Play(0,gameObject);
							}
							if (PlayerNo == 2) {
								//SoundManagerMisaki.Instance.Play(0,gameObject);	
							}
							//Diffencer ();
							Debug.Log ("Diffence");
							diffence = false;
							push = false;
						} else if (nowTime > NextButtonDownTime) {
							diffence = false;
							push = false;
						}

						if (Vector2.Angle (nowDirection, direction) < LimitAngle && (Input.GetButton ("Fire1"))
						   && nowTime <= NextButtonDownTime) {
							diffence = true;
							if ((PlayerNo == 0) || (PlayerNo == 3)){
								//SoundManager.Instance.Play(0,gameObject);
							}
							if (PlayerNo == 1) {
								//SoundManager.Instance.Play(1,gameObject);
							}
							if (PlayerNo == 2) {
								//SoundManager.Instance.Play(2,gameObject);
							}
							Absorb ();
							Debug.Log ("Absorb");
							diffence = false;
							push = false;
						} else if (nowTime > NextButtonDownTime) {
							diffence = false;
							push = false;
						}
					} else {
					}
				}
			}

			//　最初の移動キーを押していれば時間計測
			//　最初の移動キーを押していれば時間計測
			if (push) {
				//　時間計測
				nowTime += Time.deltaTime;

				if (nowTime > NextButtonDownTime) {
					push = false;
				}
			}

			input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		}
	}

	/*void Diffencer() {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject diffenceObject = GameObject.Instantiate (DiffenceWall)as GameObject;
		//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
		diffenceObject.transform.position = muzzle.position;
	}*/
	void Absorb() {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject absorbObject = GameObject.Instantiate (AbsorbWall)as GameObject;
		// Diffencerと重ならないようAbsorbを少し前に置く
		absorbObject.transform.position = muzzle.position + new Vector3 (0, 0, 0.2f);
	}

	private void OnCollisionEnter (Collision collider)
	{
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "ShotEnemy") {
			BpHealObject = Instantiate (BpHealPrefab, EffectPoint.position, Quaternion.identity);
			BpHealObject.transform.SetParent (EffectPoint);
			//animator.SetTrigger ("Absorb");
			if ((PlayerNo == 0) || (PlayerNo == 3)){
				//SoundManager.Instance.Play (18, gameObject);
			}
			if (PlayerNo == 1) {
				//SoundManager.Instance.Play (19, gameObject);
			}
			if (PlayerNo == 2) {
				//SoundManager.Instance.Play (20, gameObject);
			}
			boostPoint += BpUp;
		}
	}
} 

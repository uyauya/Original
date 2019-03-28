using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//前方に瞬間移動　+　瞬間移動後に特殊攻撃
public class FullDash : MonoBehaviour {

	public Transform muzzle;					//DashAttackプレハブ発生元
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	private Vector3 velocity = Vector3.zero;
	private Vector3 input = Vector3.zero;
	public bool push = false;          		  	//最初に移動ボタンを押したかどうか
	public float NextButtonDownTime;    	  	//次に移動ボタンが押されるまでの制限時間
	private float nowTime = 0f;         	  	//最初に移動ボタンが押されてからの経過時間
	public float LimitAngle;            	  	//最初に押した方向との違いの限界角度
	private Vector2 direction = Vector2.zero;   //移動キーの押した方向
	private Pause pause;
	public int PlayerNo;
	public int boostPoint;						//ブーストポイント
	public Transform EffectPoint;				//エフェクト発生元の位置取り
	public GameObject DashAttck;	
	public GameObject DAEffectPrefab;				
	public GameObject DAEffectObject;
	public static bool isDash = false;        	//ダッシュしているか
	public float DangerAp = 500.0f;				//ダッシュ出来るようになるHP
	public float DashDistanceX = 2.0f;			//ダッシュする横軸距離
	public float DashDistanceZ = 2.0f;			//ダッシュする縦軸距離

	public float DashDistance = 2.0f;			//瞬間移動距離
	private ModelColorChange modelColorChange;	

	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
	}


	void Update()
	{
		// レバーニュートラル設定
		velocity = Vector3.zero;
		// ポーズ中でなく、ステージクリア時でもなく、ストップ条件もなければ
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		//ポーズ中でなくプレイヤーのHPがDangerAp)以下なら
		if ((pause.isPause == false) && (PlayerAp.armorPoint <= DangerAp)) {
			//　ガードしていないとき
			if (!isDash) {
				//　移動キーを押した
				if ((Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical"))) {
					//　最初に1回押していない時は押した事にする（レバー押し1回目）
					if (!push) {
						push = true;
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
							isDash = true;
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
							//Dash ();
							isDash = true;
							Debug.Log ("Dash");
							isDash = false;
							push = false;
						} else if (nowTime > NextButtonDownTime) {
							isDash = false;
							push = false;
						}

					if (Vector2.Angle (nowDirection, direction) < LimitAngle && (Input.GetButton ("Fire1"))
						&& nowTime <= NextButtonDownTime) {
						isDash = true;
						if (PlayerNo == 0) {
						//SoundManager.Instance.Play(0,gameObject);
						}
						if (PlayerNo == 1) {
						//SoundManager.Instance.Play(1,gameObject);
						}
						if (PlayerNo == 2) {
						//SoundManager.Instance.Play(2,gameObject);
						}
						DashAttack ();
						Debug.Log ("DashAttack");
						isDash = false;
						push = false;
					} else if (nowTime > NextButtonDownTime) {
						isDash = false;
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
            //transfor.position += transform.forward * 2.0f;
		}
	}

	//現在地より進行方向(direction側)より数歩先に瞬間移動。敵との接触は無効とする
	void Dash() {
		gameObject.layer = LayerMask.NameToLayer("Invincible");
		//transform.position = new Vector3(direction * DashDistanceX, 0, direction * DashDistanceZ);
		gameObject.layer = LayerMask.NameToLayer("Player");
	}

	//ダッシュ直後に攻撃。攻撃＋BP吸収
	void DashAttack() {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject dattack = GameObject.Instantiate (DashAttck)as GameObject;
		//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
		dattack.transform.position = muzzle.position;
	}


} 

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor; 

// プレイヤーの移動、ジャンプ、アイテムゲット（ブースト回復、ステージクリア用）
public class PlayerController : MonoBehaviour {

	private Animator animator;
	public float Force= 8;							// 移動速度
	public float MaxForce = 10;						// 移動速度最大値
	public float MaxBoostForce = 15;				// ブースト時の移動速度最大値
	public float PlusForce= 0.1f;					// 移動速度加算数値
	public float jumpSpeed;							// ジャンプ力
	public float HighPoint;							// ジャンプの高さ最大値
	public float gravity;							// 重力（ジャンプ時などに影響）
	private Vector3 moveDirection = Vector3.zero;	 //プレイヤ位置方向ニュートラル設定
	public float boostPoint;						// ブーストポイント
	public float displayBoostPoint;					// ブーストポイント（画面表示用）
	public int BpDown = 20;							// ブーストゲージ消費値
	public float RecoverPoint = 0.2f;				// ブーストポイント回復値
	public GameObject gaugeImage;						// ブーストゲージ（画面表示用）
	public Text boostText;							// ブースト最大・現在数値（画面表示用）
	Vector3 moveSpeed;								// プレイヤの速さ
	private int JumpCount;							// ジャンプ回数計算用（二段ジャンプ処理に使用）
	private float interval = 2.0f;
	bool isBoost;									// ブーストボタンをオン・オフ設定
	private float timer = 0.0f;				
	bool onFloor = true;							// 床に設置しているかどうか
	public int ItemCount;							// スフィア取得個数計算用
	public int GetStar = 0;
	public int GetBigStar = 0;
	Vector3 targetSpeed = Vector3.zero; 			// 目標速度
	Vector3 addSpeed = Vector3.zero;    			// 加算速度
	public GameObject BpHealEffect;					// ブーストポイント回復アイテム取得時のエフェクト
	public int PlayerNo;							//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）
	public Transform EffectPoint;					// 回復等エフェクト発生元の位置取り
	public GameObject BpHealPrefab;					// ブーストポイント回復エフェクト格納場所
	public GameObject BpHealObject;
	public float BpHealPoint = 500;					// ブーストポイント回復値（アイテム取得時）
	protected bool isInvincible;					// 無敵処理（ダメージ受けた際に使用）
	private ModelColorChange modelColorChange;
	public float InvincibleTime;					// 無敵時間

	/*[CustomEditor(typeof(PlayerController))]
	public class PlayerControllerEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;
		
			public override void OnInspectorGUI()
			{
			PlayerController PL= target as PlayerController;
				PL.boostPointMax = EditorGUILayout.IntField( "最大ブーストポイント", PL.boostPointMax);
				PL.BpDown = EditorGUILayout.IntField( "ブーストポイント消費量", PL.BpDown);
				PL.RecoverPoint = EditorGUILayout.IntField( "ブーストポイント回復量", PL.RecoverPoint);
				PL.MaxForce = EditorGUILayout.FloatField( "移動力", PL.MaxForce);
				PL.MaxBoostForce = EditorGUILayout.FloatField( "ブースト時移動力", PL.MaxBoostForce);
				PL.jumpSpeed = EditorGUILayout.FloatField( "ジャンプ力", PL.jumpSpeed);	
				PL.HighPoint = EditorGUILayout.FloatField( "ジャンプ高さ上限", PL.HighPoint);	
				PL.gravity = EditorGUILayout.FloatField( "重力", PL.gravity);
			}
	}*/

	void Start()	//　ゲーム開始時の設定
	{
		animator = GetComponent<Animator>();		// Animatorを使う場合は設定する
		boostPoint = DataManager.BoostPointMax;		// ブーストポイントを最大値に設定
		moveSpeed = Vector3.zero;					// 開始時は移動していないので速さはゼロに
		isBoost = false;							// ブーストはオフに
		// Canvas上のゲージイメージを取得（オブジェクトに直接付いていない場合はゲットコンポーネントで取得する）
		// BoostGaugeオブジェクトに付いているImageを取得
		//gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		gaugeImage = GameObject.Find ("BoostGauge");
		boostText = GameObject.Find ("TextBg").GetComponent<Text> ();
		// 画面上(Canvas)のブーストポイントと実際(Inspector)の数値(Inspector)を同じに設定
		displayBoostPoint = boostPoint;
		if (!DataManager.FarstLevel) {
		}
		// プレイヤーのレイヤーをPlayerに設定
		gameObject.layer = LayerMask.NameToLayer("Player");
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
	}

	void Update()
	{
		//現在のブーストゲージと最大ブーストゲージをUI Textに表示する
		//boostText.text = string.Format("{0:0000} / {1:0000}", displayBoostPoint, DataManager.BoostPointMax);
		boostText.text = string.Format("{0:0000} / {1:0000}", boostPoint, DataManager.BoostPointMax);
	}

	void FixedUpdate()
	{

		//ブーストボタンが押されてブーストポイント残が1以上あればフラグを立てブーストポイントを消費
		if (Input.GetButton("Boost") && boostPoint > 0)
		{
			boostPoint -= BpDown;							//ブーストポイントをBpDown設定値分消費
			isBoost = true;									//ブースト状態にする
			StartCoroutine ("BoostCoroutine");
			// プレイヤのレイヤーをInvincibleに変更
			// Edit→ProjectSetting→Tags and LayersでInvicibleを追加
			// Edit→ProjectSetting→Physicsで衝突させたくない対象と交差している所の✔を外す
			// ここではEnemyと衝突させたくない（すり抜ける）為、Enemeyのレイヤーも追加
			// EnemeyとPlayerの交差してる✔を外す（プレイヤのLayerをPlayer、EnemyのLayerをEnemyに設定しておく）
			//gameObject.layer = LayerMask.NameToLayer("Invincible");
			StartCoroutine ("BoostCoroutine");
		}
		else
		{
			isBoost = false;								//それ以外ならブーストなし（通常状態）
			//gameObject.layer = LayerMask.NameToLayer("Player");
		}

		//通常時とブースト時で変化
		if (isBoost)										//ブーストなら
		{
			// ブースト時
			if (Force <= MaxBoostForce) {					// ForceがMaxBoostForceの値以下なら
				MaxForce += Time.deltaTime * PlusForce;		// MaxBoostForceまでMaxForce(通常最大速度)に加速
				Force = Mathf.Min(Force, MaxBoostForce);	// ForceがMaxBoostForceの値を超えない
				//Debug.Log (Force);
			}
			//ブーストキーが押されたらにパラメータを切り替える
			animator.SetBool("Boost", Input.GetButton("Boost")&& boostPoint > 0);
		}
		else
		{
			if (Force <= MaxForce) {					
				Force += Time.deltaTime * PlusForce;	
				Force = Mathf.Min(Force, MaxForce);		
				//Debug.Log (Force);
			}
			animator.SetBool("Boost", Input.GetButton("Boost")&& boostPoint > 0);
		}
			

		//モーションを切り替える
		if (Input.GetAxis("Horizontal") > 0)	// 横軸操作（右か左か押されている場合）
		{
			// クォータリオン（球体を動かすような処理）で5.0の速度でプレイヤを横に向かせる
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5.0f);
			// アニメーターをムーブに切り替え
			animator.SetBool("Move", true);
			// プレイヤに速度を加える（transform.Translateは移動だが、アッドフォースは後ろから押すような操作なので、坂道など段差が
			// ある場合、自動で加減速処理して移動する
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
			//Debug.Log (Force);
		}
		else if (Input.GetAxis("Horizontal") < 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
			//Debug.Log (Force);

		}
		else if (Input.GetAxis("Vertical") > 0)	// 縦軸操作（前か後か押されている場合）
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);

		}
		else if (Input.GetAxis("Vertical") < 0)
		{
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * 5.0f);
			animator.SetBool("Move", true);
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);

		}
		else
		{
			// 何も押されていないニュートラル状態で
			animator.SetBool("Move", false);
			// アッドフォースされた速度が0でなければフォースにマイナス処理して減速（滑り止め）
			// プレイヤの滑り具合がグラビティを変えることによって調節できるが、変更すると重い軽いでジャンプなどにも影響が出てくる
			// Edit→Project Settings→Physicsで全体的な重力は変えられるがインスペクタ上でGravity変更した方がよい
			//if(Force != 0){ Force -= 1.2f; }
			if(Force >= MaxForce){ Force -= 2.2f; }
		}

		//ジャンプキーによる上昇（二段ジャンプ）
		//ジャンプが押されて1回目なら（2回目でないなら）（一番下のコライダー処理が関係してる）
		if (Input.GetButtonDown("Jump") == true && JumpCount < 2 ) {
			// ジャンプ数加算
			JumpCount++;

			// ジャンプの上昇力を設定( v.x, 4, v.z )で縦方向に加算
			Vector3 v = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity = new Vector3( v.x, 4, v.z );
			//ジャンプモーションに切り替える
			animator.SetBool("Jump", true);
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(15,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(16,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(17,gameObject);
			}
			// ブースト状態でジャンプし、なおかつブーストポイントが10より多いなら）
		} else if (Input.GetButton ("Jump") && (Input.GetButton ("Boost") && boostPoint > 10)) {
			animator.SetBool("BoostUp", Input.GetButton ("Jump"));
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(33,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(34,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(35,gameObject);
			}
			// ジャンプの最大値までは上昇（ボタン押し続けている間は上昇し、最大値まで行ったら上昇値を0にする）
			if (transform.position.y > HighPoint)
				moveDirection.y = 0;
			moveDirection.y += gravity * Time.deltaTime;
			//ブーストポイント消費
			boostPoint -= BpDown;
			//ブーストアップモーションに切り替える
			animator.SetBool("BoostUp", Input.GetButton("Jump"));

		} else {
			// それ以外の場合は落下
			// フロア（地上）にいないなら
			if( onFloor == false ) {
				// 自重に-0.05ずつ下降値を加算して落下
				moveDirection.y -= 0.05f * Time.deltaTime;
				// 加速が-1以下なら-1にする（ふわっと落下させるため減速処理）
				if( moveDirection.y <= -1f ) moveDirection.y = -1f;
			}
		}
		// ブーストやジャンプが入力されていなければブースとポイントが徐々に回復（！は～されなければという否定形）
		// ブーストなし最大速度で回避値3倍
		if (!Input.GetButton ("Boost") && Force == MaxForce) {
			boostPoint += 3 * RecoverPoint;
		} else if (!Input.GetButton ("Boost") && Force < MaxForce) {
			boostPoint += 1 * RecoverPoint;
		}
		// ブーストポイントが最大以上にはならない
		//ブーストポイント使用 ＝ 最大値を超えない(ポイントが,0から,マックスまで); の処理
		boostPoint = Mathf.Clamp (boostPoint, 0, DataManager.BoostPointMax);

		//移動速度に合わせてモーションブラーの値を変える（MainCameraにCameraMotionBlurスクリプトを追加)
		//MainCameraのInspectorのCameraMotionBlurのExcludeLayersでプレイヤーと敵を選択して
		//プレイヤーと的にはモーションブラーがかからないようにする
		//float motionBlurValue = Mathf.Max (Mathf.Abs (moveSpeed.x), Mathf.Abs (moveSpeed.z)) / 20;
		//motionBlurValue = Mathf.Clamp (motionBlurValue, 0, 5);
		//Camera.main.GetComponent<CameraMotionBlur> ().velocityScale = motionBlurValue;

		//ブーストゲージの伸縮
		// ゲージの最大以上には上がらない
		//gaugeImage.transform.localScale = new Vector3 ((float)boostPoint / DataManager.BoostPointMax, 1, 1);
		gaugeImage.transform.localScale = new Vector3 ((float)boostPoint / DataManager.BoostPointMax, 1, 1);
		//gaugeImage.transform.localScale = new Vector3(0.5f,1,1);
	}


	private void OnCollisionEnter (Collision collider)
	{
		// アイテム２タグの物に接触したら
		if (collider.gameObject.tag == "Item2") {
			if (boostPoint == DataManager.BoostPointMax) return;
			// BpHealObjectを発生
			BpHealObject = Instantiate (BpHealPrefab, EffectPoint.position, Quaternion.identity);
			BpHealObject.transform.SetParent (EffectPoint);
			// アニメーターをItemGetに変更（SetTriggerなので自動的に元に戻る）
			animator.SetTrigger ("ItemGet");
			// ブーストポイント回復
			boostPoint += BpHealPoint;
			// 既にboostPointがMaxだったら何もしない
			if (boostPoint == DataManager.BoostPointMax) return;
			// boostPointがMaxになったら声出し
			if (boostPoint < DataManager.BoostPointMax) {
				if (PlayerNo == 0) {
					SoundManager.Instance.Play (18, gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.Play (19, gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.Play (20, gameObject);
				}
			} else if (boostPoint >= DataManager.BoostPointMax) {
				if (PlayerNo == 0) {
					SoundManager.Instance.PlayDelayed (36, 1.1f, gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.PlayDelayed (37, 1.1f, gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.PlayDelayed (38, 1.1f, gameObject);
				}
			}
			// ブーストポイントが最大以上にはならない
			boostPoint = Mathf.Clamp (boostPoint, 0, DataManager.BoostPointMax);
		}
		// 床(Floorタグ付いたもの)に着いたら全てニュートラル状態に
		if( collider.gameObject.tag == "Floor" ) {
			// 二段ジャンプ用ジャンプカウントをリセット
			JumpCount = 0;
			// 上方向移動値をリセット
			moveDirection.y = 0;
			Vector3 v = GetComponent<Rigidbody>().velocity;
			GetComponent<Rigidbody>().velocity = new Vector3( v.x, 0, v.z );
			// 床(Floor)設置判定してジャンプアニメーション解除
			onFloor = true;
			animator.SetBool("Jump", false);
		}
		// greenShere(Item3タグ付いたもの)接触時
		if(collider.gameObject.tag == "Item3") {
			if (PlayerNo == 0) {
				SoundManager.Instance.PlayDelayed (30, 1.1f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.PlayDelayed (31, 1.1f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.PlayDelayed (32, 1.1f, gameObject);
			}
			animator.SetTrigger ("ItemGet");
			// greenShere取得数を１追加する
			ItemCount += 1;
		}
		// Star（ボス撃破時ドロップするクリア用アイテム）取得時
		if(collider.gameObject.tag == "Star") {
			if (PlayerNo == 0) {
				SoundManager.Instance.PlayDelayed (30, 1.1f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.PlayDelayed (31, 1.1f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.PlayDelayed (32, 1.1f, gameObject);
			}
			animator.SetTrigger ("ItemGet");
			GetStar += 1;
		}
		// BigStar（ラスボス撃破時ドロップするクリア用アイテム）取得時
		if(collider.gameObject.tag == "BigStar") {
			if (PlayerNo == 0) {
				SoundManager.Instance.PlayDelayed (30, 1.1f, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.PlayDelayed (31, 1.1f, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.PlayDelayed (32, 1.1f, gameObject);
			}
			animator.SetTrigger ("ItemGet");
			GetBigStar += 1;
		}

	}

	// ブース時の点滅
	IEnumerator BoostCoroutine ()
	{
		gameObject.layer = LayerMask.NameToLayer("Invincible");
		//while文を10回ループ
		int count = 10;
		iTween.MoveTo(gameObject, iTween.Hash(
			//"position", transform.position - (transform.forward * KnockBackRange),
			"time", InvincibleTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			//透明にする
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		gameObject.layer = LayerMask.NameToLayer("Invincible");
	}

	private void OnCollisionStay(Collision collisionInfo) {
	}
}

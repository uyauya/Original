using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor; 

// プレイヤーの移動、ジャンプ、アイテムゲット（ブースト回復、ステージクリア用）
public class PlayerController : MonoBehaviour {

	private Animator animator;
	public float Force= 8;							//移動速度
	public float MaxForce = 10;						//移動速度最大値
	public float MaxBoostForce = 15;				//ブースト時の移動速度最大値
	public float Deceleration = 2.2f;				//減速(オートブレーキ)
	public float PlusForce= 0.1f;					//移動速度加算数値
	public float JumpForce = 4.0f;					//ジャンプ力
    public float DeGravity = -1.0f;
	public float DoubleJump = 4.0f;					//二段ジャンプ
	public float BoostJumpForce = 6.0f;				//ブースト時のジャンプ力
	public float HighPoint;							//ジャンプの高さ最大値
	public float InvincibleTime;					//無敵時間
	public float gravity;							//重力（ジャンプ時などに影響）
	private Vector3 moveDirection = Vector3.zero;	//プレイヤ位置方向ニュートラル設定
	public float boostPoint;						//ブーストポイント
	public float displayBoostPoint;					//ブーストポイント（画面表示用）
	public int BpDown = 20;							//ブーストゲージ消費値
	public float RecoverPoint = 0.2f;				//ブーストポイント回復値
	public GameObject gaugeImage;					//ブーストゲージ（画面表示用）
	public Text boostText;							//ブースト最大・現在数値（画面表示用）
	Vector3 moveSpeed;								//プレイヤの速さ
	private int JumpCount;							//ジャンプ回数計算用（二段ジャンプ処理に使用）
	private float interval = 2.0f;
	private float timer = 0.0f;				
	public int ItemCount;							//スフィア取得個数計算用
	public int GetStar = 0;
	public int GetBigStar = 0;
	Vector3 targetSpeed = Vector3.zero; 			//目標速度
	Vector3 addSpeed = Vector3.zero;    			//加算速度
	public GameObject BpHealEffect;					//ブーストポイント回復アイテム取得時のエフェクト
	public int PlayerNo;							//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）
	public Transform EffectPoint;					//回復等エフェクト発生元の位置取り
	public GameObject BpHealPrefab;					//ブーストポイント回復エフェクト格納場所
	public GameObject BpHealObject;
	public float BpHealPoint = 500;					//ブーストポイント回復値（アイテム取得時）
	protected bool isInvincible;					//無敵処理（ダメージ受けた際に使用）
	private ModelColorChange modelColorChange;
	private bool onFloor = true;					//床に設置しているかどうか
	private bool IsDownJumpButton = false;			//ジャンプ不可
	public static bool isBoost;						//ブーストボタンをオン・オフ設定
	public static bool isClear = false;				//ステージクリアしたかどうか
	public static bool isStop = false;				//プレイヤーの動きを止めたい時に使う
	public static bool isDash = false;				//ダッシュしているかどうか
	public float DashDistance = 2.0f;				//ダッシュ（瞬間移動）距離
	public float DashIdleTime = 1.0f;				//ダッシュ後のダッシュ操作不能時間
	public GameObject DashAttck;	
	public GameObject DAEffectPrefab;				
	public GameObject DAEffectObject;
    public GameObject ClearWall;
    public Transform muzzle;						//DashAttackプレハブ発生元
    public Transform Attack;
    public float DashBpDown = 1.0f;					//DashAttack起動時の消費ブーストポイント
	public float DashRate = 2.0f;					//ダッシュ時の速度の掛け率
    int layerMask = ~0;

    /*[CustomEditor(typeof(PlayerController))]
	public class PlayerControllerEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;
		
			public override void OnInspectorGUI()
			{
			PlayerController PL= target as PlayerController;
				PL.Force = EditorGUILayout.FloatField( "移動速度", PL.Force);
				PL.MaxForce = EditorGUILayout.FloatField( "移動速度上限", PL.MaxForce);
				PL.MaxBoostForce = EditorGUILayout.FloatField( "ブースト時の移動速度上限", PL.MaxBoostForce);
				PL.jumpSpeed = EditorGUILayout.FloatField( "ジャンプ力", PL.jumpSpeed);	
				PL.HighPoint = EditorGUILayout.FloatField( "ジャンプ高さ上限", PL.HighPoint);	
				PL.gravity = EditorGUILayout.FloatField( "重力", PL.gravity);
				PL.BpDown = EditorGUILayout.FloatField( "ブーストポイント消費量", PL.BpDown);
				PL.RecoverPoint = EditorGUILayout.FloatField( "ブーストポイント回復値", PL.RecoverPoint);
				PL.BpHealPoint = EditorGUILayout.FloatField( "アイテムでのBP回復値", PL.BpHealPoint);
				PL.InvisibleTime = EditorGUILayout.FloatField( "無敵時間", PL.InvisibleTime);
			}
	}*/

    void Start()	//　ゲーム開始時の設定
	{
		animator = GetComponent<Animator>();			// Animatorを使う場合は設定する
		boostPoint = DataManager.BoostPointMax;			// ブーストポイントを最大値に設定
		moveSpeed = Vector3.zero;						// 開始時は移動していないので速さはゼロに
		isBoost = false;								// ブーストはオフに
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

	//ボタン等が押された時など、その都度Updateする
	void Update()
	{
		//ステージクリア条件かストップ条件で動けなくする
		if ((isClear == true) || (isStop == true))
		{
			return;
		}
		//ProjectSettigでJumpに割り当てたキーが押されたら
		if (Input.GetButtonDown("Jump"))
		{
			//ジャンプボタンをオンにする
			IsDownJumpButton = true;
		}
		//現在のブーストゲージと最大ブーストゲージをUI Textに表示する
		//boostText.text = string.Format("{0:0000} / {1:0000}", displayBoostPoint, DataManager.BoostPointMax);
		boostText.text = string.Format("{0:0000} / {1:0000}", boostPoint, DataManager.BoostPointMax);

        //プレイヤー死亡条件になってたらDeadアニメーション
        if (BattleManager.PlayerDead == true)
        {
            animator.SetBool("Dead", true);
        }

        //ステージクリア条件かストップ条件で動けなくする
        if ((isClear == true) || (isStop == true))
        {
            return;
        }
        /*if(animator.SetBool("Move", false))
        {
            isDash = false;
            rigidbody.constraints = RigidbodyConstraints.FreezePosition;
            Debug.Log("急ブレーキ");
        }*/
        

        //ブーストボタンが押されてブーストポイント残が1以上あればフラグを立てブーストポイントを消費
        //ブーストで速度、攻撃力UP。ブースト中ブーストボタンでダッシュアタック(瞬間移動)
        if (Input.GetButtonDown("Boost") && boostPoint > 0)
		{
			if(isBoost)
        	{
				if (Input.GetButtonDown("Boost"))
				{
					isDash = true;
					Debug.Log("ダッシュ");
					boostPoint -= DashBpDown;  
				}
			}
			else
			{
				Debug.Log("ブースト");
				isBoost = true;                             //ブースト状態にする
				Boost();
				//boostPoint -= BpDown * Time.deltaTime;      //ブーストポイントをBpDown設定値分消費
				//Debug.Log("BPダウン" + BpDown);
            	//StartCoroutine("BoostCoroutine");           //コルーチン処理（下記参照）
				                                          	// プレイヤのレイヤーをInvincibleに変更
                                                            // Edit→ProjectSetting→Tags and LayersでInvicibleを追加
                                                            // Edit→ProjectSetting→Physicsで衝突させたくない対象と交差している所の✔を外す
                                                            // ここではEnemyと衝突させたくない（すり抜ける）為、Enemeyのレイヤーも追加
                                                            // EnemeyとPlayerの交差してる✔を外す（プレイヤのLayerをPlayer、EnemyのLayerをEnemyに設定しておく）
                                                            //gameObject.layer = LayerMask.NameToLayer("Invincible");
                                           					//StartCoroutine ("BoostCoroutine");
					// ブースト時、モーション変更、Attackpoint2倍に
					if (Force <= MaxBoostForce)
					{                   // ForceがMaxBoostForceの値以下なら
						MaxForce += Time.deltaTime * PlusForce;     // MaxBoostForceまでMaxForce(通常最大速度)に加速
						Force = Mathf.Min(Force, MaxBoostForce);    // ForceがMaxBoostForceの値を超えない
						//Debug.Log (Force);
						//ブースト時プレイヤー攻撃値2倍に
						DataManager.AttackPoint *= 2;
					}
					//ブースト状態ならアニメーターをBoostに切り替える
					if(isBoost)
					{
					animator.SetBool("Boost", true);
						Debug.Log("飛ぶ");
					}
			}
		}
		if (Input.GetButtonDown("Fire3")) 				//ブースト中にFire3でブースト解除
		{
			if(isBoost)
			{
				(isBoost) = false;
				Debug.Log("ブースト解除");
				if (Force <= MaxForce)
				{
					Force += Time.deltaTime * PlusForce;
					Force = Mathf.Min(Force, MaxForce);
					DataManager.AttackPoint *= 1;
					//Debug.Log (Force);
				}
				animator.SetBool("Boost", Input.GetButtonDown("Boost") && boostPoint > 0);
			}
		}

//ジャンプキーによる上昇（二段ジャンプ）
		//Debug.Log("jump JumpCount:" + JumpCount);
        // ジャンプしてない（ジャンプボタン押されてない）状態で
        IsDownJumpButton = false;
        //ジャンプが押されて1回目なら（2回目でないなら）（一番下のコライダー処理が関係してる）
        if (Input.GetButtonDown("Jump") == true && JumpCount < 2)
        {
            // ジャンプ数加算
            JumpCount++;
            //Debug.Log(JumpCount);
            // ジャンプの上昇力を設定( v.x, JumpForce, v.z )で縦方向に加算
            // Rigidbodyのvelocityをｖと略す。
            Vector3 v = GetComponent<Rigidbody>().velocity;
            // y方向にJumpForce値加算（ジャンプ）
            GetComponent<Rigidbody>().velocity = new Vector3(v.x, JumpForce, v.z);
            //ジャンプモーションに切り替える
            animator.SetBool("Jump", true);
            if(JumpCount == 2){
				GetComponent<Rigidbody>().velocity = new Vector3( v.x, DoubleJump, v.z );
				animator.SetBool("DoubleJump", true);
				//Debug.Log("ダブル");
			} else {
				GetComponent<Rigidbody>().velocity = new Vector3( v.x, JumpForce, v.z );
				animator.SetBool("Jump", true);
				//Debug.Log("ジャンプ");
			}
            // キャラ別に声変更
            if (PlayerNo == 0)
            {
                SoundManager.Instance.Play(15, gameObject);
            }
            if (PlayerNo == 1)
            {
                SoundManager.Instance.Play(16, gameObject);
            }
            if (PlayerNo == 2)
            {
                SoundManager.Instance.Play(17, gameObject);
            }
            // ブースト状態でジャンプし、なおかつブーストポイントが10より多いなら）
        }
        else if (Input.GetButton("Jump") && (Input.GetButton("Boost") && boostPoint > 10))
        {
            Vector3 v = GetComponent<Rigidbody>().velocity;
            GetComponent<Rigidbody>().velocity = new Vector3(v.x, BoostJumpForce, v.z);
            animator.SetBool("BoostUp", Input.GetButton("Jump"));
            if (PlayerNo == 0)
            {
                SoundManager.Instance.Play(33, gameObject);
            }
            if (PlayerNo == 1)
            {
                SoundManager.Instance.Play(34, gameObject);
            }
            if (PlayerNo == 2)
            {
                SoundManager.Instance.Play(35, gameObject);
            }
            // ジャンプの最大値までは上昇（ボタン押し続けている間は上昇し、最大値まで行ったら上昇値を0にする）
            if (transform.position.y > HighPoint)
                moveDirection.y = 0;
            moveDirection.y += gravity * Time.deltaTime;
            //ブーストポイント消費
            boostPoint -= BpDown;
            //ブーストアップモーションに切り替える
            animator.SetBool("BoostUp", Input.GetButton("Jump"));

        }
        else
        {
            // それ以外の場合は落下
            // フロア（地上）にいないなら
            if (onFloor == false)
            {
                // 自重に-0.05ずつ下降値を加算して落下
                moveDirection.y -= 0.05f * Time.deltaTime;
                // 落下速度が-1以下なら-1にする（ふわっと落下させるための減速処理）
                if (moveDirection.y <= DeGravity) moveDirection.y = DeGravity;
            }
        }

        // 何も押されていないニュートラル状態ではmove解除
        /*if ((Input.GetAxis("Horizontal") == 0) && (Input.GetAxis("Vertical") == 0))
        {
            animator.SetBool("Move", false);
        //if(animator.SetBool("Move", false)) {
            isDash = false;
            // アッドフォースされた速度がMaxForce以上ならフォースにマイナス処理して減速（滑り止め）
            // プレイヤの滑り具合がグラビティを変えることによって調節できるが、変更すると重い軽いでジャンプなどにも影響が出てくる
            // Edit→Project Settings→Physicsで全体的な重力は変えられるがインスペクタ上でGravity変更した方がよい
            //速度がMaxForceより上(ブースト状態)なら減速
            if (Force > MaxForce)
            {
                Force -= Deceleration;
                //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
            else if (Force > 8.0f)
            {
                CrearWall();
            }
        }*/
    }

    //一定時間間隔で常にUpdateする
    void FixedUpdate()
	{
		//プレイヤー死亡条件になってたらDeadアニメーション
		if (BattleManager.PlayerDead == true)
		{
			animator.SetBool("Dead", true);
		}

		//ステージクリア条件かストップ条件で動けなくする
		if ((isClear == true) || (isStop == true))
		{
			return;
		}

		//ブーストボタンが押されてブーストポイント残が1以上あればフラグを立てブーストポイントを消費
		/*if (Input.GetButton("Boost") && boostPoint > 0)
		{
			boostPoint -= BpDown;							//ブーストポイントをBpDown設定値分消費
			isBoost = true;									//ブースト状態にする
			StartCoroutine ("BoostCoroutine");				//コルーチン処理（下記参照）
			// プレイヤのレイヤーをInvincibleに変更
			// Edit→ProjectSetting→Tags and LayersでInvicibleを追加
			// Edit→ProjectSetting→Physicsで衝突させたくない対象と交差している所の✔を外す
			// ここではEnemyと衝突させたくない（すり抜ける）為、Enemeyのレイヤーも追加
			// EnemeyとPlayerの交差してる✔を外す（プレイヤのLayerをPlayer、EnemyのLayerをEnemyに設定しておく）
			//gameObject.layer = LayerMask.NameToLayer("Invincible");
			//StartCoroutine ("BoostCoroutine");
		}
		else
		{
			isBoost = false;								//それ以外ならブーストなし（通常状態）
			//gameObject.layer = LayerMask.NameToLayer("Player");
		}*/

		//通常時とブースト時で変化
		if (isBoost)										//ブーストならMaxBoostForce値まで加速
		{
			// ブースト時
			if (Force <= MaxBoostForce) {					// ForceがMaxBoostForceの値以下なら
				MaxForce += Time.deltaTime * PlusForce;		// MaxBoostForceまでMaxForce(通常最大速度)に加速
				Force = Mathf.Min(Force, MaxBoostForce);	// ForceがMaxBoostForceの値を超えない
				//Debug.Log (Force);
			}
			//ブースト状態ならアニメーターをBoostに切り替える
			if(isBoost)
			{
				animator.SetBool("Boost",true);
			}
		}
		else　　　　　　　　　　　　　　　　　　　　　　　　　　
		{
			if (Force <= MaxForce) {					
				Force += Time.deltaTime * PlusForce;	
				Force = Mathf.Min(Force, MaxForce);		
				//Debug.Log (Force);
			}
			animator.SetBool("Boost", Input.GetButtonDown("Boost")&& boostPoint > 0);
		}
			

		//モーションを切り替える
		if (Input.GetAxis("Horizontal") > 0)	// 右移動（右が押されている場合）
		{
			//ダッシュアタック時
			if (isDash == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5.0f);
				animator.SetBool("Move", true);
                RaycastHit hit;
                if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
               {
                    transform.position = hit.point - transform.forward;
                }else
                {
                    gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
                }
                //gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
				DashAttack ();
				isDash = false;
			}
			//ショットを撃っている状態（減速処理）
			if (PlayerShoot.isShoot == true)
            {
				//撃たない時より動作速度を落とす（敵を狙いやすくする）
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 2.0f);
				animator.SetBool("Move", true);
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force * 0.5f);
			}
			//ショット撃っていない状態
            else
            {
			// クォータリオン（球体を動かすような処理）で5.0の速度でプレイヤを横に向かせる
			transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5.0f);
			// アニメーターをMoveに切り替え
			animator.SetBool("Move", true);
			// プレイヤに速度を加える（transform.Translateは移動だが、アッドフォースは後ろから押すような操作なので、坂道など段差が
			// ある場合、自動で加減速処理して移動する
			gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
			//Debug.Log (Force);
			}
		}
		else if (Input.GetAxis("Horizontal") < 0)	//左移動
		{
			//ダッシュアタック時
			if (isDash == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5.0f);
				animator.SetBool("Move", true);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    transform.position = hit.point - transform.forward;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
                }
                //gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
				DashAttack ();
				isDash = false;
			}
			//ショットを撃っている状態（減速処理）
            if (PlayerShoot.isShoot == true)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 2.0f);
                animator.SetBool("Move", true);
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force * 0.5f);
                
            }
			//ショット撃っていない状態
            else
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5.0f);
                animator.SetBool("Move", true);
                gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
                
            }
        }
		else if (Input.GetAxis("Vertical") > 0)	// 前移動（前が押されている場合）
		{
			//ダッシュアタック時
			if (isDash == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
				animator.SetBool("Move", true);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    transform.position = hit.point - transform.forward;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
                }
                //gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
				DashAttack ();
				isDash = false;
                
            }
			//ショットを撃っている状態（減速処理）
			if (PlayerShoot.isShoot == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 2.0f);
				animator.SetBool("Move", true);
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force * 0.5f);
			}
			//ショット撃っていない状態
			else
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
				animator.SetBool("Move", true);
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
                //Debug.Log("速度" + Force);
            }

		}
		else if (Input.GetAxis("Vertical") < 0)	//後移動
		{
			//ダッシュアタック時
			if (isDash == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -18, 0), Time.deltaTime * 5.0f);
				animator.SetBool("Move", true);
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
                {
                    transform.position = hit.point - transform.forward;
                }
                else
                {
                    gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
                }
                //gameObject.GetComponent<Rigidbody>().transform.position += transform.forward * DashRate;
				DashAttack ();
				isDash = false;
			}
			//ショットを撃っている状態（減速処理）
			if (PlayerShoot.isShoot == true)
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * 2.0f);
				animator.SetBool("Move", true);
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force * 0.5f);
			}
			//ショット撃っていない状態
			else
			{
				transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * 5.0f);
				animator.SetBool("Move", true);
				gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * Force);
			}
		}
		else
		{
            // 何も押されていないニュートラル状態ではmove解除
            animator.SetBool("Move", false);
			isDash = false;
            // アッドフォースされた速度がMaxForce以上ならフォースにマイナス処理して減速（滑り止め）
            // プレイヤの滑り具合がグラビティを変えることによって調節できるが、変更すると重い軽いでジャンプなどにも影響が出てくる
            // Edit→Project Settings→Physicsで全体的な重力は変えられるがインスペクタ上でGravity変更した方がよい
            if (onFloor == false)
            {
                return;
            } else if (onFloor == true)
            {
                if (Force > MaxForce)
                {
                    Force -= Deceleration;
                    //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                }
                else
                {
                    Vector3 v = GetComponent<Rigidbody>().velocity;
                    GetComponent<Rigidbody>().velocity = new Vector3(0, v.y, 0);
                }
            }

        }


        // ブーストやジャンプが入力されていなければブーストポイントが徐々に回復（！は～されなければという否定形）
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
			// boostPointがMax以下の時の声出し
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
			// boostPointがMaxになった時の声出し
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
		// 着地処理
		// 床(Floorタグ付いたもの)に着いたら全てニュートラル状態に
		if( collider.gameObject.tag == "Floor" ) {
			// 二段ジャンプ用ジャンプカウントを0に（リセット）
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
			// greenShere取得数を１追加する（取得数計算）
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
			//移動不可でSalute
			isClear = true;	
			animator.SetBool("Salute", true);
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
			isClear = true;	
			animator.SetBool("Salute", true);
			GetBigStar += 1;
		}

	}

	// ブースト時の点滅
	IEnumerator BoostCoroutine ()
	{
		// 無敵処理
		// Edit→ProjectSetting→PhysicsでInvicivleとEnemyやBossの交差部分の✔を外して接触判定無効にする
		gameObject.layer = LayerMask.NameToLayer("Invincible");
		//while文を10回ループ
		int count = 10;		// 点滅回数
		iTween.MoveTo(gameObject, iTween.Hash(
			//"position", transform.position - (transform.forward * KnockBackRange),
			"time", InvincibleTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){	// 点滅処理
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
		// レイヤーをPlayerに戻して無敵解除にする
		gameObject.layer = LayerMask.NameToLayer("Player");
	}

	//ダッシュ直後に攻撃。
	void DashAttack()
    {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject dattack = GameObject.Instantiate (DashAttck)as GameObject;
		//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
		dattack.transform.position = muzzle.position;
	}

    void Boost()
    {
        boostPoint -= BpDown * Time.deltaTime;      //ブーストポイントをBpDown設定値分消費
		Debug.Log("BPダウン" + BpDown);
		//StartCoroutine("BoostCoroutine");  
    }

	private void OnCollisionStay(Collision collisionInfo) {
	}
}

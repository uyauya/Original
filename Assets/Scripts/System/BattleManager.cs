using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// ゲームオーバーやステージ移行などゲーム全般の条件を管理
public class BattleManager : MonoBehaviour {
    // Edit→ProjectSettings→ScriptExecutionOrder→＋でBattleManagerを出して一番上に
    // BattleManagerスクリプトを一番最初に読み取るようにする
    private Animator animator;
    int battleStatus;
	const int BATTLE_START = 0;
	const int BATTLE_PLAY  = 1;
	const int BATTLE_END   = 2;
	const int ENDING  	   = 3;
	float timer;						 	// 時間計測洋(LimitedTimer参照）
	public Image messageStart;
	public Image messageWin;
	public Image messageLose;
	public GameObject mesaageSTART;		 	// スタート表示
	public GameObject mesaageClear;		 	// ステージクリア表示
	public Text ScoreText;				 	// スコア表示用
	public Text LevelText;
    //public GameObject playerImage;          
    private int ItemCount;				 	// アイテム取得数をカウント
	PlayerController playerController;
	public GameObject WarpEffect;		 	// ボス面移行用ワープ
	int clearScore;						 	// クリア条件となるスコア  
	public GameObject Player;			
	public float ChangeTime = 5.0f;			// シーン変更までの時間
	public float ChangeTime2 = 0.0f;
	public int Count;					 	// ステージ移行する為のアイテム取得個
	public int PlayerNo;				 	//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）
	public static bool PlayerDead = false;	//プレイヤキャラが死亡したかどうか
    public GameObject[] Prefab_Player;
    private int CurrentLevel;
	public int GetStar = 0;
	public int GetBigStar = 0;
	public static bool isClear = false;				//ステージクリアしたかどうか
	public bool isPChange = false;
	public GameObject GoalPosition;
	public static bool ResetColor = false;

    void Awake(){
            Player = Instantiate (Prefab_Player [DataManager.PlayerNo]);
     }

    void Start () {
        animator = GetComponent<Animator>();            // Animatorを使う場合は設定する
        ScoreText.text = "Score:0";
        LevelText.text = "Lv:0";
        //Debug.Log("レベルテキスト");
        timer = 0;
		battleStatus = BATTLE_START;	//時間0秒、最初にスタートを表示させる
		//ScoreTextにScoreオブジェクトのTextテキストの値を代入する
		ScoreText = GameObject.Find ("Score").GetComponent<Text> ();
		LevelText = GameObject.Find ("Level").GetComponent<Text> ();
        //Debug.Log("レベル");
		// スコアテキストにDataManagerのスコアを代入
		ScoreText.text = DataManager.Score.ToString();
		LevelText.text = DataManager.Level.ToString();
        //Debug.Log("DMレベル");
        //playerImage = GameObject.Find("PlayerImage");
        //スタート時はStartは表示、WinとLoseは非表示
        messageStart.enabled = true;
		messageWin.enabled = false;
		messageLose.enabled = false;
		//playerControllerにPlayerタグのついたオブジェクトのPlayerControllerの値を代入する
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
		Player = GameObject.FindWithTag("Player");
        //instantiateValueに値を代入するのをBattleManagerより早くするため
        //EnemyスクリプトにはStartでなくAwakeに記入する（起動直後に処理）
        PlayerDead = false;
		GoalPosition = GameObject.Find("GoalPosition");
    }

	void Update () {

        if(DataManager.PlayerChange == true)
        {
            Vector3 PlayerPos = Player.transform.position;
            CurrentLevel = DataManager.Level;
            Destroy(Player);
            if (DataManager.PlayerNo == 0)				//新たなプレイヤー発生
			{
            Player = Instantiate (Prefab_Player [DataManager.PlayerNo = 3]);
            //Debug.Log("プレイヤ3");
            Player.transform.position = PlayerPos;
            DataManager.Level = CurrentLevel;
            DataManager.PlayerChange = false;
            isPChange = true;
            //Debug.Log("レベル" + DataManager.Level);
            }
        }

		if(DataManager.PlayerReturn == true)
		{
			Vector3 PlayerPos = Player.transform.position;
			CurrentLevel = DataManager.Level;
			Destroy(Player);
			if (DataManager.PlayerNo == 3)				//新たなプレイヤー発生
			{
				Player = Instantiate (Prefab_Player [DataManager.PlayerNo = 0]);
                //Debug.Log("プレイヤ0");
                //Debug.Break();
                Player.transform.position = PlayerPos;
				DataManager.Level = CurrentLevel;
                DataManager.PlayerReturn = false;
                isPChange = false;
                //Debug.Log("レベル" + DataManager.Level);
            }
		}

        if (Input.GetButtonUp("Fire4"))
        {
            if ((isPChange == false)&&(DataManager.PlayerChange == false)&&(DataManager.PlayerReturn == false))
            {
				ChangeWeapon.Weptype = 0;
				DataManager.PlayerChange = true;
                //Debug.Log("変わるよ");

            }
        if ((Input.GetButtonUp("Fire4"))&&(isPChange == true))
            {
				ChangeWeaponR.WePRtype = 0;
				DataManager.PlayerReturn = true;
				//Debug.Log("戻るよ");
            }
        }


        // 得点をテキスト形式で画面に表示
        ScoreText.text = DataManager.Score.ToString();
		LevelText.text = DataManager.Level.ToString();

		switch (battleStatus) {
		
		case BATTLE_START:
                
                //3秒経過でメッセージを消して状態移行
                timer += Time.deltaTime;
				if (timer > 3) {
				mesaageSTART.SetActive (true);		// スタート表示
				messageStart.enabled = false;
				battleStatus = BATTLE_PLAY;
				timer = 0;
				//コンティニュー判別処理
				if (DataManager.Continue == false) {
				}
			}
			break;
			
		case BATTLE_PLAY:
                ScoreText.text = DataManager.Score.ToString();
                //プレイヤーの体力が0以下になったらゲームオーバー
                if (PlayerAp.armorPoint <= 0) {
				//battleStatus = BATTLE_END;
				messageLose.enabled = true;
				//キャラクター別に声変更
				if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 3)) {
					SoundManager.Instance.Play (45, gameObject);
				}
				if (DataManager.PlayerNo == 1) {
					SoundManager.Instance.Play (46, gameObject);
				}
				if (DataManager.PlayerNo == 2) {
					SoundManager.Instance.Play (47, gameObject);
				}
				PlayerDead = true;
				// 一定時間後シーン移動（ChangeTimeで時間設定）
				// GameOver処理起動（下記参照）
				Invoke("GameOver", ChangeTime);	
			//プレイヤが現地点から高さ（Y軸）10下がったらゲームオーバー（落下判定）
			} else if (Player.transform.position.y <= -10.0f) { 
				battleStatus = BATTLE_END;
				messageLose.enabled = true;
				if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 3)) {
					SoundManager.Instance.Play (45, gameObject);
				}
				if (DataManager.PlayerNo == 1) {
					SoundManager.Instance.Play (46, gameObject);
				}
				if (DataManager.PlayerNo == 2) {
					SoundManager.Instance.Play (47, gameObject);
				}
				PlayerDead = true;
                   /* timer += Time.deltaTime;
                    if (timer > ChangeTime - 0.2f)
                    {
                        // 一定時間後シーン移動（ChangeTimeで時間設定）
                        // GameOver処理起動（下記参照）
                        PlayerDead = false;
                    }*/
                    Invoke("GameOver", ChangeTime);
                
                }
			// プレイヤーのアイテム（グリーンスフィア）取得数が一定以上ならボス面に移行
			//if (playerController.ItemCount >= Count) {	// countで取得数設定
			if (PlayerController.ItemCount == true)
			{
				ItemCount += 1;
				PlayerController.ItemCount = false;
			}
			if (ItemCount >= Count) {	// countで取得数設定
				//Debug.Log("スフィアゲット");
				//プレイヤーの動きを止める
				PlayerController.isStop = true;
				if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 3)) {
					SoundManager.Instance.Play(18,gameObject);
				}
				if (DataManager.PlayerNo == 1) {
					SoundManager.Instance.Play(19,gameObject);
				}
				if (DataManager.PlayerNo == 2) {
					SoundManager.Instance.Play(20,gameObject);
				}
				battleStatus = BATTLE_PLAY;
				Instantiate(WarpEffect, Player.transform.position, Player.transform.rotation);	// ワープ用エフェクト発生
				// 一定時間後シーン移動（ChangeTimeで時間設定）
				// NextScene処理起動（下記参照）
				Invoke("NextScene", ChangeTime);	
				//playerController.ItemCount = 0;		// シーン移動時グリーンスフィア取得数をリセット
				ItemCount = 0;		// シーン移動時グリーンスフィア取得数をリセット
				//プレイヤーの動けるようにする
				PlayerController.isStop = false;
			}
            else if(PlayerController.Goal == true)
                {
                    Invoke("NextScene", ChangeTime);
                    PlayerController.Goal = false;
                }

			// ボス撃破時スター出現
			// スタースフィアを1個以上取得したら次面へ
			if (PlayerController.GetStar == true)
			{
				GetStar += 1;
				PlayerController.GetStar = false;
			}
			if (GetStar >= 1 ) {
				Debug.Log("スターゲット");
                //正面を向く
                Player.transform.LookAt(Camera.main.transform);
                mesaageClear.SetActive (true);      // ステージクリア表示
                // 一定時間後シーン移動（ChangeTimeで時間設定）
				// NextScene処理起動（下記参照）
				Invoke("NextScene", ChangeTime);	
				//playerController.GetStar = 0;		// スター取得数をリセット
				GetStar = 0;		// スター取得数をリセット
			}

			// ラスボス撃破時ビッグスター出現
			// ビッグスターオブジェクトを1個以上取得したらエンディング
			if (PlayerController.GetBigStar == true)
			{
				GetBigStar += 1;
				PlayerController.GetBigStar = false;
			}
			if (GetBigStar >= 1) {
				Player.transform.LookAt(Camera.main.transform);
				mesaageClear.SetActive (true);		// ステージクリア表示
				Invoke ("END", ChangeTime);			// ChangeTime時間後END処理起動（下記参照）
				//playerController.GetBigStar = 0;	// ビッグスター取得数をリセット	
				GetBigStar = 0;	// ビッグスター取得数をリセット	
		}
		break;

		case BATTLE_END:
			break;

		}
	}


	//次面移動処理
	private void NextScene(){
		//StageManagerに次面番号作成
		StageManager.Instance.StageNo++;
		//DataManagerにクリアしたStageNo作成（ステージセレクト用処理）
		//ステージセレクトではクリアしたステージのみ選択出来るようにする
		if (StageManager.Instance.StageNo > DataManager.ClearScene) {
			DataManager.ClearScene = StageManager.Instance.StageNo;
		}
			SceneManager.LoadScene (StageManager.Instance.StageName [StageManager.Instance.StageNo]);
			//クリア条件解除（プレイヤキャラの操作を元に戻す）
			//PlayerController.isClear = false;
			isClear = false;
	}

	//ゲームオー画面に移動
	private void GameOver(){
        SceneManager.LoadScene ("GameOver");
	}

	//エンディング画面に移動
	private void END(){
		SceneManager.LoadScene ("END");
	}


}

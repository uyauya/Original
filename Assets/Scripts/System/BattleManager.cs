using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class BattleManager : MonoBehaviour {
	// Edit→ProjectSettings→ScriptExecutionOrder→＋でBattleManagerを出して一番上に
	// BattleManagerスクリプトを一番最初に読み取るようにする

	int battleStatus;
	const int BATTLE_START = 0;
	const int BATTLE_PLAY  = 1;
	const int BATTLE_END   = 2;
	const int ENDING  	   = 3;
	float timer;						// 時間計測洋(LimitedTimer参照）
	public Image messageStart;
	public Image messageWin;
	public Image messageLose;
	public static int score;			// 敵を倒した数。Enemyスクリプトでカウントアップ  
	public Text ScoreText;				// スコア表示用

	//public int Score;					// 得点兼プレイヤ経験値
	private int ItemCount;				// アイテム取得数をカウント
	PlayerController playerController;
	public GameObject WarpEffect;		// ボス面移行用ワープ
	int clearScore;						// クリア条件となるスコア  
	public GameObject Player;			
	public float ChangeTime;			// シーン変更までの時間
	public int Count;					// ステージ移行する為のアイテム取得個
	public int PlayerNo;				//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）

	void Start () {	
		ScoreText.text = "Score:0";
		ScoreText = GameObject.Find ("Score").GetComponent<Text> ();
		battleStatus = BATTLE_START;	//時間0秒、最初にスタートを表示させる
		timer = 0;
		//スタート時はStartは表示、WinとLoseは非表示
		messageStart.enabled = true;
		messageWin.enabled = false;
		messageLose.enabled = false;
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
		//敵の最大生成数をクリア数にする
		//instantiateValueに値を代入するのをBattleManagerより早くするため
		//EnemyスクリプトにはStartでなくAwakeに記入する（起動直後に処理）
		//clearScore = EnemyInstantiate.instantiateValue;
		//Score = 0;
		Player = GameObject.FindWithTag("Player");
		// 得点をテキスト形式で画面に表示
		ScoreText.text = DataManager.Score.ToString();

		// Scene移行時プレイヤーのパラメータの中身を取得
		/*int level = GameObject.FindWithTag("Player").GetComponent<PlayerController>().Level;
		int attackPoint = GameObject.FindWithTag("Player").GetComponent<PlayerController>().AttackPoint;
		int boostpointMax = GameObject.FindWithTag("Player").GetComponent<PlayerController>().boostPointMax;
		int armorpointMax = GameObject.FindWithTag("Player").GetComponent<PlayerAp>().armorPointMax;
		string sceneName = SceneManager.GetActiveScene ().name;
		UserParam userParam = new UserParam(DataManager.PlayerNo, level, attackPoint, boostpointMax, armorpointMax, Score, sceneName);
		// DataManagerオブジェクトからSaveスクリプトのSaveDataを取得
		GameObject.Find("DataManager").GetComponent<Save> ().SaveData (userParam);*/
		new UserParam ().SaveData ();
	}

	void Update () {
		// 得点をテキスト形式で画面に表示
		ScoreText.text = DataManager.Score.ToString();
		switch (battleStatus) {
		
		case BATTLE_START:
			//時間経過でメッセージを消して状態移行
			timer += Time.deltaTime;
				if (timer > 3) {
				//Debug.Log ("すたーと");
				messageStart.enabled = false;
				battleStatus = BATTLE_PLAY;
				timer = 0;
				if (DataManager.Continue == false) {
					/*int level = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().Level;
					int attackPoint = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint;
					int boostpointMax = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().boostPointMax;
					int armorpointMax = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().armorPointMax;
					string sceneName = SceneManager.GetActiveScene ().name;*/
					//UserParam userParam = new UserParam (DataManager.PlayerNo, level, attackPoint, boostpointMax, armorpointMax, Score, sceneName);
					//UserParam.instanse.SaveData ();
				}
			}
			break;
			
		case BATTLE_PLAY:
			ScoreText.text = DataManager.Score.ToString ();
			//Score += enemyScore;
			//プレイヤーの体力が0以下になったら敗北
			if (PlayerAp.armorPoint <= 0) {
				battleStatus = BATTLE_END;
				messageLose.enabled = true;
				new UserParam ().SaveData ();
				//SceneManager.LoadScene ("GameOver");
				if (PlayerNo == 0) {
					SoundManager.Instance.Play (45, gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.Play (46, gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.Play (47, gameObject);
				}
				Invoke("GameOver", ChangeTime);	// 一定時間後シーン移動（ChangeTimeで時間設定）
			} else if (Player.transform.position.y <= -10.0f) { 
				//Destroy(gameObject);
				battleStatus = BATTLE_END;
				messageLose.enabled = true;
				// Scene移行時プレイヤーのパラメータの中身を取得
				/*int level = GameObject.FindWithTag("Player").GetComponent<PlayerController>().Level;
				int attackPoint = GameObject.FindWithTag("Player").GetComponent<PlayerController>().AttackPoint;
				int boostpointMax = GameObject.FindWithTag("Player").GetComponent<PlayerController>().boostPointMax;
				int armorpointMax = GameObject.FindWithTag("Player").GetComponent<PlayerAp>().armorPointMax;
				string sceneName = SceneManager.GetActiveScene ().name;
				UserParam userParam = new UserParam(DataManager.PlayerNo, level, attackPoint, boostpointMax, armorpointMax, Score, sceneName);
				UserParam.instanse.SaveData ();*/
				SoundManager.Instance.Play(1,gameObject);
				new UserParam ().SaveData ();
				if (PlayerNo == 0) {
					SoundManager.Instance.Play (45, gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.Play (46, gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.Play (47, gameObject);
				}
				//SceneManager.LoadScene ("GameOver");
				Invoke("GameOver", ChangeTime);	// 一定時間後シーン移動（ChangeTimeで時間設定）
				//Time.timeScale = 1;
			}
			// プレイヤーのアイテム（グリーンスフィア）取得数が一定以上ならボス面に移行
			if (playerController.ItemCount >= Count) {	// countで取得数設定
				if (PlayerNo == 0) {
					SoundManager.Instance.Play(18,gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.Play(19,gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.Play(20,gameObject);
				}
				battleStatus = BATTLE_PLAY;
				Instantiate(WarpEffect, Player.transform.position, Player.transform.rotation);	// ワープ用エフェクト発生
				// Scene移行時プレイヤーのパラメータの中身を取得
				/*int level = GameObject.FindWithTag("Player").GetComponent<PlayerController>().Level;
				int attackPoint = GameObject.FindWithTag("Player").GetComponent<PlayerController>().AttackPoint;
				int boostpointMax = GameObject.FindWithTag("Player").GetComponent<PlayerController>().boostPointMax;
				int armorpointMax = GameObject.FindWithTag("Player").GetComponent<PlayerAp>().armorPointMax;
				//string sceneName = SceneManager.GetActiveScene ().name;
				Debug.Log(StageManager.Instance.StageNo);*/

				string sceneName = StageManager.Instance.StageName[StageManager.Instance.StageNo +1];
				//StageManager.Instance.StageNo++;
				//UserParam userParam = new UserParam(DataManager.PlayerNo, level, attackPoint, boostpointMax, armorpointMax, Score, sceneName);
				//UserParam.instanse.SaveData ();
				new UserParam ().SaveData ();
				Invoke("NextScene", ChangeTime);	// 一定時間後シーン移動（ChangeTimeで時間設定）
				playerController.ItemCount = 0;
			}	
			break;

		case BATTLE_END:
			// ボス撃破時スター出現
			// スターオブジェクトを取得したら
			if (playerController.GetStar >= 1 ) {
				battleStatus = BATTLE_PLAY;
				string sceneName = StageManager.Instance.StageName[StageManager.Instance.StageNo +1];
				new UserParam ().SaveData ();
				Invoke("NextScene", ChangeTime);	// 一定時間後シーン移動（ChangeTimeで時間設定）
				playerController.GetStar = 0;
			}
			// スコアが10000点以上ならボスステージクリア
			//if (DataManager.Score >= 10000) {
			//}
			break;
		
		case ENDING:
			// ラスボス撃破時ビッグスター出現
			// ビッグスターオブジェクトを取得したら
			if (playerController.GetBigStar >= 1) {
				Invoke ("END", ChangeTime);
				playerController.GetBigStar = 0;
			}
			//一定時間経過したら遷移可能にする
			timer += Time.deltaTime;
			
		 if(timer > 3)
			{
				//動きを止める
				//TimeScaleで制御できるのはタイマーにより制御されている処理だけ
				Time.timeScale = 0;	
				// Fire1ボタンを押してタイトルに戻すようにする
				if (Input.GetButtonDown ("Fire1"))
				{
					//Application.LoadLevel("Start");
					SceneManager.LoadScene ("Start");
					Time.timeScale = 1;
				}
			}
			break;

			
		//default:
		//	break;
		}
	}

	private void NextScene(){
		SceneManager.LoadScene (StageManager.Instance.StageName[StageManager.Instance.StageNo+1]);
	}

	private void GameOver(){
		SceneManager.LoadScene ("GameOver");
	}

	private void END(){
		SceneManager.LoadScene ("END");
	}
}

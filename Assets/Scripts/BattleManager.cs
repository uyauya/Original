using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour {

	int battleStatus;

	const int BATTLE_START = 0;
	const int BATTLE_PLAY  = 1;
	const int BATTLE_END   = 2;
	float timer;						//時間計測洋
	public Image messageStart;
	public Image messageWin;
	public Image messageLose;
	public static int score;			//敵を倒した数。Enemyスクリプトでカウントアップ  

	public int PlayerNo;				//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	private int ItemCount;				//アイテム取得数をカウント
	PlayerController playerController;

	int clearScore;						//クリア条件となるスコア  

	void Start () {	
		DontDestroyOnLoad(this.gameObject);
		battleStatus = BATTLE_START;	//時間0秒、最初にスタートを表示させる
		timer = 0;
		//スタート時はStartは表示、WinとLoseは非表示
		messageStart.enabled = true;
		messageWin.enabled = false;
		messageLose.enabled = false;
		score = 0;
		playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController> ();
		//敵の最大生成数をクリア数にする
		//instantiateValueに値を代入するのをBattleManagerより早くするため
		//EnemyスクリプトにはStartでなくAwakeに記入する（起動直後に処理）
		clearScore = EnemyInstantiate.instantiateValue;
		score = 0;
	}

	void Update () {
	
		switch (battleStatus) {
		
		case BATTLE_START:
			//時間経過でメッセージを消して状態移行
			timer += Time.deltaTime;
				if (timer > 3) {
				messageStart.enabled = false;
				battleStatus = BATTLE_PLAY;
				timer = 0;
			}
			break;
			
		case BATTLE_PLAY:
			//プレイヤーの体力が0以下になったら敗北
			if (PlayerAp.armorPoint <= 0) {
				battleStatus = BATTLE_END;
				messageLose.enabled = true;
			}
			// プレイヤーのアイテム（グリーンスフィア）取得数が1以上ならボス面に移行
			if (playerController.ItemCount >= 1) {
				SceneManager.LoadScene ("STAGE02BOSS");
			}	
			break;
			
		case BATTLE_END:
			// スコアが10000点以上ならボスステージクリア
			if (score >= 10000) {
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
					Application.LoadLevel("Start");					
					Time.timeScale = 1;
				}
			}
			break;

			
		default:
			break;
		}
	}
}

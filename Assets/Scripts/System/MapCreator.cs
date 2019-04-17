using UnityEngine;
using System.Collections;

//MAP自動作成用（MaAxis、MapArrayスクリプト連動）
//キャラクタが進むにつれて前方に足場を自動的に作っていく）
public class MapCreator : MonoBehaviour {
	public int				MAP_SIZE_X = 7;		//マップ横幅 (偶数指定の場合は、自動的に奇数にされる)
	public int				MAP_SIZE_Z = 10;	//マップ奥幅 (偶数指定の場合は、自動的に奇数にされる)
	public GameObject		player;				//プレイヤーオブジェクト格納用
    public GameObject OriginalChara;			//ステージ上に存在するPlayer判別（キャラチェンジ用）
    //public GameObject Utc_sum_humanoid;
    private MapSize		    size;				//マップサイズ型の変数
	private MapAxis			playerAxis;			//プレイヤー座標を扱うPlayerAxis型の変数
	private MapArrayBlock	mapBlock;			//地面用MapArrayBlock型の変数
	private MapArrayFloor	mapFloor;			//地上用MapArrayFloor型の変数
	private GameObject[]	tagObjects;
	public	GameObject[]	prefab_BL;			//床ブロック格納用のプレファブ配列
	public	GameObject[]	prefab_WALL;		//壁ブロック格納用のプレファブ配列
	public	GameObject[]	prefab_enemy;		//敵の格納用のプレファブ配列
	public float[] ApperanceRate;				//敵の出現割合
	public float EmitterTime = 1.0f;			//敵が出現するまでの時間
	public	GameObject[]	prefab_BreakBlock;	//壊せるブロック格納用のプレファブ配列
	public	GameObject[]	prefab_BombPoint;
	private float timer = 0.0f;					//グリーンスフィア取得計算用???
	private float interval = 2.0f;				//グリーンスフィア取得計算用???
	public GameObject[] 	Prefab_Player;
	public	GameObject		Boss02;	
    private int CurrentLevel;
    public BattleManager battleManager;

    // 起動時一番最初に選んだプレイヤーをマップに作成。（プレイヤーはDataManagerクリプトで判別・管理）
    void Awake(){
		player = Instantiate (Prefab_Player [DataManager.PlayerNo]);
		//OriginalChara = player;			//作成したプレイヤーをOriginalChara
        battleManager.Player = player;
        //OriginalChara.SetActive (true);
    }

	void Start(){
		
		size		= new MapSize(MAP_SIZE_X , MAP_SIZE_Z);								// MapSizeクラスのインスタンス生成
		playerAxis	= new MapAxis(battleManager, size , prefab_BL[0].transform.localScale);	// PlayerAxisクラスのインスタンス生成
		mapBlock	= new MapArrayBlock(prefab_BL , "BL" , size , playerAxis);			// 地面用MapArrayクラスのインスタンス生成
		mapFloor	= new MapArrayFloor("FL" , size , playerAxis);						// 地上用MapArrayクラスのインスタンス生成
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		mapFloor.setWall(prefab_WALL);					// 壁オブジェクトの渡す
		mapFloor.setObstacle(prefab_BreakBlock);		// 障害物オブジェクトを渡す
		//mapFloor.setObstacle(prefab_BombPoint);		// 起爆スイッチを渡す
		mapFloor.setEnemy(prefab_enemy);				// 敵オブジェクトを渡す
		initialize();									// プレイヤー位置／マップ初期化（下記参照）
		StartCoroutine("enemyEmitter" , EmitterTime);	// EmitterTimeの時間ごとに敵出現用のコルーチン開始（下記参照）
	}

	// ■■■プレイヤー位置／マップ初期化■■■
	private void initialize(){
		playerAxis.initialize();						// プレイヤーの初期座標を設定 (マップ中央座標)
		mapBlock.startMap_Create();						// スタート時のマップ(床)作成
		mapFloor.startMap_Create();						// スタート時のマップ(地上)作成
	}
	
	// プレイヤーが進むにつれmapBlock生成
	void Update(){
        if(DataManager.PlayerChange == true)
        {
            Vector3 PlayerPos = battleManager.Player.transform.position;
            CurrentLevel = DataManager.Level;
            Destroy(battleManager.Player);
            if (DataManager.PlayerNo == 0)				//新たなプレイヤー発生
			{
            player = Instantiate (Prefab_Player [DataManager.PlayerNo = 3]);
            battleManager.Player = player;
            battleManager.Player.transform.position = PlayerPos;
            DataManager.Level = CurrentLevel;
            DataManager.PlayerChange = false;
            //Debug.Log("レベル" + DataManager.Level);
            }
        }

        playerAxis.updateAxis ();						// プレイヤー座標を更新
		mapBlock.renewal ();							// マップ(床)の更新
		mapFloor.renewal ();							// マップ(地上)の更新
		
		timer += Time.deltaTime;						// グリーンスフィア取得数計算 BattleManagerスクリプト参照???
		if (timer >= interval) {
			Check ("Item3");
			timer = 0;
		}
	
	}

	void Check(string tagname){
		tagObjects = GameObject.FindGameObjectsWithTag(tagname);	// タグ判別用スクリプト
	}

	// ■■■敵出現用のコルーチン■■■
	IEnumerator enemyEmitter(float time){
		while(true){
			//敵が複数出現の場合は0.0～1.0で割合を決める
			//(0.1,0.9)や(0.4,0.6)など合計が1.0になるよう割り振る
			float num = Random.Range (0.0f, 1.0f);
			//敵が1種類の場合は何もしない（割り振らないので1.0のまま）
			if (ApperanceRate.Length == 0) {
				;//何もしない
			} else if (num < ApperanceRate [0]) {
				GameObject.Instantiate (prefab_enemy[0]);
			} else if (num < ApperanceRate [1]) {
				GameObject.Instantiate (prefab_enemy[1]);
			} 
			mapFloor.enemyArrival ();
			yield return new WaitForSeconds(time);		// time秒、処理を待機.
		}
	}

}
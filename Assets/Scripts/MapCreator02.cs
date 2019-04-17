using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator02 : MonoBehaviour
{
	public GameObject	player;				//プレイヤーオブジェクト格納用
	public GameObject[] Prefab_Player;
	private int CurrentLevel;
	public	GameObject	Boss;	
	public BattleManager battleManager;
	public GameObject StartPosition ;		// StartPositionオブジェクト格納
	public GameObject GoalPosition;		// GoalPositionオブジェクト格納

	// 起動時一番最初に選んだプレイヤーをマップに作成。（プレイヤーはDataManagerクリプトで判別・管理）
	void Awake(){
		player = Instantiate (Prefab_Player [DataManager.PlayerNo]);
		battleManager.Player = player;
		player.transform.position = StartPosition.transform.position;
	}
		
    // Start is called before the first frame update
    void Start()
    {
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

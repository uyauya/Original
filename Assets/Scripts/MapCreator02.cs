using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator02 : MonoBehaviour
{
	public GameObject	player;				//プレイヤーオブジェクト格納用
	//public GameObject[] Prefab_Player;
	private int CurrentLevel;
	public	GameObject	Boss;	
	public BattleManager battleManager;
	public GameObject StartPosition ;		// StartPositionオブジェクト格納
	public GameObject GoalPosition;		    // GoalPositionオブジェクト格納
    public GameObject IemBox;
		
    // Start is called before the first frame update
    void Start()
    {
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		GameObject player = GameObject.FindWithTag ("Player");
		player = battleManager.Player;
        player.transform.position = StartPosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

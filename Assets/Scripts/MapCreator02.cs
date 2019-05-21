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

	/*MapCreatorでのカメラとプレイヤーの初期位置の座標が↓なので、MapCreator02でStartPositionを任意で配置する場合は
	 StartPositionからPlayerを引いた値をMainCamera、SubCameraから引く（引くのはInspectorのPositionのみ）
	 
	MainCameraの座標が　X12　Y5　Z16		SubCameraの座標が　X12  Y12  Z20      PlayerがX12　Y0.001  Z20
						X30  Y0  Z0						   	　 X90  Y0   Z0				  X0   Y0      Z0
						X1   Y1  Z1     					   X1   Y1   Z1  			  X1　 Y1　　　Z1　　　*/
		
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

	/*private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Goal")
		{
			NextScene = true;
		}
	}*/
}

using UnityEngine;
using System.Collections;

// プレイヤを自動追尾するメインカメラ（正面視点）
public class CameraFollow : MonoBehaviour 
{
	//public Transform target;
	//public float height;
	private Vector3 offset;
    public BattleManager battleManager;


	/*MapCreatorでのカメラとプレイヤーの初期位置の座標が↓なので、MapCreator02でStartPositionを任意で配置する場合は
	 StartPositionからPlayerを引いた値をMainCamera、SubCameraから引く（引くのはInspectorのPositionのみ）
	 
	MainCameraの座標が　X12　Y5　Z16		SubCameraの座標が　X12  Y12  Z20      PlayerがX12　Y0.001  Z20
						X30  Y0  Z0						   	　 X90  Y0   Z0				  X0   Y0      Z0
						X1   Y1  Z1     					   X1   Y1   Z1  			  X1　 Y1　　　Z1　　　*/
	  

    void Start ()
	{
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		// Playerタグの付いたオブジェクトの位置をtargetとして取得
		// カメラとターゲット（プレイヤー)の距離を設定
		offset = transform.position - battleManager.Player.transform.position;
        
	}
		
	void LateUpdate ()
	{
        
        // カメラがターゲット（プレイヤー）を見つけてから追いかける（少し遅れて追いかける）
        transform.position = battleManager.Player.transform.position + offset;

	}
}


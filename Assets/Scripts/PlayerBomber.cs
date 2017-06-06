using UnityEngine;
using System.Collections;

public class PlayerBomber : MonoBehaviour {

	public	GameObject	prefab_bom;					// 手榴弾

	// ボムによる攻撃	
	private void attack02_bom(){		
		Vector3 pos = transform.position + transform.TransformDirection(Vector3.forward);		// プレイヤー位置　+　プレイヤー正面にむけて１進んだ距離		
		GameObject bom = Instantiate(prefab_bom , pos , Quaternion.identity) as GameObject;		// 手榴弾を作成		
		Vector3 bom_speed = transform.TransformDirection(Vector3.forward)  * 5;		// 手榴弾の移動速度。『プレイヤー正面に向けての速度ベクトル』を５。		
		bom_speed += Vector3.up * 5;			// 手榴弾の『高さ方向の速度』を加算		
		bom.GetComponent< Rigidbody >().velocity = bom_speed;		// 手榴弾の速度を代入		
		bom.GetComponent< Rigidbody >().angularVelocity = Vector3.forward * 7;	// 手榴弾を回転速度を代入.	
	}
	void Start () {
	
	}
	

	void Update () {
	
	}
}

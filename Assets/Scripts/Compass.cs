using UnityEngine;
using System.Collections;
// ユーザーインターフェイスをスクリプトで制御する為追加
using UnityEngine.UI;

public class Compass : MonoBehaviour {
	
	public Image compassImage;
	
	void Start () {
	
	}

	void Update () {
	
		//方角の回転
		//3次元のプレイヤーのY軸の回転情報を、2次元のコンパス画像のZ軸に渡して、
		// プレイヤーの回転に合わせてコンパスを回転させる
		compassImage.transform.rotation =  Quaternion.Euler(compassImage.transform.rotation.x, 
		                            compassImage.transform.rotation.y, transform.eulerAngles.y);
	}
}

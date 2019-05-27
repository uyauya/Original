using UnityEngine;
using System.Collections;
using UnityEngine.UI;

//敵探知レーダー用コンパス
public class Compass : MonoBehaviour {
	
	public Image compassImage;
	
	void Start () {
		compassImage = GameObject.Find ("Compass").GetComponent<Image> ();
	}

	void Update () {
	
		//方角の回転
		//3次元のプレイヤーのY軸の回転情報を、2次元のコンパス画像のZ軸に渡して、
		// プレイヤーの回転に合わせてコンパスを回転させる
		compassImage.transform.rotation =  Quaternion.Euler(compassImage.transform.rotation.x, 
		                            compassImage.transform.rotation.y, transform.eulerAngles.y);
	}
}

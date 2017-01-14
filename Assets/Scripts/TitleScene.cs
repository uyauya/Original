using UnityEngine;
using System.Collections;
using UnityEngine.UI;		// UIを使う時は追加する

public class TitleScene : MonoBehaviour {

	public Text blinkText;

	void Start () {	
	}

	void Update () {	
		//何かボタンを押したら遷移（""の中に遷移したいScene名を入れる）
		//画面遷移させるにはメニューからFile→BuildSettings→Projectビューで
		//遷移したい順番にSceneをドラッグ＆ドロップ
		if (Input.anyKeyDown) {
			Application.LoadLevel("Main");
		}		
		//ボタンを押させるためのメッセージを点滅させる
		// PingPongで点滅する
		blinkText.color = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1));
	}
}

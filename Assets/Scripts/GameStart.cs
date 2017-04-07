using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;		// UIを使う時は追加する

public class GameStart : MonoBehaviour {

	public Text blinkText;
	public void Onclick_Select()
	{
		// ボタンを押してセレクト画面に移行
		SceneManager.LoadScene("Select");
		// テキスト点滅
		blinkText.color = new Color(0, 0, 0, Mathf.PingPong(Time.time, 1));
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;		// UIを使う時は追加する

public class GameStart : MonoBehaviour {
	private AudioSource[] audioSources;
	public Text blinkText;
	public void Onclick_Select()

	{
		// ボタンを押してセレクト画面に移行
		audioSources [1].PlayDelayed(5.0f);
		SceneManager.LoadScene("Select");
		// テキスト点滅
		blinkText.color = new Color(0, 0, 0, Mathf.PingPong(Time.time, 1));

	}

	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
	}
}
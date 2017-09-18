using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;						

public class Config : MonoBehaviour {
	private AudioSource[] audioSources;	
	public void Onclick_Config ()
	{
		// ボタンを押してセレクト画面に移行
		SoundManager.Instance.Play(1);	//(1)はElmentの数
		//Invoke("LoadScene",0.3f);
		SceneManager.LoadScene("Config");
		//Debug.Log("コンフィグ");
	}

	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
		SoundManager.Instance.Play(0);
	}

	void Update () {

	}

	//void LoadScene() {
	//	SceneManager.LoadScene("Config");
	//}

}

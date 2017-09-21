using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;						

public class Config : MonoBehaviour {
	private AudioSource[] audioSources;	
	public void Onclick_Config ()
	{
		// ボタンを押してコンフィグ画面に移行
		SoundManager.Instance.Play(1);	//(1)はElmentの数
		//Invoke("LoadScene",0.3f);
		SceneManager.LoadScene("Config");
		//Debug.Log("コンフィグ");
	}

	public void Onclick_Exit ()
	{
		// ボタンを押してポーズ画面に移行
		SoundManager.Instance.Play(1);	//(1)はElmentの数
		SceneManager.LoadScene("Pause");
	}

	public void Onclick_End ()
	{
		// ボタンを押してスタート画面に移行
		SoundManager.Instance.Play(1);	//(1)はElmentの数
		SceneManager.LoadScene("Start");
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

public class SelectEvent : MonoBehaviour {
	private AudioSource[] audioSources;

	//DataManagerスクリプト参照
	public void Onclick_Kohaku() 
	{
		DataManager.PlayerNo = 0;
		SoundManager.Instance.Play(3);
		Invoke("LoadScene1",1.3f);
	}
	public void Onclick_Yuko() 
	{
		DataManager.PlayerNo = 1;
		SoundManager.Instance.Play(4);
		Invoke("LoadScene2",1.3f);
	}
	public void Onclick_Misaki() 
	{
		DataManager.PlayerNo = 2;
		SoundManager.Instance.Play(5);
		Invoke("LoadScene3",1.3f);
	}

	void Start () {
		SoundManager.Instance.Play(0);
		SoundManager.Instance.Play(1);
		SoundManager.Instance.Play(2);
	}

	void LoadScene1() {
		SceneManager.LoadScene("Kohaku");
	}
	void LoadScene2() {
		SceneManager.LoadScene("Yuko");
	}
	void LoadScene3() {
		SceneManager.LoadScene("Misaki");
	}
}

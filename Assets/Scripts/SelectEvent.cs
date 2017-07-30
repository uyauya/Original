using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

public class SelectEvent : MonoBehaviour {
	private AudioSource[] audioSources;

	//BattleManagerスクリプト参照
	public void Onclick_Kohaku() 
	{
		//GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo = 0;
		DataManager.PlayerNo = 0;
		SoundManager.Instance.Play(3);
		//SoundManager2.Instance.Play(3);
		Invoke("LoadScene1",1.3f);
		//SceneManager.LoadScene("Kohaku");
	}
	public void Onclick_Yuko() 
	{
		//GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo = 1;
		DataManager.PlayerNo = 1;
		SoundManager.Instance.Play(4);
		//SoundManager2.Instance.Play(4);
		Invoke("LoadScene2",1.3f);
		//SceneManager.LoadScene("Yuko");
	}
	public void Onclick_Misaki() 
	{
	    //GameObject.Find ("BattleManager").GetComponent<BattleManager> ().PlayerNo = 2;
		DataManager.PlayerNo = 2;
		SoundManager.Instance.Play(5);
		//SoundManager2.Instance.Play(5);
		Invoke("LoadScene3",1.3f);
		//SceneManager.LoadScene("Misaki");
	}

	void Start () {
		//audioSources = gameObject.GetComponents<AudioSource> ();
		SoundManager.Instance.Play(0);
		SoundManager.Instance.Play(1);
		SoundManager.Instance.Play(2);
		//SoundManager2.Instance.Play(0);
		//SoundManager2.Instance.Play(1);
		//SoundManager2.Instance.Play(2);
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

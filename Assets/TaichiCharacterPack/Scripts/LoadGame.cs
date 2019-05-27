using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

public class LoadGame : MonoBehaviour {

	private AudioSource[] audioSources;
	public void Onclick_SaveSlot01() 
	{
		Invoke("LoadScene3",1.3f);
	}
	public void Onclick_SaveSlot02() 
	{
		Invoke("LoadScene3",1.3f);
	}

	// Use this for initialization
	void Start()
	{
		audioSources = gameObject.GetComponents<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
	}

	void LoadScene01() {

		UserParam userParam = DataManager.userParam;
		if (userParam != null) {
			SceneManager.LoadScene (StageManager.Instance.StageName [userParam.StageNo]);
		}
	}
	void LoadScene02() {

		UserParam userParam = DataManager.userParam;
		if (userParam != null) {
			SceneManager.LoadScene (StageManager.Instance.StageName [userParam.StageNo]);
		}
	}
}

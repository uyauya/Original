using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

public class LoadLoad : MonoBehaviour {

	private AudioSource[] audioSources;
	public void Onclick_Select ()
	{
		// ボタンを押してロードポイント画面に移行
		//SoundManager00.Instance.Play(1);	//(1)はElmentの数
		//GetComponent<Animator>().SetBool("START", true);
		Invoke("LoadScene",1.3f);			//1.3秒後LoadScene起動（下記参照）
	}

	// Use this for initialization
	void Start()
	{
		audioSources = gameObject.GetComponents<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {

	}

	void LoadScene() {
		//LoadPointシーンに移行
		SceneManager.LoadScene("LoadPoint");
	}
}

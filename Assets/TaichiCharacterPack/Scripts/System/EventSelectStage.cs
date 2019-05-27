using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

//ステージセレクトシーン移行用
public class EventSelectStage : MonoBehaviour {
	//private AudioSource[] audioSources;
	private Animator animator;
	//public Text blinkText;					//点滅させる
	public void Onclick_Select ()
	{
		animator.SetBool("START", true);     
		//DataManager.Continue = false;
		//DataManager.FarstLevel = true;
		// ボタンを押してセレクト画面に移行
		SoundManager00.Instance.Play(1);	//(1)はElmentの数
		//1.3秒後LoadScene起動
		Invoke("LoadScene",1.3f);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadScene() {
		// StageSelectシーンへ移行
		SceneManager.LoadScene("StageSelect");
	}
}

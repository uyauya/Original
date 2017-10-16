using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

public class GameStart : MonoBehaviour {
	private AudioSource[] audioSources;
	private Animator animator;
	public Text blinkText;					//点滅させる
	public void Onclick_Select ()


	{
		// ボタンを押してセレクト画面に移行
		SoundManager00.Instance.Play(1);	//(1)はElmentの数
		Invoke("LoadScene",1.3f);
	}
		
	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
		animator = GetComponent<Animator> ();
		SoundManager00.Instance.Play(0);
	}

	void Update () {
		// テキスト点滅
		if (Input.GetMouseButtonDown(0)){
			animator.SetBool ("START", true);
		} else {
			animator.SetBool ("START", false);
		}
	}

	void LoadScene() {
		SceneManager.LoadScene("Select");
	}

}
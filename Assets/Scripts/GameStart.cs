using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;		// UIを使う時は追加する

public class GameStart : MonoBehaviour {
	private AudioSource[] audioSources;
	//private Animator START;
	private Animator animator;
	public Text blinkText;
	public void Onclick_Select ()


	{
		// ボタンを押してセレクト画面に移行
		//audioSources [1].PlayDelayed(5.0f);
		SoundManager.Instance.Play(1);	//(1)はElmentの数
		Invoke("LoadScene",1.3f);
		//SceneManager.LoadScene("Select");
	}

	void Awake(){
		//START = GameObject.Find ("START").GetComponent<Animator> ();
	}


	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
		animator = GetComponent<Animator> ();
		SoundManager.Instance.Play(0);
	}

	void Update () {
		// テキスト点滅
		if (Input.GetMouseButtonDown(0)){
			animator.SetBool ("START", true);
			//blinkText.color = new Color(0, 0, 0, Mathf.PingPong(Time.time * 10, 1f));
		} else {
			animator.SetBool ("START", false);
			//blinkText.color = new Color(0, 0, 0);
		}
	}

	void LoadScene() {
		SceneManager.LoadScene("Select");
	}

}
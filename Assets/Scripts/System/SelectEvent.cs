﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

// シーン移行用
public class SelectEvent : MonoBehaviour {
	private AudioSource[] audioSources;

	public void Onclick_Kohaku() 
	{
		DataManager.PlayerNo = 0;			// DataManagerからPlayerNoを取得
		SoundManager00.Instance.Play(3);	// SoundManager00から3の音を鳴らして
		Invoke("LoadScene1",1.3f);			// 1.3秒後にLoadScene1（kohakuシーンに移行）※下記参照
	}
	public void Onclick_Yuko() 
	{
		DataManager.PlayerNo = 1;
		SoundManager00.Instance.Play(4);
		Invoke("LoadScene2",1.3f);
	}
	public void Onclick_Misaki() 
	{
		DataManager.PlayerNo = 2;
		SoundManager00.Instance.Play(5);
		Invoke("LoadScene3",1.3f);
	}

	/*public void Onclick_Config ()
	{
		// ボタンを押してコンフィグ画面に移行
		SoundManager.Instance.Play(1);	//(1)はElmentの数
		//Invoke("LoadScene",0.3f);
		SceneManager.LoadScene("Config");
		//Debug.Log("コンフィグ");
	}*/

	/*public void Onclick_Exit ()
	{
		// ボタンを押してポーズ画面に移行
		//SoundManager.Instance.Play(1);	//(1)はElmentの数
		SceneManager.LoadScene("Start");
	}*/

	public void Onclick_End ()
	{
		// ボタンを押してスタート画面に移行
		//SoundManager.Instance.Play(1);	//(1)はElmentの数
		SceneManager.LoadScene("Start");
		//Application.LoadLevelAdditive("");
	}

	public void Onclick_Save ()
	{
		// ボタンを押してセーブ画面に移行
		//SoundManager.Instance.Play(1);	//(1)はElmentの数
		SceneManager.LoadScene("Save");
		//Application.LoadLevelAdditive("");
	}

	public void Onclick_Load ()
	{
		// ボタンを押してロード画面に移行
		//SoundManager.Instance.Play(1);	//(1)はElmentの数
		SceneManager.LoadScene("Save");
		//Application.LoadLevelAdditive("");
	}

	void Start () {
		SoundManager00.Instance.Play(0);
		SoundManager00.Instance.Play(1);
		SoundManager00.Instance.Play(2);
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
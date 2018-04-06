﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

public class StageSelectManager : MonoBehaviour {
	private AudioSource[] audioSources;
	public GameObject LockText;			// ステージクリア前のに表示テキスト
	public GameObject UnLockText;		// ステージクリアしてない時に表示
	public int MyStageNo;				// ボタンにつけたステージの番号

	// Use this for initialization
	void Start () {
		//ボタンに付けたステージ番号がClearSceneより大きければクリア済とみなして
		//ステージ移行可能表示にする。
		if (MyStageNo <= DataManager.ClearScene) {
			LockText.SetActive (false);
			UnLockText.SetActive (true);
		} else {
			LockText.SetActive (true);
			UnLockText.SetActive (false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//ステージセレクトで指定したボタン押して
	public void StageSelectButton(int stageNo)
	{
		if (stageNo <= DataManager.ClearScene) {
			//BattleManagerのstageNoに入れた数値の面へ移動
			UserParam userParam = DataManager.userParam;
			SceneManager.LoadScene (StageManager.Instance.StageName [stageNo]);

		}
	}


	public void Onclick_Start ()
	{
		// ボタンを押してスタート画面に移行
		SceneManager.LoadScene("Start");
	}
}

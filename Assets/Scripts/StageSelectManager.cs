using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

public class StageSelectManager : MonoBehaviour {
	private AudioSource[] audioSources;

	public GameObject[] stageButtons;	// ステージセレクトボタン格納

	// Use this for initialization
	void Start () {
		//DataManager.Instance.ClearScene;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//ステージセレクトで指定したボタン押して
	public void StageSelectButton(int stageNo)
	{
		//BattleManagerのstageNoに入れた数値の面へ移動
		SceneManager.LoadScene (StageManager.Instance.StageName[stageNo]);
	}


	public void Onclick_Start ()
	{
		// ボタンを押してスタート画面に移行
		SceneManager.LoadScene("Start");
	}
}

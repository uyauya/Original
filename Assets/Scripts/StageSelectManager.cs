using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

//ステージセレクト用
public class StageSelectManager : MonoBehaviour {
	private AudioSource[] audioSources;
	public GameObject LockText;			// ステージクリア前の表示
	public GameObject UnLockText;		// ステージクリア後の表示
	public int MyStageNo;				// ボタンにつけたステージの番号

	// Use this for initialization
	void Start () {
		//ClearSceneがMyStageNoより大きければクリア済とみなして
		//ステージ移行可能の表示にする。
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

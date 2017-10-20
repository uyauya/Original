using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {



	// Use this for initialization
	void Start()
	{
		//UserParamインスタンスを文字列に変換
		UserParamSaveJson = JsonUtility.ToJson(UserParam);
		//セーブ
		PlayerPrefs.SetString("UserParam",UserParamSaveJson);
		//ロード
		string userParamLoadJson = PlayerPrefs.GetString("UserParam");
		//データを変数に設定
		UserParam = JsonUtility.FromJson<UserParam>(UserParamLoadJson);
	}

	// Update is called once per frame
	void Update () {

	}

	void UserParamSaveJson()
	{
		
}

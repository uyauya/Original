using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {



	// Use this for initialization
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update () {

	}

	public void SaveData (UserParam userParam) 
	{
		//UserParam userParam = GetComponent<UserParam> ();
		//UserParamインスタンスを文字列に変換
		string UserParamSaveJson = JsonUtility.ToJson(userParam);
		//セーブ
		PlayerPrefs.SetString("UserParam",UserParamSaveJson);
		Debug.Log (UserParamSaveJson);
	}

	public void LoadData()
	{
		UserParam userParam = GetComponent<UserParam> ();
		//ロード
		string UserParamLoadJson = PlayerPrefs.GetString ("UserParam");
		//データを変数に設定
		userParam = JsonUtility.FromJson<UserParam> (UserParamLoadJson);
	}
}

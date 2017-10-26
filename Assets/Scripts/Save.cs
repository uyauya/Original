using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Save : MonoBehaviour {

	// Use this for initialization
	void Start()
	{
		// シーン移動してもPlayerLevelを残しておく
		DontDestroyOnLoad(this.gameObject);
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
		//Debug.Log (UserParamSaveJson);
	}

	public UserParam LoadData()
	{
		//UserParam userParam = GetComponent<UserParam> ();
		//ロード
		// Jsonの文字列データをUserParamインスタンスに変換
		string UserParamLoadJson = PlayerPrefs.GetString ("UserParam");
		//データを変数に設定
		UserParam userParam = JsonUtility.FromJson<UserParam> (UserParamLoadJson);
		return userParam;
	}
}

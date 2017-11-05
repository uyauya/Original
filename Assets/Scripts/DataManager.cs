using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シーンをまたいでデータ保持する処理
public class DataManager : MonoBehaviour {
	[System.NonSerialized]
	public static int PlayerNo;			//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	public static UserParam userParam;
	public static bool FarstLevel;
	//public static int SceneNo;		//ステージNo取得用


	// Use this for initialization
	void Start () {
		// シーン移動してもPlayerNoを残しておく
		DontDestroyOnLoad(this.gameObject);
		FarstLevel = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SaveData () 
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
		userParam = JsonUtility.FromJson<UserParam> (UserParamLoadJson);
		return userParam;
	}
}

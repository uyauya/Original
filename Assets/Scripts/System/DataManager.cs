
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーンをまたいでデータ保持する処理
public class DataManager : SingletonMonoBehaviour<DataManager> {
	[System.NonSerialized]
	public static int PlayerNo;			//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	public static UserParam userParam;
	public static bool FarstLevel;
	public static bool Continue = false;
	public static int Level;
	public static int AttackPoint;
	public static float BoostPointMax;
	public static float ArmorPointMax;
	public static int Score;
	public static string SceneName;

	// Use this for initialization
	void Start () {
		// シーン移動してもPlayerNoを残しておく
		//if (GameObject.Find ("DataManager") == null) {
			DontDestroyOnLoad (this.gameObject);
			FarstLevel = false;
		//}
		userParam = new UserParam ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// セーブ時の処理
	public void SaveData () 
	{
		UserParam instanse = new UserParam (
			DataManager.PlayerNo,
			DataManager.Level,
			DataManager.AttackPoint,
			DataManager.BoostPointMax,
			DataManager.ArmorPointMax,
			DataManager.Score,
			SceneManager.GetActiveScene ().name,
			StageManager.Instance.StageNo
		);
		//UserParamインスタンスを文字列に変換
		string UserParamSaveJson = JsonUtility.ToJson(instanse);
		//セーブ
		PlayerPrefs.SetString("UserParam",UserParamSaveJson);
		//Debug.Log (UserParamSaveJson);
	}

	// ロード時の処理
	public void LoadData()
	{
		// Jsonの文字列データをUserParamインスタンスに変換
		string UserParamLoadJson = PlayerPrefs.GetString ("UserParam");
		//データを変数に設定してロード
		UserParam instanse = JsonUtility.FromJson<UserParam> (UserParamLoadJson);
		DataManager.PlayerNo = instanse.PlayerNo;
		DataManager.Level = instanse.Level;
		DataManager.AttackPoint = instanse.AttackPoint;
		DataManager.BoostPointMax = instanse.boostPointMax;
		DataManager.ArmorPointMax = instanse.armorPointMax;
		DataManager.Score = instanse.Score;
		SceneManager.LoadScene(StageManager.Instance.StageName[StageManager.Instance.StageNo]);
	}
}

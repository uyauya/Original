
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーンをまたいでデータ保持する処理

public class DataManager : SingletonMonoBehaviour<DataManager> {
	[System.NonSerialized]
	public static int PlayerNo;			//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	public static UserParam userParam;
	public static bool Continue = false;
	public static int Level;
	public static int AttackPoint;
	public static float BoostPointMax;
	public static float ArmorPointMax;
	public static int Score;
	public static string SceneName;
	public static int ClearScene;
	public static bool FarstLevel;

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
	public void SaveData (string SlotName) 
	{
		UserData userData = new UserData ();
		//UserParam instance = new UserParam (
		userData.PlayerNo = PlayerNo;
		userData.Level = Level;
		userData.AttackPoint = AttackPoint;
		userData.BoostPointMax = BoostPointMax;
		userData.ArmorPointMax = ArmorPointMax;
		userData.Score = Score;
		//userData.SceneName = SceneManager.GetActiveScene ().name;
		userData.SceneName = StageManager.Instance.StageName[StageManager.Instance.StageNo];
		userData.ClearScene = ClearScene;
		//StageManager.Instance.StageNo;
			//this.ClearScene
		//);
		//UserParamインスタンスを文字列に変換
		string UserParamSaveJson = JsonUtility.ToJson(userData);
		Debug.Log (UserParamSaveJson);
		//セーブ
		PlayerPrefs.SetString("UserParam" + SlotName,UserParamSaveJson);
		//Debug.Log (UserParamSaveJson);
	}

	// ロード時の処理
	public void LoadData(string SlotName)
	{
		// Jsonの文字列データをUserParamインスタンスに変換
		string UserParamLoadJson = PlayerPrefs.GetString ("UserParam" + SlotName);
		//データを変数に設定してロード
		UserData instance = JsonUtility.FromJson<UserData> (UserParamLoadJson);
		PlayerNo = instance.PlayerNo;
		Level = instance.Level;

		UserParam Param = PlayerLevel.SearchParam (PlayerNo, Level);
		AttackPoint = Param.AttackPoint;
		BoostPointMax = Param.boostPointMax;
		ArmorPointMax = Param.armorPointMax;
		Score = instance.Score;
		SceneName = instance.SceneName;
		ClearScene = instance.ClearScene;
		//SceneManager.LoadScene (SceneName);
		//SceneManager.LoadScene(StageManager.Instance.StageName[StageManager.Instance.StageNo]);
	}
}

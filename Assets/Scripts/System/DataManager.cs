
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
	public static bool Continue = false;
	public static int Level;
	public static int AttackPoint;
	public static int BoostPointMax;
	public static int ArmorPointMax;
	public static int Score;
	public static string SceneName;

	// Use this for initialization
	void Start () {
		// シーン移動してもPlayerNoを残しておく
		DontDestroyOnLoad(this.gameObject);
		FarstLevel = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

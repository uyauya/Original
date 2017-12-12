
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シーンをまたいでデータ保持する処理
public class DataManager : MonoBehaviour {
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
		if (GameObject.Find ("DataManager") == null) {
			DontDestroyOnLoad (this.gameObject);
			FarstLevel = false;
		}
		userParam = new UserParam ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

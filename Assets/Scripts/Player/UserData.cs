﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UserData : MonoBehaviour {
	//[System.NonSerialized]

	public int PlayerNo;			//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	public UserParam userParam;
	//public bool FarstLevel;
	public bool Continue = false;
	public int Level;
	public int AttackPoint;
	public float BoostPointMax;
	public float ArmorPointMax;
	public int Score;
	public string SceneName;
	public int ClearScene;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーパラメータ管理
public class UserParam
{
	public static UserParam instanse;

	public int Level;			// プレイヤーレベル
	public int AttackPoint;		// 攻撃力（PlayerController参照）
	public int boostPointMax;	// ブーストポイント最大値（PlayerController参照）
	public int armorPointMax;	// プレイヤー体力最大値（PlayerAp参照）
	public int Score;			// 点数兼経験値（BattleManager参照）
	public int PlayerNo;		// プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	public string SceneName;

	public UserParam(int Pno, int level, int attackPoint, int boostMax, int armorMax, int score, string sceneName)
	{
		PlayerNo = Pno;				 
		Level = level;
		AttackPoint = attackPoint;
		boostPointMax = boostMax;
		armorPointMax = armorMax;
		Score = score;
		SceneName = sceneName;
		instanse = this;
	}

	public void SaveData () 
	{
		//UserParam userParam = GetComponent<UserParam> ();
		//UserParamインスタンスを文字列に変換
		string UserParamSaveJson = JsonUtility.ToJson(instanse);
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
		instanse = JsonUtility.FromJson<UserParam> (UserParamLoadJson);
		return instanse;
	}
}	

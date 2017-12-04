using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO ※プレイヤーレベル管理	
public class PlayerLevel : MonoBehaviour 
{
	// UserParamをuserParamListとしてリスト化する
	// 順番（pno, level, attackPoint, boostMax, armorMax, scoreの順）にレベルアップ時の数値を設定
	public static List <UserParam> userParamList = new List<UserParam>() 
	{
		new UserParam(0,01,130,3000,3000,1000, string.Empty,0),		//Level01
		new UserParam(2,01,150,3200,2700,1000, string.Empty,0),		//Level01
		new UserParam(1,01,100,3200,3000,1000, string.Empty,0),		//Level01

		new UserParam(0,02,136,3300,3150,1500, string.Empty,0),		//Level02
		new UserParam(2,02,165,3360,2830,1500, string.Empty,0),		//Level02
		new UserParam(1,02,105,3520,3150,1500, string.Empty,0),		//Level02

		new UserParam(0,03,150,3500,3460,2200, string.Empty,0),		//Level03
		new UserParam(2,03,181,3530,2970,2200, string.Empty,0),		//Level03
		new UserParam(1,03,110,3870,3310,2200, string.Empty,0),		//Level03

		new UserParam(0,04,157,3800,3630,3300, string.Empty,0),		//Level04
		new UserParam(2,04,200,3700,3120,3300, string.Empty,0),		//Level04
		new UserParam(1,04,115,4260,3470,3300, string.Empty,0),		//Level04

		new UserParam(0,05,172,4000,4000,5000, string.Empty,0),		//Level05
		new UserParam(2,05,220,3890,3280,5000, string.Empty,0),		//Level05
		new UserParam(1,05,121,4690,3650,5000, string.Empty,0),		//Level05

		new UserParam(0,06,180,4200,4200,7500, string.Empty,0),		//Level06
		new UserParam(2,06,240,4080,3440,7500, string.Empty,0),		//Level06
		new UserParam(1,06,127,5150,3830,7500, string.Empty,0),		//Level06

		new UserParam(0,07,198,4600,4620,11250, string.Empty,0),		//Level07
		new UserParam(2,07,265,4280,3620,11250, string.Empty,0),		//Level07
		new UserParam(1,07,134,5670,4020,11250, string.Empty,0),		//Level07

		new UserParam(0,08,208,4800,4850,16900, string.Empty,0),		//Level08
		new UserParam(2,08,292,4500,3800,16900, string.Empty,0),		//Level08
		new UserParam(1,08,140,6230,4220,16900, string.Empty,0),		//Level08

		new UserParam(0,09,228,5300,5340,26000, string.Empty,0),		//Level09
		new UserParam(2,09,320,4730,3900,26000, string.Empty,0),		//Level09
		new UserParam(1,09,148,6860,4430,26000, string.Empty,0),		//Level09

	};

	public Transform muzzle;
	public GameObject LevelUpPrefab;
	public GameObject LevelUpObject;
	public int PlayerNo;					//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）

	// Use this for initialization
	void Start () {
		// BattleManagerのオブジェクトを見つけてBattleManagerスクリプトのScoreを０にする
		//DataManager.Score = 0;
		//LevelInitialize (DataManager.Level);
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LevelUp() {
		
		// BattleManagerオブジェクトのBattleManagerのScoreをScoreと呼ぶ
		int Score = DataManager.Score;
		foreach(var Param in userParamList)
		{
			//Debug.Log("ParamScore"+Param.Score);
			//Debug.Log("Score"+Score);
			// 
			if (Param.Score <= Score && Score <= 2200) {
				if (Param.PlayerNo == DataManager.PlayerNo) {
					DataManager.Level = Param.Level;
					//Playerのタグがついているオブジェクトを見つけPlayerControllerスクリプトのAttackPointに
					//userParamListのAttackPoint数値を代入する
					Debug.Log ("レベルアップ");
					DataManager.AttackPoint = Param.AttackPoint;
					DataManager.BoostPointMax = Param.boostPointMax;
					DataManager.ArmorPointMax = Param.armorPointMax;
					// （プレイヤーの）muzzleにレベルアップ用エフェクト設置
					LevelUpObject = Instantiate (LevelUpPrefab, muzzle.position, Quaternion.identity);
					if (PlayerNo == 0) {
						SoundManager.Instance.Play (42, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play (43, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play (44, gameObject);
					}
				}
			} else if (Param.Score <= Score && Score > 26000) {
				Debug.Log ("打ち止め");
				return;

			}
		}
	}

	public void LevelInitialize(int level) {
		// （プレイヤーの）muzzleにレベルアップ用エフェクト設置
		//LevelUpObject = Instantiate (LevelUpPrefab, muzzle.position, Quaternion.identity);
		// BattleManagerオブジェクトのBattleManagerのScoreをScoreと呼ぶ
		//int Score = DataManager.Score;
					DataManager.Level = 1;
					//Playerのタグがついているオブジェクトを見つけPlayerControllerスクリプトのAttackPointに
					//userParamListのAttackPoint数値を代入する
		DataManager.AttackPoint = userParamList[0].AttackPoint;
		DataManager.BoostPointMax = userParamList[0].boostPointMax;
		DataManager.ArmorPointMax = userParamList[0].armorPointMax;
		}

}

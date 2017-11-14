using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO ※プレイヤーレベル管理	
public class PlayerLevel : MonoBehaviour 
{
	// UserParamをuserParamListとしてリスト化する
	// 順番（pno, level, attackPoint, boostMax, armorMax, scoreの順）にレベルアップ時の数値を設定
	public List <UserParam> userParamList = new List<UserParam>() 
	{
		new UserParam(0,01,130,3000,3000,1000, string.Empty),		//Level01
		new UserParam(1,01,100,3000,3200,1000, string.Empty),		//Level01
		new UserParam(2,01,150,3200,2700,1000, string.Empty),		//Level01

		new UserParam(0,02,136,3300,3150,1500, string.Empty),		//Level02
		new UserParam(1,02,105,3150,3520,1500, string.Empty),		//Level02
		new UserParam(2,02,165,3360,2830,1500, string.Empty),		//Level02

		new UserParam(0,03,150,3500,3460,2200, string.Empty),		//Level03
		new UserParam(1,03,110,3310,3870,2200, string.Empty),		//Level03
		new UserParam(2,03,181,3530,2970,2200, string.Empty),		//Level03

		new UserParam(0,04,157,3800,3630,2200, string.Empty),		//Level04
		new UserParam(1,04,115,3470,4260,2200, string.Empty),		//Level04
		new UserParam(2,04,200,3700,3120,2200, string.Empty),		//Level04

		new UserParam(0,05,172,4000,4000,2200, string.Empty),		//Level05
		new UserParam(1,05,121,3650,4690,2200, string.Empty),		//Level05
		new UserParam(2,05,220,3890,3280,2200, string.Empty),		//Level05

		new UserParam(0,06,180,4200,4200,2200, string.Empty),		//Level06
		new UserParam(1,06,127,3830,5150,2200, string.Empty),		//Level06
		new UserParam(2,06,240,4080,3440,2200, string.Empty),		//Level06

		new UserParam(0,07,198,4600,4620,2200, string.Empty),		//Level07
		new UserParam(1,07,134,4020,5670,2200, string.Empty),		//Level07
		new UserParam(2,07,265,4280,3620,2200, string.Empty),		//Level07

		new UserParam(0,08,208,4800,4850,2200, string.Empty),		//Level08
		new UserParam(1,08,140,4220,6230,2200, string.Empty),		//Level08
		new UserParam(2,08,292,4500,3800,2200, string.Empty),		//Level08

		new UserParam(0,09,228,5300,5340,2200, string.Empty),		//Level09
		new UserParam(1,09,148,4430,6860,2200, string.Empty),		//Level09
		new UserParam(2,09,320,4730,3900,2200, string.Empty),		//Level09
	};

	public Transform muzzle;
	public GameObject LevelUpPrefab;
	public GameObject LevelUpObject;

	// Use this for initialization
	void Start () {
		// BattleManagerのオブジェクトを見つけてBattleManagerスクリプトのScoreを０にする
		GameObject.Find ("BattleManager").GetComponent<BattleManager> ().Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void LevelUp() {
		// （プレイヤーの）muzzleにレベルアップ用エフェクト設置
		LevelUpObject = Instantiate (LevelUpPrefab, muzzle.position, Quaternion.identity);
		// BattleManagerオブジェクトのBattleManagerのScoreをScoreと呼ぶ
		int Score = GameObject.Find ("BattleManager").GetComponent<BattleManager> ().Score;
		foreach(var Param in userParamList)
		{
			Debug.Log("ParamScore"+Param.Score);
			Debug.Log("Score"+Score);
			// 
			if (Param.Score <= Score) {
				if (Param.PlayerNo == DataManager.PlayerNo) {
					GetComponent<PlayerController> ().Level = Param.Level;
					//Playerのタグがついているオブジェクトを見つけPlayerControllerスクリプトのAttackPointに
					//userParamListのAttackPoint数値を代入する
					Debug.Log("レベルアップ");
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint = Param.AttackPoint;
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().boostPointMax = Param.boostPointMax;
					GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().armorPointMax = Param.armorPointMax;
				}
			}

		}
	}

}

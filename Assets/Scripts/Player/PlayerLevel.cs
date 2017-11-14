using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーレベル管理	
public class PlayerLevel : MonoBehaviour 
{
	// UserParamをuserParamListとしてリスト化する
	// 順番（pno, level, attackPoint, boostMax, armorMax, scoreの順）にレベルアップ時の数値を設定
	public List <UserParam> userParamList = new List<UserParam>() 
	{
		new UserParam(0,01,130,3000,3000,1000, string.Empty),		//Level01
		new UserParam(1,01,110,4000,3800,1000, string.Empty),		//Level01
		new UserParam(2,01,150,3000,4500,1000, string.Empty),		//Level01

		new UserParam(0,02,130,3300,4000,1000, string.Empty),		//Level02
		new UserParam(1,02,110,4000,3800,1000, string.Empty),		//Level02
		new UserParam(2,02,150,3000,4500,1000, string.Empty),		//Level02

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level03
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level03
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level03

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level04
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level04
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level04

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level05
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level05
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level05

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level06
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level06
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level06

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level07
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level07
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level07

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level08
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level08
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level08

		new UserParam(0,03,150,3400,4500,2200, string.Empty),		//Level09
		new UserParam(1,03,130,4300,4200,2200, string.Empty),		//Level09
		new UserParam(2,03,200,3100,4800,2200, string.Empty),		//Level09
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

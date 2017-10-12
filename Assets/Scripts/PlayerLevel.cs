using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserParam
{
	public int Level;
	public int AttackPoint;
	public int boostPointMax;
	public int armorPointMax;
	public int Score;
	public int PlayerNo;

	public UserParam(int Pno, int level, int attackPoint, int boostMax, int armorMax, int score)
	{
		PlayerNo = Pno;
		Level = level;
		AttackPoint = attackPoint;
		boostPointMax = boostMax;
		armorPointMax = armorMax;
		Score = score;
	}
}


	
public class PlayerLevel : MonoBehaviour 
{
	// UserParamをuserParamListとしてリスト化する
	// 順番（pno, level, attackPoint, boostMax, armorMax, scoreの順）にレベルアップ時の数値を設定
	public List <UserParam> userParamList = new List<UserParam>() 
	{
		new UserParam(0,02,150,3300,5000,1100),		//Level02
		new UserParam(1,02,110,4000,4000,1100),		//Level02
		new UserParam(2,02,200,3300,6000,1100),		//Level02

		new UserParam(0,02,150,3300,7000,9000),		//Level03
		new UserParam(1,02,110,4000,4000,9000),		//Level03
		new UserParam(2,02,200,3300,6000,9000),		//Level03
	};

	public Transform muzzle;
	public GameObject LevelUpPrefab;
	public GameObject LevelUpObject;

	// Use this for initialization
	void Start () {
		// BattleManagerのオブジェクトを見つけてBattleManagerスクリプトのScoreを０にする
		GameObject.Find ("BattleManager").GetComponent<BattleManager> ().Score = 0;
		//boostPointMax = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ();
	}
	
	// Update is called once per frame
	void Update () {
		//LevelUp ();
	}

	public void LevelUp() {
		LevelUpObject = Instantiate (LevelUpPrefab, muzzle.position, Quaternion.identity);
		int Score = GameObject.Find ("BattleManager").GetComponent<BattleManager> ().Score;
		foreach(var Param in userParamList)
		{
			if (Param.Score <= Score) {
				if (Param.PlayerNo == 0) {
					//Playerのタグがついているオブジェクトを見つけPlayerControllerスクリプトのAttackPointに
					//userParamListのAttackPoint数値を代入する
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint = Param.AttackPoint;
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().boostPointMax = Param.boostPointMax;
					GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().armorPointMax = Param.armorPointMax;
				}
				if (Param.PlayerNo == 1) {
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint = Param.AttackPoint;
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().boostPointMax = Param.boostPointMax;
					GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().armorPointMax = Param.armorPointMax;
				}
				if (Param.PlayerNo == 2) {
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint = Param.AttackPoint;
					GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().boostPointMax = Param.boostPointMax;
					GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().armorPointMax = Param.armorPointMax;
				}
			}

		}
	}

}

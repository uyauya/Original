using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーパラメータ項目設定
public class UserParam
{
	public int Level;			// プレイヤーレベル
	public int AttackPoint;		// 攻撃力（PlayerController参照）
	public int boostPointMax;	// ブーストポイント最大値（PlayerController参照）
	public int armorPointMax;	// プレイヤー体力最大値（PlayerAp参照）
	public int Score;			// 点数兼経験値（BattleManager参照）
	public int PlayerNo;		// プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照

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


// プレイヤーレベル管理	
public class PlayerLevel : MonoBehaviour 
{
	// UserParamをuserParamListとしてリスト化する
	// 順番（pno, level, attackPoint, boostMax, armorMax, scoreの順）にレベルアップ時の数値を設定
	public List <UserParam> userParamList = new List<UserParam>() 
	{
		new UserParam(0,01,130,3000,3000,1000),		//Level01
		new UserParam(1,01,110,4000,3800,1000),		//Level01
		new UserParam(2,01,150,3000,4500,1000),		//Level01

		new UserParam(0,02,130,3300,4000,1000),		//Level02
		new UserParam(1,02,110,4000,3800,1000),		//Level02
		new UserParam(2,02,150,3000,4500,1000),		//Level02

		new UserParam(0,03,150,3400,4500,2200),		//Level03
		new UserParam(1,03,130,4300,4200,2200),		//Level03
		new UserParam(2,03,200,3100,4800,2200),		//Level03
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
		LevelUpObject = Instantiate (LevelUpPrefab, muzzle.position, Quaternion.identity);
		int Score = GameObject.Find ("BattleManager").GetComponent<BattleManager> ().Score;
		foreach(var Param in userParamList)
		{
			Debug.Log("ParamScore"+Param.Score);
			Debug.Log("Score"+Score);
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

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
	public List <UserParam> userParamList = new List<UserParam>() 
	{
		new UserParam(0,01,100,3000,5000,0000),		//Level01
		new UserParam(1,01,080,3500,4000,0000),		//Level01
		new UserParam(2,01,150,3000,6000,0000),		//Level01

		new UserParam(0,02,150,3300,5000,1100),		//Level02
		new UserParam(1,02,110,4000,4000,1100),		//Level02
		new UserParam(2,02,200,3300,6000,1100),		//Level02
	};
	// Use this for initialization
	void Start () {
		GameObject.Find ("BattleManager").GetComponent<BattleManager> ().Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

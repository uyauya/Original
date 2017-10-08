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
		new UserParam(0,1,100,100,100,100),		//Level01
		new UserParam(1,1,100,100,100,100),		//Level01
		new UserParam(2,1,100,100,100,100),		//Level01

		new UserParam(0,2,150,110,100,100),		//Level02
		new UserParam(1,2,150,120,100,100),		//Level02
		new UserParam(2,2,100,150,100,100),		//Level02
	};
	// Use this for initialization
	void Start () {
		//Score = GameObject.Find ("BattleManager").GetComponent<Score> ();
		//Score = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}

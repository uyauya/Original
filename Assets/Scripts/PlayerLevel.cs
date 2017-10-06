using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserParam
{
	public int Level;
	public int boostPointMax;
	public int armorPointMax;
	public int Score;

	public UserParam(int boostPointMax, int armorPointMax, int Score)
	{
		Level = Level;
		AttackPoint = attackpoint;
		boostPointMax = boostMax;
		armorPointMax = armorMax;
		Score = score;
	}
}
List<UserParam>userParamList = new List<UserParam> 
{
	new UserParam(1,100,100,100,100),		//Level01
	new UserParam(2,100,110,100,100),		//Level02
}
	
public class PlayerLevel : MonoBehaviour 
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
	public float count;		
	private GameObject Rain;	
	private GameObject RainStorm;
	public static bool isRain = false;
	public static bool isRainStorm = false;
	public int CountRain = 5;	
	public int CountRainStorm = 10;
	public int CountNothing = 15;
	public int CountReset = 20;
	private Vector3 offset;
	public BattleManager battleManager;

	void Start () {
		Rain = GameObject.Find ("Rain");
		RainStorm = GameObject.Find ("RainStorm");
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		offset = transform.position - battleManager.Player.transform.position;
	}

	void Update () 
	{
		count += Time.deltaTime;
		// CountAfternoonに設定しているの時間を過ぎたらLightMorningとLightAfternoonを消灯
		if (count > CountRain)
		{
			Rain.SetActive(true);
			RainStorm.SetActive(false);
			isRain = true;
			isRainStorm = false;
		}
		if (count > CountRainStorm)
		{
			Rain.SetActive(true);
			RainStorm.SetActive(true);
			isRain = false;
			isRainStorm = true;
		}
		if (count > CountNothing)
		{
			Rain.SetActive(false);
			RainStorm.SetActive(false);
			isRain = false;
			isRainStorm = false;
		}
		if (count > CountReset)
		{
			count = 0;
		}
	}

	void LateUpdate ()
	{
		transform.position = battleManager.Player.transform.position + offset;
	}
}

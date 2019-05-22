using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDrop : MonoBehaviour
{
	public float count;		
	private GameObject Snowfall;
	private GameObject HeavySnowfall;	
	private GameObject Snowstorm;
	public static bool isSnowfall = false;
	public static bool isHeavySnowfall = false;
	public static bool isSnowstorm = false;
	public int CountSnowfall = 0;	
	public int CountHeavySnowfall = 5;	
	public int CountSnowstorm = 10;
	public int CountNothing = 15;
	public int CountReset = 17;
	private Vector3 offset;
	public BattleManager battleManager;

	void Start () {
		Snowfall = GameObject.Find ("SnowFall");
		HeavySnowfall = GameObject.Find ("HeavySnowFall");
		Snowstorm 	 = GameObject.Find ("SnowStorm");
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		offset = transform.position - battleManager.Player.transform.position;
	}

	void Update () 
	{
		count += Time.deltaTime;
		if (count > CountSnowfall)
		{
			Snowfall.SetActive(true);
			HeavySnowfall.SetActive(false);
			Snowstorm.SetActive(false);
			isSnowfall = true;
			isHeavySnowfall = false;
			isSnowstorm = false;
		}
		if (count > CountHeavySnowfall)
		{
			Snowfall.SetActive(true);
			HeavySnowfall.SetActive(true);
			Snowstorm.SetActive(false);
			isSnowfall = false;
			isHeavySnowfall = true;
			isSnowstorm = false;
		}
		if (count > CountSnowstorm)
		{
			Snowfall.SetActive(true);
			HeavySnowfall.SetActive(true);
			Snowstorm.SetActive(true);
			isSnowfall = false;
			isHeavySnowfall = false;
			isSnowstorm = true;
		}
		if (count > CountNothing)
		{
			Snowfall.SetActive(false);
			HeavySnowfall.SetActive(false);
			Snowstorm.SetActive(false);
			isSnowfall = false;
			isHeavySnowfall = false;
			isSnowstorm = false;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowDrop : MonoBehaviour
{
	public float count;		
	private GameObject HeavySnowfall;	
	private GameObject Snowfall;
	private GameObject Snowstorm;
	public bool isHeavySnowfall = false;
	public bool isSnowfall = false;
	public bool isSnowstorm = false;
	public int CountHeavySnowfall = 5;	
	public int CountSnowfall = 10;	
	public int CountSnowstorm = 15;
	public int CountNothing = 20;

	void Start () {
		HeavySnowfall = GameObject.Find ("HeavySnowfall");
		Snowfall = GameObject.Find ("Snowfall");
		Snowstorm 	 = GameObject.Find ("Snowstorm");
	}

	void Update () {
		count += Time.deltaTime;
		if (count > CountSnowfall) {
			Snowfall.SetActive(isSnowfall == true);
		}
		if (count > CountHeavySnowfall) {
			HeavySnowfall.SetActive(isHeavySnowfall == true);
			Snowfall.SetActive(isSnowfall == false);
		}
		if (count > CountSnowstorm) {
			Snowstorm.SetActive (isSnowstorm == true);
			HeavySnowfall.SetActive(isHeavySnowfall == false);
		}
		if (count > CountNothing) {
			HeavySnowfall.SetActive(isHeavySnowfall == false);
			count = 0;
		}
	}
}

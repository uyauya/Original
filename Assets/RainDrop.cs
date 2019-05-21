using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainDrop : MonoBehaviour
{
	public float count;		
	private GameObject Rain;	
	private GameObject RainStorm;
	public bool isRain = false;
	public bool isRainStorm = false;
	public int CountRain = 5;	
	public int CountRainStorm = 10;	

	void Start () {
		Rain = GameObject.Find ("Rain");
		RainStorm = GameObject.Find ("RainStorm");
	}

	void Update () {
		count += Time.deltaTime;
		if (count > CountRain) {
			Rain.SetActive(isRain == true);
		}
		if (count > CountRainStorm) {
			RainStorm.SetActive(isRainStorm == true);
		}
	}
}

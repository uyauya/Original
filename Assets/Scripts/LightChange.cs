using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 時間経過と共にライトを消していき、朝から夜の時間推移を表現
public class LightChange : MonoBehaviour {
	public float count;
	private GameObject lightAfternoon;
	private GameObject lightEvening;
	private GameObject lightNight;
	private GameObject lightMorning;
	public bool isLightAfternoon = true;
	public bool isLightEvening = true;
	public bool isLightNight = true;
	public bool isLightMorning = true;
	public int CountAfternoon = 5;
	public int CountEvening = 10;
	public int CountNight = 15;
	public int CountMorning = 20;
	public int CountNoon = 25;

	void Start () {
		// 最初に以下3種類のLightを付けておく
		lightAfternoon = GameObject.Find ("LightAfternoon");
		Debug.Log (lightAfternoon);
		lightEvening = GameObject.Find ("LightEvening");
		Debug.Log (lightEvening);
		lightNight 	 = GameObject.Find ("LightNight");
		Debug.Log (lightNight);
		lightMorning = GameObject.Find ("LightMorning");
		Debug.Log (lightMorning);
	}

	void Update () {
		Debug.Log (lightMorning);
		// 時間経過と共にライトを消していく
		count += Time.deltaTime;
		//Debug.Log (lightMorning);
		//Debug.Log (count);
		if (count > CountAfternoon) {
			//Debug.Log (count);
			lightMorning.SetActive(isLightMorning == false);
			// Debug.Log (lightMorning);
			lightAfternoon.SetActive(isLightAfternoon == false);
		}
		if (count > CountEvening) {
			lightEvening.SetActive(isLightEvening == false);
		}
		if (count > CountNight) {
			lightNight.SetActive (isLightNight == false);
		}
		if (count > CountMorning) {
			lightMorning.SetActive (isLightMorning == true);
		}
		if (count > CountNoon) {
			lightAfternoon.SetActive (isLightAfternoon == true);
		// カウントをリセット
			count = 0;
		}
	}
}

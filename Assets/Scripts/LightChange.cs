using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightChange : MonoBehaviour {
	public float count;
	public Text timeLabel;
	private GameObject lightMorning;
	private GameObject lightEvening;
	private GameObject lightNight;

	// Use this for initialization
	void Start () {
		lightMorning = GameObject.Find ("LightMorning");
		lightEvening = GameObject.Find ("LightEvening");
		lightNight 	 = GameObject.Find ("Night");
	}
	
	// Update is called once per frame
	void Update () {
		count += Time.deltaTime;
		timeLabel.text = "" + count.ToString ("0");

		if (count > 10) {
			lightMorning.SetActive (false);
		}
		if (count > 20) {
			lightEvening.SetActive (false);
		}
		if (count > 30) {
			lightNight.SetActive (false);
		}
	}
}

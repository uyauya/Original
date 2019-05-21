using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningDrop : MonoBehaviour
{
	public float count;		
	private GameObject Lightning;	
	private GameObject LightningStrike;
	private GameObject LightningField;
	private GameObject LightningSpark;
	public bool isLightning = false;
	public bool isLightningStrike = false;
	public bool isLightningField = false;
	public bool isLightningSpark = false;
	public int CountLightning = 5;	
	public int CountLightningStrike = 10;	
	public int CountLightningField = 15;		
	public int CountLightningSpark = 20;


	void Start () {
		Lightning = GameObject.Find ("Lightning");
		LightningStrike = GameObject.Find ("LightningStrike");
		LightningField 	 = GameObject.Find ("LightningField");
		LightningSpark = GameObject.Find ("LightningSpark");
	}

	void Update () {
		count += Time.deltaTime;
		if (count > CountLightning) {
			Lightning.SetActive(isLightning == true);
		}
		if (count > CountLightningStrike) {
			LightningStrike.SetActive(isLightningStrike == true);
		}
		if (count > CountLightningField) {
			LightningField.SetActive (isLightningField == true);
		}
		if (count > CountLightningSpark) {
			LightningSpark.SetActive (isLightningSpark == true);
			count = 0;
		}
	}
}

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
	public int CountNothing = 25;
	public int CountReset = 27;
	private Vector3 offset;
	public BattleManager battleManager;

	void Start () {
		Lightning = GameObject.Find ("Lightning");
		LightningStrike = GameObject.Find ("LightningStrike");
		LightningField 	 = GameObject.Find ("LightningField");
		LightningSpark = GameObject.Find ("LightningSpark");
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		offset = transform.position - battleManager.Player.transform.position;
	}

	void Update () 
	{
		count += Time.deltaTime;
		// CountAfternoonに設定しているの時間を過ぎたらLightMorningとLightAfternoonを消灯
		if (count > CountLightning)
		{
			Lightning.SetActive(true);
			LightningStrike.SetActive(false);
			LightningField.SetActive(false);
			LightningSpark.SetActive(false);
			isLightning = true;
			isLightningStrike = false;
			isLightningField = false;
			isLightningSpark = false;
		}
		if (count > CountLightningStrike)
		{
			Lightning.SetActive(false);
			LightningStrike.SetActive(true);
			LightningField.SetActive(false);
			LightningSpark.SetActive(false);
			isLightning = false;
			isLightningStrike = true;
			isLightningField = false;
			isLightningSpark = false;
		}
		if (count > CountLightningField)
		{
			Lightning.SetActive(false);
			LightningStrike.SetActive(false);
			LightningField.SetActive(true);
			LightningSpark.SetActive(false);
			isLightning = false;
			isLightningStrike = false;
			isLightningField = true;
			isLightningSpark = false;
		}
		if (count > CountLightningSpark)
		{
			Lightning.SetActive(false);
			LightningStrike.SetActive(false);
			LightningField.SetActive(true);
			LightningSpark.SetActive(false);
			isLightning = false;
			isLightningStrike = false;
			isLightningField = false;
			isLightningSpark = true;
		}
		if (count > CountNothing)
		{
			Lightning.SetActive(true);
			LightningStrike.SetActive(false);
			LightningField.SetActive(false);
			LightningSpark.SetActive(false);
			isLightning = false;
			isLightningStrike = false;
			isLightningField = false;
			isLightningSpark = false;
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

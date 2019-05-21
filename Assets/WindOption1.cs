using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindOption1 : MonoBehaviour
{
	public float count;		
	private GameObject FogFlow;	
	private GameObject Cyclone;
	public bool isFogFlow = false;
	public bool isCyclone = false;
	public int CountFogFlow = 5;	
	public int CountCyclone = 10;	

	void Start () {
		FogFlow = GameObject.Find ("FogFlow");
		Cyclone = GameObject.Find ("Cyclone");
	}

	void Update () {
		count += Time.deltaTime;
		if (count > CountFogFlow) {
			FogFlow.SetActive(isFogFlow == true);
		}
		if (count > CountCyclone) {
			Cyclone.SetActive(isCyclone == true);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPosition : MonoBehaviour {
	public GameObject[] PlayerPlefab;
	public GameObject StartPoint;

	// Use this for initialization
	void Start () {
		GameObject go = Instantiate (PlayerPlefab[DataManager.PlayerNo]);
		go.transform.position = StartPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

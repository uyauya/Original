using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMuzzle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.parent = GameObject.Find ("Boss02").transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakBlock1 : MonoBehaviour {

	protected BlockBasic blockBasic;

	// Use this for initialization
	void Start () {
		blockBasic = gameObject.GetComponent<BlockBasic> ();
		blockBasic.Initialize ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

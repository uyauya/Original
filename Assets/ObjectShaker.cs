using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectShaker : MonoBehaviour {

	public void Shake(GameObject shakeObj){
		iTween.ShakePosition(shakeObj,iTween.Hash("x",0.3f,"y",0.3f,"time",0.5f));
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

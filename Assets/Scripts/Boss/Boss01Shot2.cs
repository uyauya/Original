using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss01Shot2 : MonoBehaviour {

	public float DestroyTime = 4.0F;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, DestroyTime);
	}

	// Update is called once per frame
	void Update () {

	}
}

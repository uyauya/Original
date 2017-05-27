﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour {

	public Vector3 localGravity;
	private Rigidbody rb;

	void Start () {
		rb = this.GetComponent<Rigidbody>();
		rb.useGravity = false;
	}

	void FixedUpdate () {
		setLocalGravity ();
	}

	void setLocalGravity(){
		rb.AddForce (localGravity, ForceMode.Acceleration);
	}
}

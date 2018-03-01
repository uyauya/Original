using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPose : MonoBehaviour {

	private Animator animator;	

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		animator.SetTrigger ("Salute");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Shot : MonoBehaviour {

	public GameObject target;

	void Start () {
		
	}

	void Update () {
		transform.position = target.transform.position + (target.transform.forward * 2);
		transform.position += transform.forward;
		transform.RotateAround (target.transform.position + (target.transform.forward * 4), Vector3.up, 1);
	}

}


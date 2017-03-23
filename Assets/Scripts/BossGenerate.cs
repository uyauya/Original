using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGenerate : MonoBehaviour {

	public GameObject Boss02;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")){

			Boss02.SetActive(true);
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ボス発生用。ボスの周りに接触枠を作り、枠に接触したらボスを出現させる
public class BossGenerate : MonoBehaviour {

	public GameObject Boss02;

	void OnTriggerEnter(Collider other){
		if(other.gameObject.CompareTag("Player")){

			Boss02.SetActive(true);
		}
	}
}


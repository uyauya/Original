using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShot02Range : MonoBehaviour
{
	public static bool isHitDesision = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		
    }

	//private void OnTriggerEnter(Collision Collider) {
	private void OnTriggerEnter(Collider other) {
		//if (collider.gameObject.tag == "Player") {
		if (other.tag == "Player"){
			isHitDesision = true;
		}
	}

	//private void OnTriggerExit(Collider Collider) { 
	private void OnTriggerExit(Collider other) {
		//if (collider.gameObject.tag == "Player") {
		if (other.tag == "Player") {
			isHitDesision = false;
		}
	}
}


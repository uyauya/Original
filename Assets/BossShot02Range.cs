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

	private void OnOnTriggerEnter(Collision collider) {
		if (collider.gameObject.tag == "Player") {
			isHitDesision = true;
		}
	}

	private void OnTriggerExit(Collider collider) {  
		if (collider.gameObject.tag == "Player") {
			isHitDesision = false;
		}
	}
}


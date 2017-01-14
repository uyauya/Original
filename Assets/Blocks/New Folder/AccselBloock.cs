using UnityEngine;
using System.Collections;

public class AccselBloock : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		other.gameObject.GetComponent<Rigidbody>().
			AddForce(new Vector3(0,0,10), ForceMode.VelocityChange);
	}
}



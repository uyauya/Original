using UnityEngine;
using System.Collections;

public class WarpBlock : MonoBehaviour {

	public float XPower;
	public float YPower;
	public float ZPower;

	void OnCollisionEnter(Collider other){
		if(other.gameObject.tag == "Player")
		//other.gameObject.transform.position = new Vector3 (XPower, YPower, ZPower);
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(XPower, YPower, ZPower));
	}
}
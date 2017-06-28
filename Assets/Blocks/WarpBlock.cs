using UnityEngine;
using System.Collections;

public class WarpBlock : MonoBehaviour {

	public float XPower;
	public float YPower;
	public float ZPower;

	void OnTriggerEnter(Collider other){
		other.gameObject.transform.position = new Vector3 (XPower, YPower, ZPower);
	}
}
using UnityEngine;
using System.Collections;

public class MovingFloor : MonoBehaviour {

	/*public Vector3 defaultScale = Vector3.zero;
	// Use this for initialization
	void Start () 
	{
		defaultScale = transform.lossyScale;
	}
	
	// Update is called once per frame
	void Update () 
	{
		Vector3 lossyScle = transform.lossyScale;
		Vector3 localScale = transform.localScale;
		transform.localScale = new Vector3 (
			localScale.x / lossyScle.x * defaultScale.x,
			localScale.y / lossyScle.y * defaultScale.y,
			localScale.z / lossyScle.z * defaultScale.z
		);
	}

	void OnCollisionEnter(Collision collision){
		if (transform.parent = null && collision.gameObject.tag == "MovingFloor") {
			var emptyObject = new GameObject ();
			emptyObject.transform.parent = collision.gameObject.transform;
			transform.parent = emptyObject.transform;
		}
	}

	void OncollisionExit(Collision collision) {
		if (transform.parent != null && collision.gameObject.tag == "MovingFlor") {
			transform.parent = null;
		}
	}*/
}

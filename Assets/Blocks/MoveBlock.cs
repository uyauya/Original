using UnityEngine;
using System.Collections;

public class MoveBlock : MonoBehaviour {
	
	//public AudioSource source;
	int beforeTime;
	
	void Update () 
	{
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.CompareTag ("Block")) {
			other.gameObject.SendMessage ("Move", 1f, SendMessageOptions.DontRequireReceiver);
			//int current = Time.frameCount;
			//if(current - beforeTime > 7) {
				//source.Play();
				//beforeTime = current;
			//}
		}
	}
}


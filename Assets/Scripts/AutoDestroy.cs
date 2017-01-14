using UnityEngine;
using System.Collections;

public class AutoDestroy : MonoBehaviour {

	public float destroyTime = 0.1F;
	
	// Use this for initialization
	void Start () {
	
		//時間経過で自動消滅する
		Destroy(this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

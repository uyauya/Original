using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 時間経過で自動的に消去させる仕様
public class AutoDelete : MonoBehaviour {

	public float destroyTime = 0.1F;	// 自動的に消えるまでの時間

	// Use this for initialization
	void Start () {

		//時間経過で自動消滅する
		Destroy(this.gameObject, destroyTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

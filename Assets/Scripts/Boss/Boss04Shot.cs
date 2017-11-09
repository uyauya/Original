using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (rigidbodyの付いた）キャラを押していくショット
public class Boss04Shot : MonoBehaviour {

	public float Pressure;   	// 圧力割合
	public Vector3 velocity;	// 風速

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {		
	}

	void OnTriggerStay(Collider col) {
		if ( col.GetComponent<Rigidbody>() == null ) {
			return;
		}

		// 相対速度計算
		var relativeVelocity = velocity - col.GetComponent<Rigidbody>().velocity;
		// 空気抵抗を与える
		col.GetComponent<Rigidbody>().AddForce(Pressure * relativeVelocity);
	}

}

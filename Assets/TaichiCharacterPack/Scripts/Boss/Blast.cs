using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// (rigidbodyの付いた）キャラを(放射状に）吹き飛ばす
public class Blast : MonoBehaviour {
	public float Pressure;   	// 圧力割合
	public float speed;         // 爆風の速さ

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
		var velocity = (col.transform.position - transform.position).normalized * speed;
		// 空気抵抗を与える
		col.GetComponent<Rigidbody>().AddForce(Pressure * velocity);
	}

}

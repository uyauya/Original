using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;	// enumを使う

public class ControllerConfig : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		DownKeyCheck ();
	}

	void DownKeyCheck(){
		// 何かキーが押されていれば
		if (Input.anyKeyDown) {
			// KeyCode内を一つ一つ検索して何が押されているか確認、押されたキーをcodeとする
			foreach (KeyCode code in Enum.GetValues(typeof(KeyCode))) {
				// 確認された（押された）キーを
				if (Input.GetKeyDown (code)) {
					// Fire1と認識させる
					Debug.Log (code);
					break;
				}
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// StartPositionのオブジェクトを配置した場所にプレイヤを出現させる
public class StartPosition : MonoBehaviour {
	public GameObject[] PlayerPlefab;	// プレイヤオブジェクト格納
	public GameObject StartPoint;		// StartPositionオブジェクト格納

	// Use this for initialization
	void Start () {
		// プレイヤを出現させる
		GameObject go = Instantiate (PlayerPlefab[DataManager.PlayerNo]);
		// 出現させたプレイヤをStartPositionに配置
		go.transform.position = StartPoint.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

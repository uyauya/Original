using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarController : MonoBehaviour {

	// 回転速度
	public float RotSpeedH = 5.0f;
	public float RotSpeedL = 1.0f;

	// Use this for initialization
	void Start () {
		//回転を開始する角度をランダムで設定
		this.transform.Rotate (0, Random.Range (0, 360), 0);
	}

	// Update is called once per frame
	void Update () {
		//回転速度をランダムで設定
		this.transform.Rotate (0, Random.Range (RotSpeedL, RotSpeedH), 0);
	}
}

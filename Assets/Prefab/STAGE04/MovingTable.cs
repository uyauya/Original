using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 自動的にエアホッケーの動きのような動きをするテーブル
// ランダムスピードでピンボールのように壁にはじかれて壁の内側を動き続ける
public class MovingTable : MonoBehaviour {

	private float Spped;
	public float XSpeedF = 5.0f;		// ランダムスピードの最速
	public float XSpeedL = 1.0f;		// ランダムスピードの最遅
	public float YSpeedF = 0.0f;		// ランダムスピードの最速
	public float YSpeedL = 0.0f;		// ランダムスピードの最遅
	public float ZSpeedF = 5.0f;		// ランダムスピードの最速
	public float ZSpeedL = 1.0f;		// ランダムスピードの最遅

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// スピードをランダムにする
		float x = Random.Range (XSpeedF,XSpeedL);
		float y = Random.Range (YSpeedF,YSpeedL);
		float z = Random.Range (ZSpeedF,ZSpeedL);
		gameObject.transform.localPosition += new Vector3 (x, y, z);
		Spped = Random.Range (1, 9) / 10f;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour {

	// 最小サイズ
	public float Minimum = 1.0f;
	// 拡大縮小スピード
	public float MagSpeed = 10.0f;
	// 拡大率
	public float Magnification = 0.07f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// 雲を拡大縮小（X軸方向,Z軸方向に拡大縮小）
		this.transform.localScale = new Vector3(this.Minimum + Mathf.Sin(Time.time * this.MagSpeed) * this.Magnification,
			this.transform.localScale.y, this.Minimum + Mathf.Sin(Time.time * this.MagSpeed) * this.Magnification);
	}
}

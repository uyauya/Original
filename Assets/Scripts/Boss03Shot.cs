using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 火炎放射器
// 弾をブレさせながら徐々に大きくして炎のパーティクルをつける
public class Boss03Shot : MonoBehaviour {

	public float DestroyTime = 100.0F;	// 弾消滅までの時間
	public float Bigger = 1.0F;
	public float Xsize = 1.001F;
	public float Ysize = 1.001F;
	public float Zsize = 1.001F;

	// Use this for initialization
	void Start () {
		//Destroy(gameObject, DestroyTime);

	}

	// Update is called once per frame
	void Update () {
		// 時間あたりBiggerのわる割合で拡大。等間隔でない場合は(x,y,z)にそれぞれ値を入れる
		transform.localScale = new Vector3 (Mathf.Sin(Time.time * Bigger) + Xsize, Ysize, Zsize);
		transform.localPosition = new Vector3 (Mathf.Sin (Time.time * 1.2f) + 2, 2, 2);
	}
}


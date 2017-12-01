using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 火炎放射器
// 弾をブレさせながら徐々に大きくして炎のパーティクルをつける
public class Boss03Shot : MonoBehaviour {

	/*public float DestroyTime = 100.0F;	// 弾消滅までの時間
	public float Bigger = 1.0F;
	public float Xsize = 1.001F;
	public float Ysize = 1.001F;
	public float Zsize = 1.001F;
	float m_varX = Random.Range(-1 ,1);
	float m_varY = Random.Range(-1 ,1);
	float m_varZ = Random.Range(-1 ,1);
	public GameObject obj;
	Vector3 scale;

	// Use this for initialization
	void Start () {
		Destroy(gameObject, DestroyTime);
		scale = obj.transform.localScale;
	}

	// Update is called once per frame
	void Update () {
		// 時間あたりBiggerのわる割合で拡大。
		//float delta = Mathf.Sin(Time.time * Bigger);
		//transform.localScale = new Vector3 (delta + Xsize, delta + Ysize, delta + Zsize);
		//transform.localPosition = new Vector3 (Mathf.Sin (Time.time * 1.2f) + 2, 2, 2);
		// 等間隔でない場合は(x,y,z)にそれぞれ値を入れる
		// transform.localScale = new Vector3 (Mathf.Sin(Time.time * Bigger) + Xsize, Ysize, Zsize);
		// それぞれを違う比率で拡大
		//transform.localScale = new Vector3(Mathf.Sin((Time.time + m_varX) * Bigger) + Xsize,
		//	Mathf.Sin((Time.time + m_varY) * Bigger) + Ysize, Mathf.Sin((Time.time + m_varZ) * Bigger) + Zsize);
	}

	void Scale() {
		iTween.ScaleTo (obj, iTween.Hash ("x", scale.x + 1, "y", scale.y + 1, "z", scale.z + 1, "time", 3));
		scale = obj.transform.localScale;
	}*/
}


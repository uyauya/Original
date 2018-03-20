using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMove : MonoBehaviour {

	public float amplitude = 0.01f;		// 揺れ具合
	private int frameCnt = 0;			// ランダムで揺らすための時間取り
	private Vector3 _dir;				// 進行方向
	private float _speed;				// 進行速度
	public float XdirectionS = -0.1f;	// X方向範囲（ここから）
	public float XdirectionL = 0.1f;	// X方向範囲（ここまで）
	public float YdirectionS = -0.1f;
	public float YdirectionL = 0.1f;
	public float ZdirectionS = -0.1f;
	public float ZdirectionL = 0.1f;
	public float XspeedS = -0.1f;		// X方向最低速度
	public float XspeedL = 0.1f;		// X方向最高速度	
	public float YspeedS = -0.1f;
	public float YspeedL = 0.1f;
	public float ZspeedS = -0.1f;
	public float ZspeedL = 0.1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		{
			// スピードをランダムにする
			float x = Random.Range (XspeedS,XspeedL);
			float y = Random.Range (YspeedS,YspeedL);
			float z = Random.Range (ZspeedS,ZspeedL);
			gameObject.transform.localPosition += new Vector3 (x, y, z);
			_speed = Random.Range (1, 9) / 10f;
			// 方向をランダムにする
			x = Random.Range (XdirectionS,XdirectionL);
			y = Random.Range (YdirectionS,YdirectionL);
			z = Random.Range (ZdirectionS,ZdirectionL);
			_dir = new Vector3 (x, y, z);
		}
		gameObject.transform.Translate (_dir * _speed);
		StartCoroutine ("UpDownCoroutine");
	}

	IEnumerator Boss02ShotCoroutine () {

		frameCnt += 1;
		// フレームカウントリセット用
		if (10000 <= frameCnt) {
			frameCnt = 0;
		}
		if (0 == frameCnt % 700) {
			// 上下に振動させる
			float posYSin = Mathf.Sin (2.0f * Mathf.PI * (float)(frameCnt % 200) / (200.0f - 1.0f) * 0.1f);
			iTween.MoveAdd (gameObject, new Vector3 (0, amplitude * posYSin, 0), 0.0f);
			yield return new WaitForSeconds (0.1f);
		}
	}
}

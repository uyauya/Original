using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤが近づいたらブロックの高さをランダムな速さ、高さに上下させる。
public class ExpansionBlock : MonoBehaviour {

	protected EnemyBasic enemyBasic;
	//bool dead = false;
	private int frameCnt = 0;				// ブロックをランダムで揺らすための時間取り
	private float BlockSpeed;
	private float BlockHeight;
	private float YBlockSpeedL;				// ブロックの上下させる速度（遅い）
	private float YBlockSpeedH;				// ブロックの上下させる速度（速い）
	private float YBlockHeightL;			// ブロックの上下させる高さ（低い）
	private float YBlockHeightH;			// ブロックの上下させる高さ（高い）


	// Use this for initialization
	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
	}

	// Update is called once per frame
	void Update () {
		// ターゲット（プレイヤー）との距離がSearch以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search) {
			MoveBlock ();
		}
	}

	void MoveBlock () {
		//float Speedy = Random.Range (YBlockSpeedH, YBlockSpeedL);
		//BlockSpeed = Random.Range (1, 3) / 10f;
		//float Heighty = Random.Range (YBlockHeightH, YBlockHeightL);
		//BlockHeight = Random.Range (-5, 5) / 10f;
		float Heighty = Random.Range (-5, 2) / 10f;
		//gameObject.transform.localScale += new Vector3 (0, Heighty, 0);
		gameObject.transform.localScale += new Vector3 (0, Mathf.Sin (Time.time * 1.2f) * 2,  0);
		Debug.Log (Heighty);
	}


}

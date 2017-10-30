using UnityEngine;
using System.Collections;

// Bullet01スクリプトの継承先
// public class Bullet01Aの後にMonoBehaviourでなく継承元の名前を書く
public class Bullet01A : Bullet01 {

	protected Bullet01 bullet01;

	void Start () {
		bullet01 = gameObject.GetComponent<Bullet01> ();
		// Initializeで継承元のデータを最初に読み取る
		bullet01.Initialize ();
	}	

	void Update () {

	}	

}

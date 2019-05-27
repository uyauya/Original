using UnityEngine;
using System.Collections;

// Bullet01スクリプトの継承先
// public class Bullet01Aの後にMonoBehaviourでなく継承元の名前を書く
public class Bullet01RC : Bullet01 {

	protected Bullet01R bullet01R;

	void Start () {
		bullet01R = gameObject.GetComponent<Bullet01R> ();
		// Initializeで継承元のデータを最初に読み取る
		bullet01R.Initialize ();
	}	

	void Update () {

	}	

}

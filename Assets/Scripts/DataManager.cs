using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// シーンをまたいでデータ保持する処理
public class DataManager : MonoBehaviour {
	[System.NonSerialized]
	public static int PlayerNo;		//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）SelectEventスクリプト参照
	public static int SceneNo;		//ステージNo取得用

	// Use this for initialization
	void Start () {
		// シーン移動してもPlayerNoを残しておく
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JsonSave : MonoBehaviour {
	/*
	//スクリプト内のセーブしたい部分を[SerializeField]でまとめる。
	//外部から取得する為publicにする。
	[SerializeField]
	public class DataManager {
		[SerializeField]
		public static int player;		//プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）
		public static int SceneNo;		//ステージNo取得用
	}

	[SerializeField]
	public class PlayerController {
		[SerializeField]
		public int boostPointMax;		// プレイヤーブースト最大値
	}

	[SerializeField]
	public class PlayerAp {
		[SerializeField]
		public int armorPointMax;		// プレイヤー体力最大値
	}

	// Use this for initialization
	void Start () {
		//セーブデータの設定
		//セーブ可能箇所の中の"i"場所を指定
		SaveData.SetInt ("i", 6);
		SaveData.SetClass<player> ("p1", new player ());
		SaveData.Save ();

		Player getPlayer = SaveData.GetClass<player> ("p1", new player ());

		Debug.Log ("取得したint値は" + SaveData.GetInt ("i"));
		Debug.Log (getplayer.name);
		Debug.Log (getplayer.ItemCount[0]);
	}

	// Update is called once per frame
	void Update () {

	}*/
}

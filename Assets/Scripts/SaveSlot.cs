﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する
using UnityEngine.UI;				// UIを使う時は追加する

public class SaveSlot : MonoBehaviour {

	public Text text;
	public void OnClick(int number) {
		switch (number) {
		case 0:
			DataManager.Instance.SaveData("SaveSlot01");
			break;
		case 1:
			break;
		case 2:
			break;
		case 3:
			break;
		case 4:
			break;
		case 5:
			break;
		default:
			break;
		}
	}
}

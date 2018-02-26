using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する
using UnityEngine.UI;				// UIを使う時は追加する

public class SaveSlot : MonoBehaviour {
	public GameObject Dialogs;
	//public Text text;
	private int Number;
	public void PushButton(int number) {
		Number = number;
		Dialogs.SetActive (true);
		DialogSettings ();
	}

	public void OnClick() {
		switch (Number) {
		case 0:
			DataManager.Instance.SaveData("SaveSlot01");
			break;
		case 1:
			DataManager.Instance.SaveData("SaveSlot02");
			break;
		case 2:
			DataManager.Instance.SaveData("SaveSlot03");
			break;
		case 3:
			DataManager.Instance.SaveData("SaveSlot04");
			break;
		case 4:
			DataManager.Instance.SaveData("SaveSlot05");
			break;
		case 5:
			DataManager.Instance.SaveData("SaveSlot06");
			break;;
		default:
			break;
		}
	}
	public void DialogSettings(){
		
		Dialog.Message ("いいよ",OnClick,DialogNo);
	}
	public void DialogNo(){
		Dialogs.SetActive (false);
	}
	YesOrNo Dialog = new YesOrNo ();	
}

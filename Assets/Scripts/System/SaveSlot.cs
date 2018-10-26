using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する
using UnityEngine.UI;				// UIを使う時は追加する

public class SaveSlot : MonoBehaviour {
	private AudioSource[] audioSources;
	public GameObject Dialogs;
	private int Number;
	public void PushButton(int number) {
		Number = number;
		DialogManager.Instance.Make("せーぶするよ？",OnYesButtonAction,OnNoButtonAction);
	}

	public void OnYesButtonAction() {
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

	public void OnNoButtonAction()
	{
		Debug.Log("Noボタンが押されました");
		DialogManager.Instance.Hide ();
	}
}

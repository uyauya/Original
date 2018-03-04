using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する
using UnityEngine.UI;				// UIを使う時は追加する

public class LoadSlot : MonoBehaviour {
	public GameObject Dialogs;
	//public Text text;
	private int Number;
	public void PushButton(int number) {
		Number = number;
		//Dialogs.SetActive (true);
		//DialogSettings ();
		DialogManager.Instance.Make("ろーどするよ？",OnYesButtonAction,OnNoButtonAction);
	}

	public void OnYesButtonAction() {
		switch (Number) {
		case 0:
			DataManager.Instance.LoadData("SaveSlot01");
			break;
		case 1:
			DataManager.Instance.LoadData("SaveSlot02");
			break;
		case 2:
			DataManager.Instance.LoadData("SaveSlot03");
			break;
		case 3:
			DataManager.Instance.LoadData("SaveSlot04");
			break;
		case 4:
			DataManager.Instance.LoadData("SaveSlot05");
			break;
		case 5:
			DataManager.Instance.LoadData("SaveSlot06");
			break;;
		default:
			break;
		}
	}
	//public void DialogSettings(){
		
	//	Dialog.Message ("いいよ",OnClick,DialogNo);
	//}
	//public void DialogNo(){
	//	Dialogs.SetActive (false);
	//}
	//YesOrNo Dialog = new YesOrNo ();	
	public void OnNoButtonAction()
	{
		Debug.Log("Noボタンが押されました");
		DialogManager.Instance.Hide ();
	}
}

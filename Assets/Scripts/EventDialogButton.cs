using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDialogButton : MonoBehaviour {

	// Yesボタンが押されたときのイベント
	public void OnYesButton()
	{
		DialogManager.Instance.Hide();
		DialogManager.Instance.YesAction();
	}

	// Noボタンが押されたときのイベント
	public void OnNoButton()
	{
		DialogManager.Instance.Hide();
		DialogManager.Instance.NoAction();
	}

}

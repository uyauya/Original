using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDialogButton : MonoBehaviour {

	// Yesボタンが押されたときのイベント
	public void OnYesButton()
	{
		DialogManager.Instance.Hide();
		SoundManager00.Instance.Play(0);
		SoundManager00.Instance.PlayDelayed (2, 3.0f);
		DialogManager.Instance.YesAction();
	}

	// Noボタンが押されたときのイベント
	public void OnNoButton()
	{
		DialogManager.Instance.Hide();
		SoundManager00.Instance.Play(1);
		SoundManager00.Instance.PlayDelayed (2, 3.0f);
		DialogManager.Instance.NoAction();
	}

}

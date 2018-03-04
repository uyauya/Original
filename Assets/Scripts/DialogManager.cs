using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class DialogManager : SingletonMonoBehaviour<DialogManager>
{
	// ダイアログ本体
	[SerializeField]
	private GameObject Dialog;

	// ダイアログメッセージ
	[SerializeField]
	private GameObject Message;

	// ボタンが押されたときのアクション
	public UnityAction YesAction;
	public UnityAction NoAction;


	protected void Start ()
	{

	}

	protected void Update ()
	{

	}

	// ダイアログ表示
	public void Show()
	{
		Dialog.SetActive(true);
	}

	// ダイアログ非表示
	public void Hide()
	{
		Dialog.SetActive(false);
	}

	// ダイアログ生成
	public void Make(string message, UnityAction yesAction, UnityAction noAction)
	{
		Message.GetComponent<Text>().text = message;
		YesAction = yesAction;
		NoAction = noAction;
		Show();
	}

}

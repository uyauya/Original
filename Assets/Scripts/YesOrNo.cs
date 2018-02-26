using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class YesOrNo : MonoBehaviour {
	public Text DialogMessage;
	public GameObject YesButton;
	public GameObject NoButton;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Message(string message,UnityAction Yes,UnityAction No) {
		DialogMessage.text = message;
		YesButton.GetComponent<EventYesOrNo> ().YesAction = Yes;
		NoButton.GetComponent<EventYesOrNo> ().NoAction = No;
	}
}

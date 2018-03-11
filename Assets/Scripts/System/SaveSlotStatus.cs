using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlotStatus : MonoBehaviour {
	public Text text;
	//public string SlotName;

	// Use this for initialization
	void Start () {
		//GetSlotStatus (SlotName);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GetSlotStatus (string SlotName) {
		//DataManager.Instance.LoadData (SlotName);
		string Level = DataManager.Level.ToString();
		text.text = Level;
	}
}

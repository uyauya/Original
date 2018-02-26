using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class EventYesOrNo : MonoBehaviour {

	public UnityAction YesAction;
	public UnityAction NoAction;

	public void OnclickYes(){
		YesAction ();
	}

	public void OnclickNo(){
		NoAction ();
	}
}

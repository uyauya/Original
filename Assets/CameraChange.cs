using UnityEngine;
using System.Collections;

public class CameraChange : MonoBehaviour {

	private GameObject MainCam;
	private GameObject SubCam;
	
	void Start () {
		MainCam = GameObject.Find("MainCamera");
		SubCam = GameObject.Find("SubCamera");
		
		SubCam.SetActive(false);
	}
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.Tab)){
			if(MainCam.activeSelf){
				MainCam.SetActive (false);
				SubCam.SetActive (true);
			}else{
				MainCam.SetActive (true);
				SubCam.SetActive (false);
			}
		}
	}
	
}

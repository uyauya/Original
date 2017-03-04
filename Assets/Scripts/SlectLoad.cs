using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlectLoad : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine ("LoadTime");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator LoadTime (){
		yield return new WaitForSeconds (1.0f);
		SceneManager.LoadScene ("STAGE02");
	}
	
}

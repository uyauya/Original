using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SlectLoad : MonoBehaviour {
	private AudioSource[] audioSources;
	public float WaitTime = 1.8f;

	// Use this for initialization
	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
		SoundManager00.Instance.Play(0);
		StartCoroutine ("LoadTime");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator LoadTime (){
		yield return new WaitForSeconds (WaitTime);
		SceneManager.LoadScene ("STAGE02");
	}
	
}

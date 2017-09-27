using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigControl : MonoBehaviour 
{
	public bool isConfig;
	public GameObject Config;
	public AudioSource audioSource;

	void Start()
	{
		isConfig = false;
		Config.SetActive(false);
	}
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isConfig) {
				isConfig = false;
				Config.SetActive(false);
				Time.timeScale = 1f;
				//audioSource.Play();
			} else {
				isConfig = true;
				Config.SetActive(true);
				Time.timeScale = 0;
				//SoundManager.Instance.Stop ();
				//audioSource.Pause();		//一時停止（完全停止する場合はaudioSource.Stop();
			}
		}
	}
}

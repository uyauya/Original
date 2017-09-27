using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigControl : MonoBehaviour 
{
	public bool isConfig;
	public GameObject Setting;
	public AudioSource audioSource;

	void Start()
	{
		isConfig = false;
		Setting.SetActive(false);
	}
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			if (isConfig) {
				isConfig = false;
				Setting.SetActive(false);
				Time.timeScale = 1f;
			} else {
				isConfig = true;
				Setting.SetActive(true);
				Time.timeScale = 0;
			}
		}
	}
}

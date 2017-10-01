using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public bool isPause;
	public bool isConfig;
	public GameObject Tutorial;
	public GameObject Setting;
	public AudioSource audioSource;

	void Start()
	{
		isPause = false;
		isConfig = false;
		Tutorial.SetActive(false);
		Setting.SetActive(false);
	}
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)&& !isConfig) {
			if (isPause) {
				isPause = false;
				Tutorial.SetActive (false);
				Time.timeScale = 1f;
				audioSource.Play ();
			} else {
				isPause = true;
				Tutorial.SetActive (true);
				Time.timeScale = 0;
				//SoundManager.Instance.Stop ();
				audioSource.Pause ();		//一時停止（完全停止する場合はaudioSource.Stop();
			}
		}
				if (Input.GetKeyDown (KeyCode.LeftShift) && isPause ) {
				if (isConfig) {
					isConfig = false;
					Setting.SetActive (false);
					//Time.timeScale = 1f;
				} else {
					isConfig = true;
					Setting.SetActive (true);
					//Time.timeScale = 0;
				}
			}
		}
	
}

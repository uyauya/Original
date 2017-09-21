using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	public bool isPause;
	public GameObject Tutorial;
	public AudioSource audioSource;

	void Start()
	{
		isPause = false;
		Tutorial.SetActive(false);
	}
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPause) {
				isPause = false;
				Tutorial.SetActive(false);
				Time.timeScale = 1f;
				audioSource.Play();
			} else {
				isPause = true;
				Tutorial.SetActive(true);
				Time.timeScale = 0;
				//SoundManager.Instance.Stop ();
				audioSource.Pause();		//一時停止（完全停止する場合はaudioSource.Stop();
			}
		}
	}
}

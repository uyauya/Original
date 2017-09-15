using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	bool isPause;
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
				audioSource.Stop();
			}

			//Camera.main.GetComponent<Animator>().SetBool("isPause", isPause);
		}
	}
}

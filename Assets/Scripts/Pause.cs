using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	bool isPause;
	public GameObject Tutorial;

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
			} else {
				isPause = true;
				Tutorial.SetActive(true);
				Time.timeScale = 0;
			}

			Camera.main.GetComponent<Animator>().SetBool("isPause", isPause);
		}
	}
}

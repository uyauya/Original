using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
	bool isPause;
	public Image Tutorial;

	void Start()
	{
		isPause = false;
		Tutorial.enabled = false;
	}
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (isPause) {
				isPause = false;
				Tutorial.enabled = false;
				Time.timeScale = 1f;
			} else {
				isPause = true;
				Tutorial.enabled = true;
				Time.timeScale = 0;
			}

			Camera.main.GetComponent<Animator>().SetBool("isPause", isPause);
		}
	}
}

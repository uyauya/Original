using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
	void Start ()
    {
        // save current timescale
        PlayerPrefs.SetFloat("TimeScale", Time.timeScale);
        // pause
        Time.timeScale = 0;
	}

    public void Resume()
    {
        // resume
        Time.timeScale = PlayerPrefs.GetFloat("TimeScale");
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("pause");
    }

    public void LoadConfigScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("config", UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }
}

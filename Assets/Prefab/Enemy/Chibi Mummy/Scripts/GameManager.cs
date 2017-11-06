using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetKeyDown("escape"))
        {
            Debug.Log("Detect escape down.");
            Scene pauseScene = SceneManager.GetSceneByName("pause");
            if (!pauseScene.isLoaded)
                SceneManager.LoadScene("pause", UnityEngine.SceneManagement.LoadSceneMode.Additive);
        }
	}
}

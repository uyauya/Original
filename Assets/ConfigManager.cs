using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Slider m_gameSpeed;

    private void Start()
    {
        m_gameSpeed.value = PlayerPrefs.GetFloat("TimeScale");
    }

    public void ChangeGameSpeed()
    {
        PlayerPrefs.SetFloat("TimeScale", m_gameSpeed.value);
    }

    public void ResetGameSpeed()
    {
        m_gameSpeed.value = 1f;
    }

    public void CloseConfigScene()
    {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("config");
    }
}
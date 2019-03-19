using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class ButtonController : MonoBehaviour
{
    [SerializeField] bool m_defaultSelected;
    [SerializeField] AudioClip m_selectionSe;
    [SerializeField] GameObject[] m_objectsActivatedOnlyOnSelection;
    Button m_button;
    AudioSource m_audioSource;

    void Start()
    {
        m_button = GetComponent<Button>();
        m_audioSource = GetComponent<AudioSource>();
        OnDeselected();

        if (m_defaultSelected)
        {
            m_button.Select();
        }
    }

    void Update()
    {

    }

    public void OnSelected()
    {
        m_audioSource.PlayOneShot(m_selectionSe);

        foreach(var go in m_objectsActivatedOnlyOnSelection)
        {
            go.SetActive(true);
        }
    }

    public void OnDeselected()
    {
        foreach (var go in m_objectsActivatedOnlyOnSelection)
        {
            go.SetActive(false);
        }
    }
}

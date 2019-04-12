using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button), typeof(AudioSource))]
public class ButtonController : MonoBehaviour
{
    [SerializeField] bool m_defaultSelected;
    [SerializeField] AudioClip m_selectionSe;							//鳴らす音
    [SerializeField] GameObject[] m_objectsActivatedOnlyOnSelection;	//表示をON/OFFさせるものの格納場所
    Button m_button;
    AudioSource m_audioSource;
	public static bool SelectAction;

	//非選択状態とする
    void Start()
    {
		SelectAction = false;
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

	//選択状態の時音を鳴らし、表示をONにする。
    public void OnSelected()
    {
        //m_audioSource.PlayOneShot(m_selectionSe);

        foreach(var go in m_objectsActivatedOnlyOnSelection)
        {
			go.SetActive(true);
			SelectAction = true;

        }
    }

    public void OnDeselected()
    {
		
		foreach (var go in m_objectsActivatedOnlyOnSelection)
        {

			SelectAction = false;
			go.SetActive(false);
        }
    }
}

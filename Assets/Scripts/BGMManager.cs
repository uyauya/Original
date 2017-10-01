using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BGMManager : MonoBehaviour {

	public AudioSource SoundSource;
	private List<AudioSource> _bgmAudioSource;
	[SerializeField]
	GameObject bgm;
	// Use this for initialization
	void Start () 
	{

		bgm.GetComponent<Slider>().onValueChanged.AddListener((value) =>
			{
				//foreach(var SoundSource in _bgmAudioSource)
				//{
					SoundSource.volume = value;
				//}
			});
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}

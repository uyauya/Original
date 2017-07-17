using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

	[SerializeField]
	private AudioSource audioSource;

	[SerializeField]
	private List<AudioClip> audioClipList = new List<AudioClip>(); 


	// 音を再生する
	public void Play(int number, GameObject go = null)
	{
		AudioClip clip = audioClipList[number];

		if (go != null)
		{
			AudioSource audioSource = go.GetComponent<AudioSource>();
			audioSource.PlayOneShot(clip);
		}
		else
		{
			audioSource.PlayOneShot(clip);
		}
	}

	// 音を再生する
	public void PlayDelayed(int number,float delay, GameObject go = null)
	{
		AudioClip clip = audioClipList[number];

		if (go != null)
		{
			AudioSource audioSource = go.GetComponent<AudioSource>();
			audioSource.PlayOneShot(clip);
		}
		else
		{
			audioSource.PlayOneShot(clip);
		}
	}
	private void Start()
	{

	}

	private void Update()
	{

	}

}

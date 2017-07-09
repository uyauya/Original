using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSound : MonoBehaviour {

	public AudioSource audioSources01;  // 声
	public AudioSource audioSources02;	// 音
	public AudioClip audioClip01;
	public AudioClip audioClip02;
	public AudioClip audioClip03;

	void Start () {
		//audioSources = gameObject.GetComponent<AudioSource>();
		//audioSources01.clip = audioClip02;
		//audioSources02.clip = audioClip03;
		audioSources01.PlayOneShot (audioClip02);
		audioSources02.PlayOneShot (audioClip03);
	}

}

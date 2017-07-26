using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager3 : SingletonMonoBehaviour<SoundManager3> {

	[SerializeField]
	private List<AudioClip> audioClipList = new List<AudioClip>(); 


	// 音を再生する
	public void Play(int number, GameObject go = null)
	{
		Debug.Log("要素数" + audioClipList.Count);
		AudioClip clip = audioClipList[number];

		if (go != null)
		{
			StartCoroutine(PlayCoroutine(clip, go));
		}
		else
		{
			StartCoroutine(PlayCoroutine(clip, gameObject));
		}
	}

	// 音を遅延再生する
	public void PlayDelayed(int number, float delay, GameObject go = null)
	{
		AudioClip clip = audioClipList[number];

		if (go != null)
		{
			StartCoroutine(PlayDelayedCoroutine(clip, delay, go));
		}
		else
		{
			StartCoroutine(PlayDelayedCoroutine(clip, delay, gameObject));
		}

	}

	// 音を再生する コルーチン
	private IEnumerator PlayCoroutine(AudioClip clip, GameObject go)
	{
		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.PlayOneShot(clip);
		yield return new WaitWhile(() => audioSource.isPlaying);
		Destroy(audioSource);
	}

	// 音を遅延再生する コルーチン
	private IEnumerator PlayDelayedCoroutine(AudioClip clip, float delay, GameObject go)
	{
		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.PlayDelayed(delay);
		yield return new WaitWhile(() => audioSource.isPlaying);
		Destroy(audioSource);
	}


	private void Start()
	{

	}

	private void Update()
	{

	}

}

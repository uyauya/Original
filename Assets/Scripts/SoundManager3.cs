using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundManager3 : MonoBehaviour {

	[SerializeField]
	private List<AudioClip> audioClipList = new List<AudioClip>(); 
	[SerializeField]
	private float volume = 10;
	[SerializeField]
	private List<AudioSource> _bgmAudioSource;



	// 音を再生する
	public void Play(int number, GameObject go = null)
	{
		//Debug.Log("要素数" + audioClipList.Count);
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
		audioSource.volume = volume;
		audioSource.PlayOneShot(clip);
		yield return new WaitWhile(() => audioSource.isPlaying);
		Destroy(audioSource);
	}

	// 音を遅延再生する コルーチン
	private IEnumerator PlayDelayedCoroutine(AudioClip clip, float delay, GameObject go)
	{
		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.PlayDelayed(delay);
		yield return new WaitWhile(() => audioSource.isPlaying);
		Destroy(audioSource);
	}

	// 音を停止する
	public void Stop(int number, GameObject go = null)
	{
		//Debug.Log("要素数" + audioClipList.Count);
		AudioClip clip = audioClipList[number];

		if (go != null)
		{
			//StartCoroutine(PlayCoroutine(clip, go));
			go.GetComponent<AudioSource>().Stop();
		}
		else
		{
			//StartCoroutine(PlayCoroutine(clip, gameObject));
			gameObject.GetComponent<AudioSource>().Stop();
		}
	}

	void Start()
	{
	this.GetComponent<Slider>().onValueChanged.AddListener(value =>
	{
	foreach(var SoundSource in _bgmAudioSource)
	{
	SoundSource.volume = value;
	}
	});
	}
	private void Update()
	{

	}

}

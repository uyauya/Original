using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class SoundManager : SingletonMonoBehaviour<SoundManager> {

	[SerializeField]
	private List<AudioSource> _voiceAudioSource;
	[SerializeField]
	private List<AudioClip> audioClipList = new List<AudioClip>(); 
	[SerializeField]
	GameObject voice;
	private float Value;

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
		audioSource.volume = Value;
		audioSource.PlayOneShot(clip);
		yield return new WaitWhile(() => audioSource.isPlaying);
		Destroy(audioSource);
	}

	// 音を遅延再生する コルーチン
	private IEnumerator PlayDelayedCoroutine(AudioClip clip, float delay, GameObject go)
	{
		AudioSource audioSource = go.AddComponent<AudioSource>();
		audioSource.clip = clip;
		audioSource.volume = Value;
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

	private void Start()
	{
		voice.GetComponent<Slider> ().value = Volume ();
		voice.GetComponent<Slider>().onValueChanged.AddListener((value) =>
			{
				Value = value;
				foreach(var SoundSource in _voiceAudioSource)
				{
				SoundSource.volume = value;
				}
			});
	}

	private void Update()
	{

	}

	public void Play()
	{
		AudioSource Voiceplay = gameObject.GetComponent<AudioSource> ();
		Voiceplay.Play ();
	}

	public void Stop()
	{
		AudioSource Voicestop = gameObject.GetComponent<AudioSource> ();
		Voicestop.Stop ();
	}

	public void Pause()
	{
		AudioSource Voicepause = gameObject.GetComponent<AudioSource> ();
		Voicepause.Pause ();
	}

	public float Volume()
	{
		AudioSource Voicevolume = gameObject.GetComponent<AudioSource> ();
		return Voicevolume.volume;
	}
}

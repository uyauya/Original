using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour 

	{
		/// <summary>
		/// インスペクタ側から音声ファイルをリストに設定する
		/// </summary>
		[SerializeField]
		private List<AudioClip> _audioClipList;

		/// <summary>
		/// オーディオソースの数(同時再生数を考慮して値を設定)
		/// </summary>
		[SerializeField]
		private int _audioSourceNum = 5;

		[SerializeField]
		private List<AudioSource> _audioSourceList;
		private void Start()
		{
			//オーディオソースを指定数アタッチする
			for (int i = 0; i < _audioSourceNum; i++)
			{
				_audioSourceList.Add(gameObject.AddComponent<AudioSource>());
			}
		}

		//音声を再生するとに使う関数
		public void Play(string audioClipName)
		{
			foreach (var audioSource in _audioSourceList)
			{
				//オーディオクリップが設定されていない or 再生されていなければ
				if (audioSource.clip == null ||
					audioSource.isPlaying == false)
				{
					//指定した名前のオーディオクリップを取得
					AudioClip targetAudioClip = GetAudioClipFromName(audioClipName);
					if (targetAudioClip != null)
					{
						//クリップを設定
						audioSource.clip = targetAudioClip;
						//再生
						audioSource.Play();
					}
				}
			}
		}

		private AudioClip GetAudioClipFromName(string audioClipName)
		{
			foreach (var audioClip in _audioClipList)
			{
				if (audioClip.name == audioClipName)
				{
					return audioClip;
				}
			}
			Debug.LogErrorFormat("指定されたAudioClipは存在しません Name:{0}", audioClipName);
			return null;
		}
	}

using UnityEngine;
using System.Collections;

public class Sound : MonoBehaviour {

	private AudioSource 	audioSource;
	public AudioClip[]		sound;

	void Start(){
		audioSource = gameObject.AddComponent<AudioSource>();
		audioSource.loop = false;
	}

	private void soundRings(int value){
		if(value < 0){return;}
		if(value < sound.Length-1){return;}
		if(sound[value] == null){return;}

		audioSource.PlayOneShot(sound[value]);
	}
}


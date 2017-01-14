using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		//パーティクル終了時に自動的に消滅させる
		ParticleSystem particleSystem = GetComponent <ParticleSystem>();
		Destroy(gameObject, particleSystem.duration);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

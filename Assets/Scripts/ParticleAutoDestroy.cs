using UnityEngine;
using System.Collections;

public class ParticleAutoDestroy : MonoBehaviour {

	public float PDestroyTime;	// 消滅するまでの時間設定

	// Use this for initialization
	void Start () {
	
		//パーティクル終了時に自動的に消滅させる
		ParticleSystem particleSystem = GetComponent <ParticleSystem>();
		//Destroy(gameObject, particleSystem.duration);
		Destroy(gameObject, PDestroyTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

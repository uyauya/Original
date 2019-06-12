using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEquip : MonoBehaviour
{
	public static bool ParticleOn = false;
	public GameObject Particle01;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		//TrailOnの状態時、trailRenderer使用可とする
		if(ParticleOn == true)
		{
			Particle01.SetActive(true);
		}
		else if(ParticleOn == false)
		{
			Particle01.SetActive(false);
		}
	}
}

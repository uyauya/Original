using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatRange : MonoBehaviour
{
	public static bool isCombatDesision = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter(Collider other)
	{
		if ((other.tag == "Enemy")||(other.tag == "Wall"))
		{
			isCombatDesision = true;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if ((other.tag == "Enemy")||(other.tag == "Wall"))
		{
			isCombatDesision = false;
		}
	}
}
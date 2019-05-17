﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootPrintController : MonoBehaviour
{
	void Start () {
		StartCoroutine (Disappearing ());
	}

	IEnumerator Disappearing()
	{
		int step = 90;
		for (int i = 0; i < step; i++)
		{
			GetComponent<SpriteRenderer> ().material.color = new Color (1, 1, 1, 1 - 1.0f * i / step);
			yield return null;
		}
		Destroy (gameObject);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//足跡を消す
public class FootPrintController : MonoBehaviour
{
	public int stepTime = 90;

	void Start () {
		StartCoroutine (Disappearing ());
	}

	IEnumerator Disappearing()
	{
		//int step = 90;
		//for (int i = 0; i < step; i++)
		for (int i = 0; i < stepTime; i++)
		{
			//GetComponent<SpriteRenderer> ().material.color = new Color (1, 1, 1, 1 - 1.0f * i / step);
			GetComponent<SpriteRenderer> ().material.color = new Color (1, 1, 1, 1 - 1.0f * i / stepTime);
			yield return null;
		}
		Destroy (gameObject);
	}
}

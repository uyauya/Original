using System.Collections;
using System.Collections.Generic;
using UnityEngine;					

[RequireComponent(typeof(Slider))]
public class Config : MonoBehaviour {

	void Start()
	{
	this.GetComponent<Slider>().onValueChanged.AddListener(value => this.AudioSource.volume = value);
	}
}
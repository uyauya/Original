using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	private AudioSource[] audioSources;
	private Rigidbody rb;
	private Animator animator;
	private Pause pause;
	public int PlayerNo;						
	public bool isBig;							

	// Start is called before the first frame update
	void Start()
	{
		audioSources = gameObject.GetComponents<AudioSource>();
		animator = GetComponent<Animator> ();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update()
	{
		if (pause.isPause == false) 
		{
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			if (isBig == false) 
			{	
				if (Input.GetButtonDown ("Fire5")) 
				{
					animator.SetTrigger ("Attack");
				}
			}
		}
	}
}

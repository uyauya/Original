
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	public GameObject WeaponAttack;
	public Transform WeaponRange;
	private float Attack;
	public float damage = 500;
	public float shotInterval;			
	public float shotIntervalMax = 0.25F;
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	public int PlayerNo;
	private Pause pause;
	public bool isBig;							

	// Start is called before the first frame update
	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
		Transform WeaponRange = GameObject.FindWithTag ("Player").transform.Find("WeaponRange");
	}

	// Update is called once per frame
	void Update()
	{
		if (pause.isPause == false) 
		{
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			if (isBig == false) 
			{
				if (Input.GetButtonUp ("Fire5")) 
				{
					PlayerController.WeaponEquip = true;
					damage = Attack;
					animator.SetTrigger ("Attack");
					Combat ();
				}
			}
		}
	}

	void Combat() 
	{
		if (Time.time - shotInterval > shotIntervalMax) 
		{
			shotInterval = Time.time;
			GameObject weaponAttack = GameObject.Instantiate (WeaponAttack)as GameObject;
			weaponAttack.transform.position = WeaponRange.position;
		}
		if((PlayerNo == 0) || (PlayerNo == 3)){
			SoundManager.Instance.Play(9,gameObject);
		}
		if (PlayerNo == 1) {
			SoundManager.Instance.Play(10,gameObject);
		}
		if (PlayerNo == 2) {
			SoundManager.Instance.Play(11,gameObject);	
		}
	}
}
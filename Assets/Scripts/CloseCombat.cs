

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseCombat : MonoBehaviour
{
	public GameObject WeaponAttack00;
	public GameObject WeaponAttack01;
	public GameObject WeaponAttack02;
	public GameObject WeaponAttack03;
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
	public float DelayTime = 0.2f;

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
                    PlayerEquip.WeaponEquip = true;
					damage = Attack;
					animator.SetTrigger ("Attack");
					Invoke("Combat", DelayTime);
				}
			}
		}
	}

	void Combat() 
	{
		if(PlayerNo == 0)
		{
			GameObject weaponAttack = GameObject.Instantiate (WeaponAttack00)as GameObject;
			weaponAttack.transform.position = WeaponRange.position;
			SoundManager.Instance.Play(9,gameObject);
		}
		if(PlayerNo == 3){
			GameObject weaponAttack = GameObject.Instantiate (WeaponAttack03)as GameObject;
			weaponAttack.transform.position = WeaponRange.position;
			SoundManager.Instance.Play(9,gameObject);
		}
		if (PlayerNo == 1) {
			GameObject weaponAttack = GameObject.Instantiate (WeaponAttack01)as GameObject;
			weaponAttack.transform.position = WeaponRange.position;
			SoundManager.Instance.Play(10,gameObject);
		}
		if (PlayerNo == 2) {
			GameObject weaponAttack = GameObject.Instantiate (WeaponAttack02)as GameObject;
			weaponAttack.transform.position = WeaponRange.position;
			SoundManager.Instance.Play(11,gameObject);	
		}
	}
}
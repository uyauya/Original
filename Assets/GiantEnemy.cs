using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemy : MonoBehaviour
{
	public Transform GiantHeadMuzzle;		
	public Transform GiantHandL;
	public Transform GiantHandR;
	public float BeamInterval = 0;
	public float BeamIntervalMax = 20;
	public GameObject exprosion;
	int AttackPhase = 0;
	float AttackPhaseTime = 0.0f;
	public Transform GiantMuzzle;              
	public int TargetPosition;				   
	public float TargetSpeed;				   
	public float MoveSpeed;					   
	protected BossBasic bossBasic;			   
	bool dead = false;
	public static GameObject BossLifeBar;
	private float timeCount;
	public float RandomCount = 0;

	void Start () {
		bossBasic = gameObject.GetComponent<BossBasic> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
	}


	void Update () {
		AttackPhaseTime += Time.deltaTime;
		if( bossBasic.armorPoint <= 0f)
		{
			BossLifeBar.SetActive(false);
			return;	
		}
		if( bossBasic.animator.GetBool("dead") == true ) return;
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.0f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		bossBasic.timer += Time.deltaTime;
		if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) <= TargetPosition)
		{
			timeCount += Time.deltaTime;
			RandomCount -= Time.deltaTime;
			if (RandomCount <= 0)
			{
				RandomAction();
				RandomCount = 5;
			}

		}
		if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) >= TargetPosition)
		{
			if(AttackPhase == 0)
			{
				AttackPhase = 3;
				AttackPhaseTime = 0.0f;
			}
		}
		if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) >= TargetPosition)
		{
			if(AttackPhase == 0)
			{
				AttackPhase = 4;
				AttackPhaseTime = 0.0f;
			}
		}

		switch (AttackPhase)
		{
		case 1:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			if (AttackPhaseTime >= 6)
			{
				AttackPhase = 2;
				AttackPhaseTime = 0;
			}
			bossBasic.shotInterval01 += Time.deltaTime;
			if (bossBasic.shotInterval01 > bossBasic.shotInterval01Max) 
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
					(bossBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
				transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
				bossBasic.animator.SetTrigger ("attack01");
			}
			break;
		case 2:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			if (AttackPhaseTime >= 6)
			{
				AttackPhase = 2;
				AttackPhaseTime = 0;
			}
			bossBasic.shotInterval02 += Time.deltaTime;
			if (bossBasic.shotInterval02 > bossBasic.shotInterval02Max) 
			{
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
					(bossBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
				transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
				bossBasic.animator.SetTrigger ("attack02");
			}
				break;
		case 3:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			if (AttackPhaseTime >= 6)
			{
				AttackPhase = 2;
				AttackPhaseTime = 0;
			}
			bossBasic.shotInterval01 += Time.deltaTime;
			if (bossBasic.shotInterval01 > bossBasic.shotInterval01Max) 
			{
				bossBasic.animator.SetTrigger ("attack03");
			}
				break;
		case 4:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			if (AttackPhaseTime >= 6)
			{
				AttackPhase = 2;
				AttackPhaseTime = 0;
			}
			BeamInterval += Time.deltaTime;
			if (BeamInterval > BeamIntervalMax) 
			{
				bossBasic.animator.SetTrigger ("shout");
			}
				break;
		case 5:
			if (AttackPhaseTime >= 3)
			{
				AttackPhase = 0;
				AttackPhaseTime = 0;
				bossBasic.animator.enabled = false;
			}
			break;
		}
	}
	public void RandomAction()
	{
		int num = Random.Range(0, 8);
		if (num <= 2)
		{
			if(AttackPhase == 0)
			{
				AttackPhase = 1;
				AttackPhaseTime = 0.0f;
			}
		}
		else if (num > 3 && num <= 5)
		{
			if(AttackPhase == 0)
			{
				AttackPhase = 2;
				AttackPhaseTime = 0.0f;
			}
		}
		else
		{
			bossBasic.animator.SetTrigger ("shout");
		}
	}
}

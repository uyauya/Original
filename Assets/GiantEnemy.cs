using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantEnemy : MonoBehaviour
{
	public Animator animator;	
	public Transform BeamMuzzle;	
	public Transform GiantHeadMuzzle;		
	public Transform GiantHandL;
	public Transform GiantHandR;
	public GameObject GiantFire;
	public GameObject GiantBeam;		
	public float GiantShotInterval = 0;
	public float GiantShotIntervalMax = 20;
	public GameObject exprosion;
	public int TargetPosition;				   
	public float TargetSpeed;				   
	public float MoveSpeed;					   
	protected BossBasic bossBasic;			   
	bool dead = false;
    public float Magnification = 1.3f;
    public static GameObject BossLifeBar;
	private float timeCount = 0;
	public float RandomCount = 1;
    int AttackPhase = 0;
    float AttackPhaseTime = 0.0f;
    


	void Start () {
		animator = GetComponent< Animator >();	
		bossBasic = gameObject.GetComponent<BossBasic> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
	}


	void Update () {
        //Debug.Log("up");
		AttackPhaseTime += Time.deltaTime;
		if( bossBasic.armorPoint <= 0f)
		{
			BossLifeBar.SetActive(false);

			return;	
		}
		if( bossBasic.animator.GetBool("dead") == true ) return;
        //Debug.Log("up");
        Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.0f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		bossBasic.timer += Time.deltaTime;
        //近距離
		if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) <= TargetPosition)
		{
            if (AttackPhaseTime >= 1)
            {
                RandomCount -= Time.deltaTime;
                if (RandomCount <= 0)
                {
                    RandomAction();
                    RandomCount = 1;
                    AttackPhaseTime = 0;
                    //Debug.Log("Update");
                }
            }
		}
        //遠距離
		if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) >= TargetPosition)
		{
			if(AttackPhaseTime >= 1)
			{
				AttackPhase = 3;
				AttackPhaseTime = 0.0f;
			}
		}
		if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) >= TargetPosition)
		{
			if(AttackPhase == 2)
			{
				AttackPhase = 4;
				AttackPhaseTime = 0.0f;
			}
		}

		switch (AttackPhase)
		{
		case 1:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
                bossBasic.shotInterval01 += Time.deltaTime;
                Debug.Log(bossBasic.shotInterval01);
                if (bossBasic.shotInterval01 > bossBasic.shotInterval01Max) 
			{
				animator.SetBool("walk", true);
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
					(bossBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
				transform.position += transform.forward * Time.deltaTime * MoveSpeed;
				if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) <= bossBasic.Search) 
				{
					
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
						(bossBasic.battleManager.Player.transform.position - transform.position), Time.deltaTime * bossBasic.EnemySpeed);
					if (bossBasic.armorPoint >= bossBasic.LimitBap) 
					{
						animator.SetTrigger("attack01");
					}  else
					{
						bossBasic.EnemyAttack = bossBasic.EnemyAttack + bossBasic.AddBAttack;
						animator.SetFloat("Speed", Magnification);
						animator.SetTrigger("attack01");
					}
				}
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
				if (Vector3.Distance(bossBasic.battleManager.Player.transform.position, transform.position) <= bossBasic.Search) 
				{
					transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
						(bossBasic.battleManager.Player.transform.position - transform.position), Time.deltaTime * bossBasic.EnemySpeed);
					if (bossBasic.armorPoint >= bossBasic.LimitBap) 
					{
						animator.SetTrigger("attack02");
					}  else
						{
						bossBasic.EnemyAttack = bossBasic.EnemyAttack + bossBasic.AddBAttack;
						animator.SetFloat("Speed", Magnification);
						animator.SetTrigger("attack02");
					}
				}
			}
			break;
		case 3:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
					(bossBasic.target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
				transform.position += transform.forward * Time.deltaTime * MoveSpeed * 5;
				if (bossBasic.armorPoint >= bossBasic.LimitBap) 
				{
					animator.SetTrigger("attack03");
				}
                else
				{
					bossBasic.EnemyAttack = bossBasic.EnemyAttack + bossBasic.AddBAttack;
					animator.SetFloat("Speed", Magnification);
					animator.SetTrigger("attack03");
				}
			break;
		case 4:
			GetComponent<Rigidbody>().velocity = Vector3.zero;
			GiantShotInterval += Time.deltaTime;
			if (GiantShotInterval > GiantShotIntervalMax) 
			{
				if (EnemyTargetRange.isAttackDesision == true)
				{
					animator.SetTrigger ("shout");
					giantFire();
				}
				if ((BeamRange.isBeamDesision == true) && (EnemyTargetRange.isAttackDesision == false))
				{
					if (bossBasic.armorPoint >= bossBasic.LimitBap) 
					{
						animator.SetTrigger ("shout");
						//giantBeam();
					}
				}
				if ((BeamRange.isBeamDesision == false) && (EnemyTargetRange.isAttackDesision == false))
				{
					return;
				}
			}
				break;
		case 5:

			break;
		}
	}
	public void RandomAction()
	{
		int num = Random.Range(0, 8);
		if (num <= 7)
		{
			if(AttackPhase == 0)
			{
				AttackPhase = 1;
				AttackPhaseTime = 0.0f;
			}
		}
		/*else if (num > 3 && num <= 5)
		{
			if(AttackPhase == 0)
			{
				AttackPhase = 1;
				AttackPhaseTime = 0.0f;
			}
		}*/
		else
		{
			animator.SetTrigger ("shout");
		}
	}

	private void giantFire()
	{
		GameObject giantFire = GameObject.Instantiate(GiantFire) as GameObject;
		giantFire.transform.position = GiantHeadMuzzle.position;
		giantFire.transform.rotation = GiantHeadMuzzle.transform.rotation;
	}

	/*private void GiantBeam()
	{
		GameObject giantBeam = GameObject.Instantiate(GiantBeam) as GameObject;
		giantBeam.transform.position = BeamMuzzle.position;
		giantBeam.transform.rotation = BeamMuzzle.transform.rotation;
	}*/
}

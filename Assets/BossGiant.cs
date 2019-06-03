using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGiant : MonoBehaviour
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
	public int TargetRange;				   
	public float TargetSpeed;				   
	public float MoveSpeed;					   
	protected BossBasicR bossBasicR;			   
	bool dead = false;
	public float Magnification = 1.3f;
	public static GameObject BossLifeBar;
	private float timeCount = 0;
	public float RandomCount = 1;
	int AttackPhase = 0;
	float AttackPhaseTime = 0.0f;
	public float Search;
	public GameObject Target;	

	// Start is called before the first frame update
    void Start()
    {
		animator = GetComponent< Animator >();	
		bossBasicR = gameObject.GetComponent<BossBasicR> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
		Target = GameObject.FindWithTag ("Player");	
    }

	void FixedUpdate()
	{
		
	}
    // Update is called once per frame
    void Update()
    {
		animator.SetBool("walk", false);
		AttackPhaseTime += Time.deltaTime;
		if( bossBasicR.armorPoint <= 0f)
		{
			BossLifeBar.SetActive(false);
			return;	
		}
		if(animator.GetBool("dead") == true ) return;
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.0f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		//bossBasic.timer += Time.deltaTime;
		if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= Search) 
		{
			animator.SetBool("walk", true);
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * MoveSpeed);
		}
		//近距離
		/*if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= TargetRange)
		{
			if (AttackPhaseTime >= 1)
			{
				//animator.SetBool("idle", false);
				//animator.SetBool("walk", false);
				//animator.SetTrigger("attack01");
				//RandomAction();
				AttackPhaseTime = 0;
			}
		}*/
    }

	public void RandomAction()
	{
		int num = Random.Range(0, 8);
		if (num <= 7)
		{
			if(AttackPhase == 0)
			{
				animator.SetTrigger("attack01");
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

}

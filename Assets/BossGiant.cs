using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGiant : MonoBehaviour
{
	private Animator animator;	
	public Transform BeamMuzzle;	
	public Transform GiantHeadMuzzle;		
	public Transform GiantHandL;
	public Transform GiantHandR;
	public GameObject GiantFire;
	public GameObject GiantBeam;		
	public float GiantShotInterval = 0;
	public float GiantShotIntervalMax = 20;
	public GameObject exprosion;
	public int CrossRange;
	public int TargetRange;
	public float SearchRange;
	public float MoveSpeed = 5;
	public float RMoveSpeed = 5;
	public float RollSpeed;
	public float StopTime = 3;
	public float LastSpeed;
	protected BossBasicR bossBasicR;			   
	bool dead = false;
	public float Magnification = 1.3f;
	public static GameObject BossLifeBar;
	private float timeCount = 0;
	public float RandomCount = 1;
	int AttackPhase = 0;
	float AttackPhaseTime = 0.0f;
	public GameObject Target;	

	// Start is called before the first frame update
    void Start()
    {
		animator = GetComponent< Animator >();	
		bossBasicR = gameObject.GetComponent<BossBasicR> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
		Target = GameObject.FindWithTag ("Player");	
		RMoveSpeed = MoveSpeed;
    }

	void FixedUpdate()
	{
		if(BossBasicR.isBDamage == true)
		{
			animator.SetTrigger("damaged");
		}
		if(BossBasicR.isBDead == true)
		{
			animator.SetBool("dead", true);
		}
		if( bossBasicR.armorPoint <= 1000f)
		{
			TrailEquip.TrailOn = true;
			BossBasicR.isPowerUp = true;
		}
		if(BossBasicR.isPowerUp == true)
		{
			
		}	
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
		if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= SearchRange) 
		{
            animator.SetBool("walk", true);
            transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);
				GetComponent<Rigidbody>().velocity = (transform.forward * MoveSpeed);
        }
		//遠距離
		if ((Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= SearchRange)
			&&(Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) > TargetRange))
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);
			StartCoroutine("StopCoroutine");
			animator.SetTrigger("shout");
		}
		//近距離
        if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= TargetRange)
		{
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);
				RandomAction();
        }
		if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= CrossRange)
		{
			StartCoroutine("StopCoroutine");
			//Debug.Log(MoveSpeed);
		}
    }

    public void RandomAction()
	{
		int num = Random.Range(0, 9);
		if (num <= 4)
		{
			animator.SetTrigger("attack01");
		}
		else
		{
			animator.SetTrigger("attack02");
		}
	}

	IEnumerator StopCoroutine()
	{         
		MoveSpeed = 0;                      
		yield return new WaitForSeconds(StopTime); 
		MoveSpeed = RMoveSpeed;   
		Debug.Log(MoveSpeed);
	}

}

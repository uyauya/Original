using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGiant : MonoBehaviour
{
	private Animator animator;	
	public Transform GiantHeadMuzzle;		
	public Transform GiantHandL;
	public Transform GiantHandR;
	public GameObject GiantBullet;		
	public float ShotInterval = 0;
	public float ShotIntervalMax = 2;
	public GameObject exprosion;
	public int CrossRange;
	public int TargetRange;
	public int ShotRange;
	public float SearchRange;
	public float MoveSpeed = 5;
	public float RMoveSpeed = 5;
	public float RollSpeed;
	public float StopTime = 6;
	public float LastSpeed;
	protected BossBasicR bossBasicR;			   
	bool dead = false;
	public float Magnification = 1.3f;
	public static GameObject BossLifeBar;
	private float timeCount = 0;
	public float RandomCount = 1;
	public GameObject Target;
	private float ShotSpeed;			// 射出速度
	private Vector3 ShotDirection;		// 射出方向
	public float XspeedS = -0.1f;		// X方向最低速度
	public float XspeedL = 0.1f;		// X方向最高速度	
	public float YspeedS = -0.1f;
	public float YspeedL = 0.1f;
	public float ZspeedS = -0.1f;
	public float ZspeedL = 0.1f;
	public float XdirectionS = -0.1f;	// X方向範囲（ここから）
	public float XdirectionL = 0.1f;	// X方向範囲（ここまで）
	public float YdirectionS = -0.1f;
	public float YdirectionL = 0.1f;
	public float ZdirectionS = -0.1f;
	public float ZdirectionL = 0.1f;
	private int SFrameCount = 0;			 
	public int ShotCount = 50;
	public float DushRate = 10;
	public float ChargeTime = 0;
	public float ChargeTimeMax = 10;
	public float LimitBap = 3000;
	private int AttackPhase = 0;
	public float AttackPhaseTime = 0.0f;


	// Start is called before the first frame update
    void Start()
    {
		animator = GetComponent< Animator >();	
		bossBasicR = gameObject.GetComponent<BossBasicR> ();
		BossLifeBar = GameObject.Find ("BossLife");
		BossLifeBar.SetActive(true);
		Target = GameObject.FindWithTag ("Player");	
		RMoveSpeed = MoveSpeed;
		animator.SetTrigger("shout");
		StartCoroutine("StopCoroutine");
    }

	void FixedUpdate()
	{
		
	}

    // Update is called once per frame
    void Update()
    {
		AttackPhaseTime += Time.deltaTime;
		if(BossBasicR.isBDamage == true)
		{
			Debug.Log("ダメージ");
			animator.SetTrigger("damaged");
			animator.SetFloat("Speed",0.7f);
		}
		if(BossBasicR.isBDead == true)
		{
			Debug.Log("デッド");
			MoveSpeed = 0;
			//TrailEquip.TrailOn = false;
			animator.SetTrigger("isdead");
			animator.SetFloat("Speed",0.5f);
		}
		if( bossBasicR.armorPoint <= LimitBap)
		{
			ParticleEquip.ParticleOn = true;
			//TrailEquip.TrailOn = true;
			//BossBasicR.isPowerUp = true;
		}

		animator.SetBool("walk", true);
		ChargeTime += Time.deltaTime;
		//Debug.Log("ChargeTime" + ChargeTime);
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
				if ((Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= SearchRange)
					&&(Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) > ShotRange))
					{
						AttackPhase = 1;
						//StartCoroutine("StopCoroutine");
					}	
				if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= TargetRange)
					{
						AttackPhase = 2;
						//StartCoroutine("StopCoroutine");
					}	
				//チャージタイムが溜まったら
				if(ChargeTime >= ChargeTimeMax)
					{
						AttackPhase = 3;
						//StartCoroutine("StopCoroutine");
					}	
        }

		if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= CrossRange)
		{
			StartCoroutine("StopCoroutine");
			//Debug.Log(MoveSpeed);
		}

		switch (AttackPhase)
		{
			case 1:
				//遠距離
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);

				SFrameCount += 1;
				if (SFrameCount >= ShotCount)
				{
					StartCoroutine("StopCoroutine");
					animator.SetTrigger("shout");
					GiantShot();
					SFrameCount = 0;
				}
			break;

			case 2:
				//近距離
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
				(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);
				RandomAction();
			break;

			case 3:
				if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= SearchRange)
					{
						
						animator.SetTrigger("running");
						transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
						(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);
						GetComponent<Rigidbody>().velocity = (transform.forward * MoveSpeed * DushRate);
						Debug.Log("突撃");
						if (Vector3.Distance(bossBasicR.battleManager.Player.transform.position, transform.position) <= TargetRange)
						{
							ParticleEquip.ParticleOn = true;
							//TrailEquip.TrailOn = true;
							transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation
							(bossBasicR.battleManager.Player.transform.position - transform.position), Time.deltaTime * RollSpeed);
							animator.SetTrigger("attack03");
							ParticleEquip.ParticleOn = false;
							//TrailEquip.TrailOn = false;
							Debug.Log("ラッシュ");
							ChargeTime = 0;
							Debug.Log("チャージリセット");
							animator.SetBool("walk", true);
						}
						AttackPhase = 0;
						Debug.Log("歩く");
					}
				else
					{
					return;
					}
			break;
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

	private void GiantShot()
	{
		if (GiantBullet != null)
		{

			GameObject giantBullet = Instantiate(GiantBullet, this.transform.position, Quaternion.identity);
			giantBullet.transform.position = GiantHeadMuzzle.position;
			Rigidbody rid = giantBullet.GetComponent<Rigidbody>();
			{
				// スピードをランダムにする（Random.Rangeの場合はfloatにする）
				float x = Random.Range (XspeedS,XspeedL);	//XspeedSからXspeedLまでの数値のランダム
				float y = Random.Range (YspeedS,YspeedL);
				float z = Random.Range (ZspeedS,ZspeedL);
				ShotSpeed = Random.Range (1, 9) / 10f;
				// 方向をランダムにする
				x = Random.Range (XdirectionS,XdirectionL);
				y = Random.Range (YdirectionS,YdirectionL);
				z = Random.Range (ZdirectionS,ZdirectionL);
				ShotDirection = new Vector3 (x, y, z);
				ShotDirection = ShotDirection.normalized;
			}
			// 弾を(ShotSpeed速度* ShotDirection方向）で前進。後ろにForceMode.Impulseをつけて瞬間的に力を加える
			rid.AddForce(ShotSpeed * ShotDirection, ForceMode.Impulse);
		}
		else
		{
			return;
		}
	}

	IEnumerator StopCoroutine()
	{         
		MoveSpeed = 0;                      
		yield return new WaitForSeconds(StopTime); 
		MoveSpeed = RMoveSpeed;  
	}

}

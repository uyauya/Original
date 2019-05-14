using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;	
using UnityEditor;					

public class PlayerShootR : MonoBehaviour {
	public GameObject Bullet01;					
	public GameObject Bullet01B;				
	public GameObject Bullet01C;				
	public GameObject UBullet01;				
	public GameObject UBullet01B;				
	public GameObject UBullet01C;				
	private GameObject bullet01;
	public Transform muzzle;					
	public GameObject muzzleFlash;				
	public GameObject ErekiSmoke;				
	public float interval;
	public float shotInterval;					
	public float shotIntervalMax = 0.25F;
	private float time = 0F;
	private float triggerDownTime = 0F;			
	private float triggerDownTimeStart = 0F;	
	private float triggerDownTimeEnd = 0F;		
	public float Attack = 200;					
	public float attackPoint;					
	private float power = 0;					
	public float damage;						
	public float ChargeTime;					
	public float ChargeTime1 = 1.0f;			
	public float ChargeTime2 = 3.0f;			
	public float AddAttackRate = 2.5f;          
	private float NormalSize = 1.0F;
	public float BigSize;
	private Animator animator;
	private Rigidbody rb;
	public Image gaugeImage;
	public int boostPoint;						
	Bullet01 bullet01_script;					
	public GameObject effectPrefab;				
	public GameObject effectObject;
	public int BpDown = 50;						
	public bool isCharging = false;						
	private AudioSource[] audioSources;
	public int PlayerNo;						
	private Pause pause;						
	public bool isBig;							
	public static bool isShoot = false;			
	//public BattleManager battleManager;

	void Start () {
		gaugeImage = GameObject.Find ("BoostGauge").GetComponent<Image> ();
		audioSources = gameObject.GetComponents<AudioSource>(); 			
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();			
		attackPoint = DataManager.AttackPoint;
		Transform muzzle = GameObject.FindWithTag ("Player").transform.Find("muzzle");
	}

	void Update () {
		//if ((pause.isPause == false) && (PlayerController.IsClear == false) && (PlayerController.IsStop == true)) {
		if (pause.isPause == false) {
			isBig = GameObject.FindWithTag ("Player").GetComponent<PlayerAp> ().isBig;
			//isBig = battleManager.Player.GetComponent<PlayerAp>().isBig;
			if (isBig == false) {	
				if (Input.GetButtonDown ("Fire1")) {
					triggerDownTimeStart = Time.time;
					isCharging = true;
					effectObject = Instantiate (effectPrefab, muzzle.position, Quaternion.identity);
					effectObject.transform.Find ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.red;
					effectObject.transform.SetParent (muzzle);
				}
				if (Input.GetButton ("Fire1")) {
					isShoot = true;
					if (Time.time - triggerDownTimeStart >= ChargeTime1 && Time.time - triggerDownTimeStart <= ChargeTime2) {
						effectObject.GetComponent<ParticleSystem> ().startColor = Color.red;
						effectObject.transform.Find ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.white;
					} else if (Time.time - triggerDownTimeStart > ChargeTime2) {
						effectObject.GetComponent<ParticleSystem> ().startColor = Color.blue;
						effectObject.transform.Find ("ErekiSmoke").GetComponent<ParticleSystem> ().startColor = Color.yellow;
					}
				}
				if (Input.GetButtonUp ("Fire1")) {
					isShoot = false;
					triggerDownTimeEnd = Time.time;
					isCharging = false;
					Destroy (effectObject);
					ChargeTime = triggerDownTimeEnd - triggerDownTimeStart;
					damage = Attack + attackPoint * AddAttackRate * ChargeTime;
					if (GetComponent<PlayerController> ().boostPoint >= BpDown) {
						Bullet ();
						GetComponent<PlayerController> ().boostPoint -= BpDown;
					}
					animator.SetTrigger ("Shot");　	
					if (time >= interval) {    
						time = 0f;
					}
				}
				//Instantiate(muzzleFlash, muzzle.transform.position, transform.rotation);
			}
		}
	}

	void Bullet() 
	{

		if ((DataManager.PlayerNo == 0)|| (DataManager.PlayerNo == 1)|| (DataManager.PlayerNo == 2)) 
		{
			if (ChargeTime <= ChargeTime1) 
			{
				GameObject bulletObject = GameObject.Instantiate (Bullet01)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01R> ().damage = this.damage;
			} else if (ChargeTime1 < ChargeTime && ChargeTime  <= ChargeTime2) 
			{
				GameObject bulletObject = GameObject.Instantiate (Bullet01B)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01RB> ().damage = this.damage;
			} else 
			{
				GameObject bulletObject = GameObject.Instantiate (Bullet01C)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01RC> ().damage = this.damage;
			}
		}
		else if (DataManager.PlayerNo == 3)
		{
			if (ChargeTime <= ChargeTime1) 
			{	
				GameObject bulletObject = GameObject.Instantiate (UBullet01)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01R> ().damage = this.damage;
			} else if (ChargeTime1 < ChargeTime && ChargeTime  <= ChargeTime2) 
			{
				GameObject bulletObject = GameObject.Instantiate (UBullet01B)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01RB> ().damage = this.damage;
			} else 
			{
				GameObject bulletObject = GameObject.Instantiate (UBullet01C)as GameObject;
				bulletObject.transform.position = muzzle.position;
				bulletObject.GetComponent<Bullet01RC> ().damage = this.damage;
			}
		}

		if ((PlayerNo == 0)|| (PlayerNo == 3)){	
			SoundManager.Instance.Play(0,gameObject);
			SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
		}
		if (PlayerNo == 1) {	
			SoundManager.Instance.Play(1,gameObject);	
			SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
		}
		if (PlayerNo == 2) {	
			SoundManager.Instance.Play(2,gameObject);		
			SoundManager2.Instance.PlayDelayed (0, 0.2f, gameObject);
		}
	}

	public void KickEvent (){	
		Debug.Log("kick");
	}


}



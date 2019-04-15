
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Shot : MonoBehaviour 
{
	public GameObject ThrowingShot;  	// 放物線を描く弾
	private float ThrowingSpeed;		// 射出速度
	private float ThrowingAngle;        // 射出角度
	private Vector3 ThrowingAngleDirection;		// 射出方向
	private float Interval = 0;			// 射出間隔
	public float DestroyTime = 5;		// 射出後消滅するまでの時間
	public GameObject explosion;		// 弾の爆発
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
	public float XangleS = 0.0f;			// X方向最低角度
	public float XangleL = 90.0f;			// X方向最高角度	
	public float YangleS = 0.0f;	
	public float YangleL = 90.0f;
	public float ZangleS = 0.0f;	
	public float ZangleL = 90.0f;

	private void Start()
	{
		
	}

	private void Update()
	{
		//ボスが死んだら弾も消滅
		if (BossBasic.BossDead == true)
		{
			Destroy (gameObject);
			//Debug.Log("死亡");
		}
	}

	private void FixedUpdate()
	{
			ThrowingShoot();
	}

	/// ボールを射出する
	private void ThrowingShoot()
	{
		if (ThrowingShot != null)
		{
			{
				// スピードをランダムにする
				float x = Random.Range (XspeedS,XspeedL);
				float y = Random.Range (YspeedS,YspeedL);
				float z = Random.Range (ZspeedS,ZspeedL);
				gameObject.transform.localPosition += new Vector3 (x, y, z);
				ThrowingSpeed = Random.Range (1, 9) / 10f;
				// 方向をランダムにする
				x = Random.Range (XdirectionS,XdirectionL);
				y = Random.Range (YdirectionS,YdirectionL);
				z = Random.Range (ZdirectionS,ZdirectionL);
				ThrowingAngleDirection = new Vector3 (x, y, z);
				// 角度をランダムにする
				float xx = Random.Range (XangleS,XangleL);
				float yy = Random.Range (XangleS,XangleL);
				float zz = Random.Range (XangleS,XangleL);
				ThrowingAngle = Random.Range (1, 9) / 10f;
			}
			//Interval += Time.deltaTime;
			// 出現後一定時間(DestroyTime)で自動的に消滅させる
			Destroy(gameObject, DestroyTime);
			// 弾を(ThrowingAngle * ThrowingSpeed速度）で前進
			gameObject.transform.Translate (ThrowingAngle * ThrowingSpeed　* ThrowingAngleDirection　);
		}
		else
		{
			return;
		}
	}

	private void OnCollisionEnter(Collision collider) 
	{

		//プレイヤータグの付いたオブジェクトと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Shot") 
		{
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}
}


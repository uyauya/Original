
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//弾を連続してランダムに放物線を描くように落とすショット（火山の噴火イメージ）
public class Boss03Shot : MonoBehaviour 
{

    public Transform Bossmuzzle;		// 弾発射元（銃口）
    public GameObject BossShot;  		// 放物線を描く弾
	private float ShotSpeed;			// 射出速度
	private Vector3 ShotDirection;		// 射出方向
	private float Interval = 0;			// 射出間隔
	private int frameCount = 0;			 
	public int ShotCount = 10;
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

	private void Start()
	{
		
	}

	private void Update()
	{

	}

	private void FixedUpdate()
	{
		//カウントを1ずつ加算していき、ShotCountになったらBossShoot処理開始（下記参照）、カウントを0にしてリセット
		frameCount += 1;
		if (frameCount >= ShotCount)
        {
			frameCount = 0;
            BossShoot();
        }
      
	}

	//BossShotを射出する
	private void BossShoot()
	{
		if (BossShot != null)
		{

			GameObject bossShot = Instantiate(BossShot, this.transform.position, Quaternion.identity);
            bossShot.transform.position = Bossmuzzle.position;
            Rigidbody rid = bossShot.GetComponent<Rigidbody>();
			{
				// スピードをランダムにする（Random.Rangeの場合はfloatにする）
				float x = Random.Range (XspeedS,XspeedL);	//XspeedSからXspeedLまでの数値のランダム
				float y = Random.Range (YspeedS,YspeedL);
				float z = Random.Range (ZspeedS,ZspeedL);
				ShotSpeed = Random.Range (1, 9) / 10f;
				// 方向をランダムにする
				x = Random.Range (XdirectionS,XdirectionL);
                y = Random.Range (YdirectionS,YdirectionL);
                //y = 0;
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

}


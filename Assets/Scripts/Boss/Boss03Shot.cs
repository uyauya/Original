
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Shot : MonoBehaviour 
{

    public Transform Bossmuzzle;		// 弾発射元（銃口）
    public GameObject BossShot;  		// 放物線を描く弾
	private float ShotSpeed;			// 射出速度
	private float ShotAngle;        	// 射出角度
	private Vector3 ShotDirection;		// 射出方向
	private float Interval = 0;			// 射出間隔
	private int frameCnt = 0;			 
	//public float DestroyTime = 5;		// 射出後消滅するまでの時間
	//public GameObject explosion;		// 弾の爆発
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
	//public float XangleS = 0.0f;		// X方向最低角度
	//public float XangleL = 90.0f;		// X方向最高角度	
	//public float YangleS = 0.0f;	
	//public float YangleL = 90.0f;
	//public float ZangleS = 0.0f;	
	//public float ZangleL = 90.0f;

	private void Start()
	{
		
	}

	private void Update()
	{
		//ボスが死んだら弾も消滅
		/*if (BossBasic.BossDead == true)
		{
            Debug.Log("死亡");
            Destroy (gameObject);
			
		}*/
	}

	private void FixedUpdate()
	{
        frameCnt += 1;
        if (frameCnt >= 10)
        {
            frameCnt = 0;
            BossShoot();
        }
      
	}

	/// ボールを射出する
	private void BossShoot()
	{
		if (BossShot != null)
		{
			GameObject bossShot = Instantiate(BossShot, this.transform.position, Quaternion.identity);
            bossShot.transform.position = Bossmuzzle.position;
            Rigidbody rid = bossShot.GetComponent<Rigidbody>();
			{
				// スピードをランダムにする
				float x = Random.Range (XspeedS,XspeedL);
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
                // 角度をランダムにする
                /*float xr = Random.Range (XangleS,XangleL);
				float yr = Random.Range (XangleS,XangleL);
				float zr = Random.Range (XangleS,XangleL);
				ShotAngle = Random.Range (1, 9) / 10f;*/
			}
            // 弾を(ShotAngle角度 * ShotSpeed速度* ShotDirection方向）で前進
            //rid.AddForce(ShotAngle * ShotSpeed　* ShotDirection, ForceMode.Impulse);
            rid.AddForce(ShotSpeed * ShotDirection, ForceMode.Impulse);
            //出現後一定時間(DestroyTime)で自動的に消滅させる
            //Destroy(gameObject, DestroyTime);
			//frameCnt += 10;
			// フレームカウントリセット用
			//if (10000 <= frameCnt) {
				//frameCnt = 0;
			//}
		}
		else
		{
			return;
		}
	}

	/*private void OnCollisionEnter(Collision collider) 
	{

		//プレイヤータグの付いたオブジェクトと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Shot") 
		{
            Debug.Log("衝突");
            Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
		}
	}*/

}


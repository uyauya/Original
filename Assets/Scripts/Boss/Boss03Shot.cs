
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Shot : MonoBehaviour 
{
	private GameObject ThrowingObject;  // 射出するオブジェクト
	private float ThrowingSpeed;		// 射出速度
	private float ThrowingAngle;        // 射出角度
	private float Interval = 0;			// 射出間隔
	public float XspeedS = -0.1f;		// X方向最低速度
	public float XspeedL = 0.1f;		// X方向最高速度	
	public float YspeedS = -0.1f;
	public float YspeedL = 0.1f;
	public float ZspeedS = -0.1f;
	public float ZspeedL = 0.1f;
	public float XangleS = -0.1f;		// X方向最低角度
	public float XangleL = 0.1f;		// X方向最高角度	
	public float YangleS = -0.1f;
	public float YangleL = 0.1f;
	public float ZangleS = -0.1f;
	public float ZangleL = 0.1f;

	private void Start()
	{
		Collider collider = GetComponent<Collider>();
		if (collider != null)
		{
			// 干渉しないようにisTriggerをつける
			collider.isTrigger = true;
		}
	}

	private void FixedUpdate()
	{
			ThrowingShot();
	}

	/// ボールを射出する
	private void ThrowingShot()
	{
		if (ThrowingObject != null)
		{
			// Ballオブジェクトの生成
			GameObject Boss03Bullet = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);
			// 射出角度
			float angle = ThrowingAngle;
			// 射出速度を算出
			//Vector3 velocity = CalculateVelocity(this.transform.position, 10, angle);
			// 射出
			Rigidbody rid = Boss03Bullet.GetComponent<Rigidbody>();
			//rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
		}
		else
		{
			return;
		}
	}

	/// 標的に命中する射出速度の計算
	/// <param name="pointA">射出開始座標</param>
	/// <param name="pointB">標的の座標</param>
	/// <returns>射出速度</returns>
	private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
	{
		// 射出角をラジアンに変換
		float rad = angle * Mathf.PI / 180;
		// 水平方向の距離x
		float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));
		// 垂直方向の距離y
		float y = pointA.y - pointB.y;
		// 斜方投射の公式を初速度について解く
		float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

		if (float.IsNaN(speed))
		{
			// 条件を満たす初速を算出できなければVector3.zeroを返す
			return Vector3.zero;
		}
		else
		{
			return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
		}
	}
}


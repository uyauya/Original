using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltPlayer : MonoBehaviour {

	// 移動に与える力
	[SerializeField] private float   m_movePower = 500.0f;

	// 前回与えた移動の力
	private Vector3 m_prevVelocity  = Vector3.zero;

	void Update()
	{
		var body        = GetComponent<Rigidbody>();

		// 前回与えた力の逆方向の力を与えて相殺
		body.AddForce( -m_prevVelocity );

		var velocity    = Vector3.zero;

		if( Input.GetKey( KeyCode.UpArrow ) )
		{
			velocity   += Vector3.forward;
		}
		if( Input.GetKey( KeyCode.DownArrow ) )
		{
			velocity   += Vector3.back;
		}
		if( Input.GetKey( KeyCode.LeftArrow ) )
		{
			velocity   += Vector3.left;
		}
		if( Input.GetKey( KeyCode.RightArrow ) )
		{
			velocity   += Vector3.right;
		}

		velocity   *= m_movePower;
		body.AddForce( velocity );

		// 与えた力を保存
		m_prevVelocity  = velocity;
	}
}

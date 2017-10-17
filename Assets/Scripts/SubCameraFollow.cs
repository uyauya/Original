using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubCameraFollow : MonoBehaviour 
	{
		public Transform target;
		public float height;
		//public float smoothing = 0.5f;
		Vector3 offset;

		/// <summary>
		/// Start this instance.
		/// </summary>
		void Start ()
		{
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			// カメラとターゲット（プレイヤー)の距離を設定
			offset = transform.position - target.position;
			height = transform.position.y;
		}

		/// <summary>
		/// Lates the update.
		/// </summary>
		void LateUpdate ()
		{
			// カメラがターゲット（プレイヤー）を見つけてから追いかける（少し遅れて追いかける）
			//transform.position = target.position + offset;
			transform.position = new Vector3(target.position.x, height, target.position.z);
		}
	}

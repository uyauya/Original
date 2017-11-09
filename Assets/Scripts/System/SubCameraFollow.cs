using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤーを自動追尾するサブカメラ（上から視点）
public class SubCameraFollow : MonoBehaviour 
	{
		public Transform target;
		public float height;
		//public float smoothing = 0.5f;
		Vector3 offset;

		void Start ()
		{
			// Playerタグの付いたオブジェクトの位置をtargetとして取得
			target = GameObject.FindGameObjectWithTag ("Player").transform;
			// カメラとターゲット（プレイヤー)の距離を設定
			offset = transform.position - target.position;
			// y（縦）位置固定してプレイヤを真ん中に固定する
			height = transform.position.y;
		}
		
		void LateUpdate ()
		{
			// カメラがターゲット（プレイヤー）を見つけてから追いかける（少し遅れて追いかける）
			//transform.position = target.position + offset;
			transform.position = new Vector3(target.position.x, height, target.position.z);
		}
	}

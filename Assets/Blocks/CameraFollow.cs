using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;
	public float height;
	//public float smoothing = 0.5f;
	Vector3 offset;

	void Start ()
	{
		target = GameObject.FindGameObjectWithTag ("Player").transform;
		// カメラとターゲット（プレイヤー)の距離を設定
		offset = transform.position - target.position;
	}
		
	void LateUpdate ()
	{
		// カメラがターゲット（プレイヤー）を見つけてから追いかける（少し遅れて追いかける）
		transform.position = target.position + offset;
	}
}


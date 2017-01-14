using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour 
{
	public Transform target;
	public float smoothing = 5f;
	Vector3 offset;

	void Start()
	{
		// カメラとターゲット（プレイヤー)の距離を設定
		offset = transform.position - target.position;
	}

	void FixedUpdate()
	{
		// カメラがターゲット（プレイヤー）を見つけてから追いかける（少し遅れて追いかける）
		Vector3 targetCamPos = target.position + offset;
		transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
	}
}


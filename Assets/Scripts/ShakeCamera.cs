using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// カメラの揺れ(MainCamera、SubCameraにつける）
public class ShakeCamera : MonoBehaviour
{

	public float shake_decay = 0.001f;			// 揺れの減衰率
	public float coef_shake_intensity = 0.1f;	// 揺れの強さ
	private Vector3 originPosition;				// 揺れの元の場所
	private Quaternion originRotation;			// 揺れの角度
	private float shake_intensity;				// 揺れの強さ
	
	void Update ()
	{  
		if (shake_intensity > 0) {  
			transform.position = originPosition + Random.insideUnitSphere * shake_intensity;  
			transform.rotation = new Quaternion (
				originRotation.x + Random.Range (-shake_intensity, shake_intensity) * 1.1f,  
				originRotation.y + Random.Range (-shake_intensity, shake_intensity) * 1.1f,  
				originRotation.z + Random.Range (-shake_intensity, shake_intensity) * 1.0f,  
				originRotation.w + Random.Range (-shake_intensity, shake_intensity) * 1.0f);  
			shake_intensity -= shake_decay;  
		}  
	}

	public void Shake ()
	{  
		originPosition = transform.position;  
		originRotation = transform.rotation;  
		shake_intensity = coef_shake_intensity;  
	}  

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{

	public float shake_decay = 0.001f;
	public float coef_shake_intensity = 0.1f;
	private Vector3 originPosition;
	private Quaternion originRotation;
	private float shake_intensity;
	
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

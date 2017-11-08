using UnityEngine;
using System.Collections;

public class CamVibrationManager : MonoBehaviour {

	public static float vibration;
	public float VibSizeX = -1;
	public float VibSizeY = -1;
	public float VibSizeZ = -1;
	Vector3 defaultPosition;

	void Start () {
		vibration = 0;		
		defaultPosition = transform.localPosition;		// 初期値
	}
	
	// Update is called once per frame
	void Update () {
		vibration = Mathf.Clamp (vibration, 0, 0.5F);
		if (vibration > 0) {
			// カメラ位置をランダムにし、揺れて見せる（下のvibration * の値が揺らす値）
			Vector3 randomPosition;
			randomPosition.x = Random.Range (vibration * VibSizeX, vibration);
			randomPosition.y = Random.Range (vibration * VibSizeY, vibration);
			randomPosition.z = Random.Range (vibration * VibSizeZ, vibration);
			transform.localPosition = defaultPosition + randomPosition;
			vibration -= Time.deltaTime;
		} else {
			transform.localPosition = defaultPosition;
		}
	}
}

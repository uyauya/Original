using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

	public float ShakeTime = 0.5f;
	public Vector3 ShakeRange = new Vector3(0.2f, 0.2f, 0f);

	private float shakeTime;
	private float timer;

	private Vector3 originPos;
	private bool onShakeEnd;

	void Start () {
		//init
		shakeTime = -1f;
		timer = 0f;
		originPos = transform.position;
		onShakeEnd = false;
	}
	
	void Update () {
		if (timer <= shakeTime) {
			onShakeEnd = true;
			timer += Time.deltaTime;
			transform.position = originPos + MulVector3(ShakeRange, Random.insideUnitSphere);
		} else {
			if (onShakeEnd) {
				transform.position = originPos;
				onShakeEnd = false;
			}
			originPos = transform.position;
		}
	}
	public void Shake() {
		timer = 0f;
		shakeTime = ShakeTime;
	}

	private Vector3 MulVector3(Vector3 a, Vector3 b) {
		return new Vector3(a.x * b.x, a.y * b.y, a.z * b.z);
	}
}

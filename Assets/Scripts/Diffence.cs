using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diffence : MonoBehaviour {
	/*private Animator animator;
	public bool diffence = false;				// ディフェンス状態かどうか
	public bool push = false;					// 1回目方向キー入れたかどうか
	public float nextPushTime;					// 2回目方向キー入れるまでの時間
	private float nowTime = 0.0f;				// 1回目と2回目の時間差
	private float limitAngle = 30;				// 方向キーの向きの誤差の許容範囲用
	private float limitTime = 1.0f;				// 時間差上限
	private Vector2 direction = Vector2.zero;	// 方向キーの押した方向

	void Start () {
		animator = GetComponent<Animator>();
	}
		
	void Update () {
		if (!diffence) {
			if ((Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical"))) {
				if (!push) {
					push = true;
					direction = new Vector2 (Input.GetButtonDown ("Horizontal"), Input.GetButtonDown ("Vertical"));
					nowTime = 0.0f;
				} else {
					var nowDirection = new Vector2 (Input.GetButtonDown ("Horizontal"), Input.GetButtonDown ("Vertical"));
					if (Vector2.Angle (nowDirection, direction) < limitAngle && ( nowTime <= limitTime) ) {
						diffence = true;
						animator.SetBool ("Diffence") = true;
						}
					}
				}
			}
	
			if (push) {
				nowTime += Time.deltaTime;
			if (nowTime > limitTime) {
					push = false;
				}
			}
	}*/

}
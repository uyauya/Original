using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {
	//HingiJointコンポーネントを入れる
	private HingeJoint MyHingeJoint;

	//初期の傾き
	private float DefaultAngle = 20;
	//弾いた時の傾き
	private float FlickAngle = -20;

	// Use this for initialization
	void Start () {
		//HinjiJointコンポーネント取得
		this.MyHingeJoint = GetComponent<HingeJoint>();

		//フリッパーの傾きを設定
		SetAngle(this.DefaultAngle);
	}

	// Update is called once per frame
	void Update () {

		//左矢印キーを押した時左フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.LeftArrow) && tag == "LeftFripper") {
			SetAngle (this.FlickAngle);
		}
		//右矢印キーを押した時右フリッパーを動かす
		if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripper") {
			SetAngle (this.FlickAngle);
		}

		//矢印キー離された時フリッパーを元に戻す
		if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripper") {
			SetAngle (this.DefaultAngle);
		}
		if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripper") {
			SetAngle (this.DefaultAngle);
		}
	}

	//フリッパーの傾きを設定
	public void SetAngle (float angle){
		JointSpring jointSpr = this.MyHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.MyHingeJoint.spring = jointSpr;
	}
}

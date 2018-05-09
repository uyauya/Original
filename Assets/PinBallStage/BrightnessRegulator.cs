using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrightnessRegulator : MonoBehaviour {
	// Materialを入れる
	Material myMaterial;

	// Emissionの最小値
	public float minEmission = 0.3f;
	// Emissionの強度
	public float magEmission = 2.0f;
	// 角度
	public int degree = 0;
	//発光速度
	public int speed = 10;
	// ターゲットのデフォルトの色
	public Color Mywhite;
	public Color MyYellow;
	public Color MyRed;

	// Use this for initialization
	void Start () {

		// タグによって光らせる色を変える
		if (tag == "SmallStarTag") {
			Color Mywhite;
		} else if (tag == "LargeStarTag") {
			Color MyYellow;
		}else if(tag == "SmallCloudTag" || tag == "LargeCloudTag") {
			Color MyRed;
		}

		//オブジェクトにアタッチしているMaterialを取得
		this.myMaterial = GetComponent<Renderer> ().material;

		//オブジェクトの最初の色を設定
		myMaterial.SetColor ("_EmissionColor", Mywhite * minEmission);
	}

	// Update is called once per frame
	void Update () {

		if (this.degree >= 0) {
			// 光らせる強度を計算する
			Color emissionColor = Mywhite * (this.minEmission + Mathf.Sin (this.degree * Mathf.Deg2Rad) * this.magEmission);

			// エミッションに色を設定する
			myMaterial.SetColor ("_EmissionColor", emissionColor);

			//現在の角度を小さくする
			this.degree -= this.speed;
		}
	}

	//衝突時に呼ばれる関数
	void OnCollisionEnter(Collision other) {
		//角度を180に設定
		this.degree = 180;
	}
}

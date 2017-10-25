using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 時間経過と共にライトを消灯→点灯、朝→夜を繰り返す時間推移を表現
// インスペクタでライトのColorとIntensityで色と明るさを調整しておく。ライトの場所にも注意
public class LightChange : MonoBehaviour {
	public float count;						// ライト消灯・点灯時間計測用
	private GameObject lightAfternoon;		// ディレクショナルライト昼用（明るい）
	private GameObject lightEvening;		// ディレクショナルライト昼用（やや暗め）
	private GameObject lightNight;			// ディレクショナルライト昼用（暗い）
	private GameObject lightMorning;		// ディレクショナルライト昼用（やや明るめ）
	public bool isLightAfternoon = true;	// LightAfternoonを点灯しておく
	public bool isLightEvening = true;
	public bool isLightNight = true;
	public bool isLightMorning = true;
	public int CountAfternoon = 5;			// lightAfternoonが消灯する時間
	public int CountEvening = 10;			// lightEveningが消灯する時間
	public int CountNight = 15;				// lightNightが消灯する時間
	public int CountMorning = 20;			// lightNightが点灯する時間
	public int CountNoon = 25;				// lightAfternoonが点灯する時間

	void Start () {
		// LightAfternoonという名前のオブジェクトをlightAfternoonと呼ぶことにする
		lightAfternoon = GameObject.Find ("LightAfternoon");
		lightEvening = GameObject.Find ("LightEvening");
		lightNight 	 = GameObject.Find ("LightNight");
		lightMorning = GameObject.Find ("LightMorning");
	}

	void Update () {
		// 時間経過と共にライトを消灯→点灯
		// countに経過時間を足していく（時間計測開始）
		count += Time.deltaTime;
		// CountAfternoonに設定しているの時間を過ぎたらLightMorningとLightAfternoonを消灯
		if (count > CountAfternoon) {
			lightMorning.SetActive(isLightMorning == false);
			lightAfternoon.SetActive(isLightAfternoon == false);
		}
		if (count > CountEvening) {
			lightEvening.SetActive(isLightEvening == false);
		}
		if (count > CountNight) {
			lightNight.SetActive (isLightNight == false);
		}
		if (count > CountMorning) {
			lightMorning.SetActive (isLightMorning == true);
		}
		if (count > CountNoon) {
			lightAfternoon.SetActive (isLightAfternoon == true);
			// countをリセット（Updateの最初に戻す）
			count = 0;
		}
	}
}

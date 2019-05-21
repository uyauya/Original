using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 時間経過と共にライトを消灯→点灯、朝→夜を繰り返す時間推移を表現
// インスペクタでライトのColorとIntensityで色と明るさを調整しておく。ライトの場所にも注意
public class LightChange : MonoBehaviour {
	public float count;						// ライト消灯・点灯時間計測用
	public GameObject directionalLight;
    private GameObject lightMorning;        // ディレクショナルライト昼用（やや明るめ）
    private GameObject lightAfternoon;		// ディレクショナルライト昼用（明るい）
	private GameObject lightEvening;		// ディレクショナルライト昼用（やや暗め）
	private GameObject lightNight;			// ディレクショナルライト昼用（暗い）
	public int CountMorning = 0;           // lightNightが点灯する時間
    public int CountAfternoon = 5;			// lightAfternoonが消灯する時間
	public int CountEvening = 10;			// lightEveningが消灯する時間
	public int CountNight = 15;				// lightNightが消灯する時間
	public int CountMidnight = 20;			// lightAfternoonが点灯する時
    public int CountEnd = 22;          // lightAfternoonが点灯する時

    void Start () {
        // LightAfternoonという名前のオブジェクトをlightAfternoonと呼ぶことにする
        lightMorning = GameObject.Find("LightMorning");
        lightAfternoon = GameObject.Find ("LightAfternoon");
		lightEvening = GameObject.Find ("LightEvening");
		lightNight 	 = GameObject.Find ("LightNight");
	    directionalLight = GameObject.Find("DirectionalLight");
        lightMorning.SetActive(false);
        lightAfternoon.SetActive(false);
        lightEvening.SetActive(false);
        lightNight.SetActive(false);
        directionalLight.SetActive(false);
    }

    void Update()
    {
        // 時間経過と共にライトを消灯→点灯
        // countに経過時間を足していく（時間計測開始）
        count += Time.deltaTime;
        // CountAfternoonに設定しているの時間を過ぎたらLightMorningとLightAfternoonを消灯
        if (count > CountMorning)
        {
            lightMorning.SetActive(true);
            lightAfternoon.SetActive(false);
            lightEvening.SetActive(false);
            lightNight.SetActive(false);
        }
        if (count > CountAfternoon)
        {
            lightMorning.SetActive(false);
            lightAfternoon.SetActive(true);
            lightEvening.SetActive(false);
            lightNight.SetActive(false);
        }
        if (count > CountEvening)
        {
            lightMorning.SetActive(false);
            lightAfternoon.SetActive(false);
            lightEvening.SetActive(true);
            lightNight.SetActive(false);
        }
        if (count > CountNight)
        {
            lightMorning.SetActive(false);
            lightAfternoon.SetActive(false);
            lightEvening.SetActive(false);
            lightNight.SetActive(true);
        }

        if (count > CountMidnight)
        {
            lightMorning.SetActive(false);
            lightAfternoon.SetActive(false);
            lightEvening.SetActive(false);
            lightNight.SetActive(false);
        }
        if (count > CountMidnight)
        {
            count = 0;
        }
    }
}

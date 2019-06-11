using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//ボスのライフバー表示用
public class BossLifeBar : MonoBehaviour {

	public BossBasic bossBasic;
    public BossBasicR bossBasicR;
	private Slider hpSlider;

	// Use this for initialization
	void Start () {
		//ボスライフバー（画面表示用）とボスライフを連動させる
		hpSlider = transform.Find ("Slider").GetComponent <Slider>();
		hpSlider.value = (float) bossBasic.GetarmorPointMax () / (float)bossBasic.GetarmorPointMax ();
        hpSlider.value = (float)bossBasicR.GetarmorPointMax() / (float)bossBasicR.GetarmorPointMax();
    }

	// Update is called once per frame
	void Update () {

	}

	public void SetDisable() {
		gameObject.SetActive (false);
	}

	public void UpdateArmorPointValue() {
		//Debug.Log (hpSlider);
		if(hpSlider != null) {
			hpSlider.value = (float) bossBasic.GetarmorPoint () / (float) bossBasic.GetarmorPointMax ();
            hpSlider.value = (float)bossBasicR.GetarmorPoint() / (float)bossBasicR.GetarmorPointMax();
            //Debug.Log ("armorPoint");
        }
	}

}


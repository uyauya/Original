using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour {

	private EnemyBasic enemyBasic;

	private Slider hpSlider;

	// Use this for initialization
	void Start () {
		enemyBasic = transform.root.GetComponent <EnemyBasic> ();
		hpSlider = transform.Find ("EnemyAp").GetComponent <Slider>();
		hpSlider.value = (float) enemyBasic.GetarmorPointMax () / (float) enemyBasic.GetarmorPointMax ();
	}
	
	// Update is called once per frame
	void Update () {
		//transform.rotation = Camera.main.transform.rotation;
		//transform.LookAt(Camera.main.transform);
		gameObject.transform.LookAt(GameObject.Find("MainCamera").transform);
	}

	public void SetDisable() {
		gameObject.SetActive (false);
	}

	public void UpdateArmorPointValue() {
		hpSlider.value = (float) enemyBasic.GetarmorPoint () / (float) enemyBasic.GetarmorPointMax ();
	}

}

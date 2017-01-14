using UnityEngine;
using System.Collections;

public class BoostEffect : MonoBehaviour {
	// ImportPackageでEffectsをインポート
	// プレイヤーの任意の場所にライトを追加してboostKightに名前変更
	// 
	// CullingMaskをプレイヤーに設定すればプレイヤーの周りだけ光るようになる
	public GameObject boostLight;

	void Start () {
		boostLight.SetActive (false);
	}

	void Update () {
		
		bool flgBoost = false;
		
		//ブーストorジャンプ時エフェエクト効果
		if (Input.GetButton ("Boost") || Input.GetButton ("Jump"))
			flgBoost = true;
			
		boostLight.SetActive (flgBoost);
	}
}

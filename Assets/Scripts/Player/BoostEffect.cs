using UnityEngine;
using System.Collections;

public class BoostEffect : MonoBehaviour {
	// ImportPackageでEffectsをインポート
	// プレイヤーの任意の場所にライトを追加してboostLightに名前変更
	// CullingMaskをプレイヤーに設定すればプレイヤーの周りだけ光るようになる
	public GameObject boostLight;
	//public GameObject DashLight;
	private bool FlagBoost = false;
	//private bool DashBoost = false;

	void Start () 
	{
		//初期設定ではライトオフ
		boostLight.SetActive (false);
		//DashLight.SetActive (false);
	}

	void Update () 
	{
		//ブーストorジャンプ時エフェエクト効果
		if (PlayerController.isBoost)
		{
			FlagBoost = true;
			boostLight.SetActive (FlagBoost);
		}
		/*else if (PlayerController.isDash)
		{
			DashBoost = true;
			DashLight.SetActive (FlagBoost);
			FlagBoost = false;
			boostLight.SetActive (false);
		}
		else 
		{
			return;
		}*/
			
	}
}

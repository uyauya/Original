using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// Canvas内のユーザーインターフェースは、階層として下にある方角が、実行時は上になる
public class Marker : MonoBehaviour {

	Image marker;
	public Image markerImage;
	GameObject compass;
	GameObject target;

	void Start () {
		
		target = GameObject.Find("PlayerTarget");
		//マーカーをレーダー（コンパス）上に表示する
		compass = GameObject.Find ("CompassMask");
		marker = Instantiate(markerImage, compass.transform.position, Quaternion.identity) as Image;
		// transform.SetParentで子オブジェクトにしてfalseにすれば子オブジェクト時でもスケールを維持する
		marker.transform.SetParent(compass.transform, false);
		
	}

	void Update () {
	
		//マーカーをプレイヤーの相対位置に配置する
		// 現在の敵の壱からターゲットの位置を引く
		// 3次元のX,Z座標から2次元UIのX,Y座標に置き換える
		Vector3 position = transform.position - target.transform.position;
		marker.transform.localPosition = new Vector3 (position.x, position.z, 0);
		
		/*
		//レーダーの範囲外に出たら表示しない
		if (Vector3.Distance (target.transform.position, transform.position) <= 150)
			marker.enabled = true;
		else
			marker.enabled = false;
		*/
	}
	
	//敵が消滅したらマーカーも消滅させる
	void OnDestroy() {
		Destroy(marker);
	}
}

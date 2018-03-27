using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator3 : MonoBehaviour {

	public Transform blockPrefab;
	public Transform topWall;   	//上の壁の参照を追加
	public int placeX;         	 	//横に並べる個数
	public int placeZ;          	//奥に並べる個数
	public float totalDepth;    	//奥に並べる座標

	// Use this for initialization
	void Start () {
		//配置する座標を設定
		Vector3 placePosition = new Vector3(
			topWall.position.x-topWall.localScale.x/2+blockPrefab.localScale.x/2,
			0,
			topWall.position.z-topWall.localScale.z/2-blockPrefab.localScale.z/2);

		//配置する回転角を設定
		Quaternion q = new Quaternion();
		q= Quaternion.identity;				//回転なし

		//幅と奥行きを調整
		Vector3 localscale = blockPrefab.localScale;
		localscale.x = topWall.localScale.x / placeX;
		localscale.z = totalDepth / placeZ;
		blockPrefab.localScale = localscale;

		//配置
		for (int i = 0; i < placeZ; i++)
		{
			Vector3 currentPlacePosition
			= placePosition
				- Vector3.forward * blockPrefab.localScale.z * i;
			for(int j = 0; j < placeX; j++)
			{
				Instantiate(blockPrefab, currentPlacePosition, q);
				currentPlacePosition.x += blockPrefab.transform.localScale.x;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

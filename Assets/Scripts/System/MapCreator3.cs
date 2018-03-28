using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator3 : MonoBehaviour {

	public Transform BlockPrefab;
	//public Transform BaseWall;   	//上の壁の参照を追加
	public int PlaceX;         	 	//横に並べる個数
	public int PlaceZ;          	//奥に並べる個数
	public float TotalDepthX;    	//奥に並べる座標
	public float TotalDepthZ;    	//奥に並べる座標

	// Use this for initialization
	void Start () {
		//配置する座標を設定
		Vector3 placePosition = new Vector3(-1,-9,-1);
		/*Vector3 placePosition = new Vector3(
			BaseWall.position.x-BaseWall.localScale.x/2+BlockPrefab.localScale.x/2,
			0,
			BaseWall.position.z-BaseWall.localScale.z/2-BlockPrefab.localScale.z/2);*/

		//配置する回転角を設定
		Quaternion q = new Quaternion();
		q= Quaternion.identity;				//回転なし

		//幅と奥行きを調整
		Vector3 localscale = BlockPrefab.localScale;
		//localscale.x = BaseWall.localScale.x / PlaceX;
		//localscale.z = TotalDepth / PlaceZ;
		localscale.x = TotalDepthX / PlaceX;
		localscale.z = TotalDepthZ / PlaceZ;
		BlockPrefab.localScale = localscale;

		//配置
		for (int i = 0; i < PlaceZ; i++)
		{
			Vector3 currentPlacePosition
			= placePosition
				- Vector3.forward * BlockPrefab.localScale.z * i;
			for(int j = 0; j < PlaceX; j++)
			{
				Instantiate(BlockPrefab, currentPlacePosition, q);
				currentPlacePosition.x += BlockPrefab.transform.localScale.x;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

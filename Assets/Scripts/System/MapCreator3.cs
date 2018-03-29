using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator3 : MonoBehaviour {

	public Transform BlockPrefab;
	public Transform WallPrefab;
	public int PlaceX;         	 	//横に並べる個数
	public int PlaceZ;          	//奥に並べる個数
	public float TotalDepthX;    	//奥に並べる座標
	public float TotalDepthZ;    	//奥に並べる座標
	public float StartPointX;
	public float StartPointY;
	public float StartPointz;


	// Use this for initialization
	void Start () {
		//配置する座標を設定
		Vector3 placePosition = new Vector3(StartPointX,StartPointY,StartPointz);

		//配置する回転角を設定
		Quaternion q = new Quaternion();
		q= Quaternion.identity;				//回転なし

		//幅と奥行きを調整
		Vector3 localscale = BlockPrefab.localScale;
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

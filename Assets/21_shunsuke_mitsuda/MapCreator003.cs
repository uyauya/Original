using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator003 : MonoBehaviour
{
	public Vector3 placePosition;	//配置する座標
	public Transform BlockPrefab;
	//public Transform BaseWall;    //上の壁の参照を追加
	public int PlaceX;              //横に並べる個数
	public int PlaceZ;              //奥に並べる個数
	public float TotalDepthX;       //奥に並べる座標
	public float TotalDepthZ;       //奥に並べる座標

	// Use this for initialization
	void Start()
	{

		//配置する回転角を設定
		Quaternion q = new Quaternion();
		q = Quaternion.identity;                //回転なし

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

			Debug.Log (currentPlacePosition);
			for (int j = 0; j < PlaceX; j++)
			{
				Transform tx = Instantiate(BlockPrefab, currentPlacePosition, q);   // ルートに広げると面倒なので名前を付けて子オブジェクトとする
				currentPlacePosition.x += BlockPrefab.transform.localScale.x;
				tx.SetParent(this.transform);
				tx.gameObject.name = BlockPrefab.name + System.String.Format("{0:00}", i) + System.String.Format("{0:00}", j);
			}
		}
		// ここからy軸にも生成する

		for (int i = 0; i < PlaceZ; i++) {
			Vector3 currentPlacePosition = placePosition + Vector3.up - Vector3.forward * BlockPrefab.localScale.z * i;

			if (i == 0 || i == PlaceZ - 1) {
				for (int j = 0; j < PlaceX; j++) {
					Transform tx = Instantiate (BlockPrefab, currentPlacePosition, q);   
					currentPlacePosition.x += BlockPrefab.transform.localScale.x;
					tx.SetParent (this.transform);
					tx.gameObject.name = BlockPrefab.name + System.String.Format ("{0:00}", i) + System.String.Format ("{0:00}", j);
				}
			} else {
				for (int j = 0; j < PlaceX; j++) {
					if (j == 0 || j == PlaceX - 1) {
						Transform tx = Instantiate (BlockPrefab, currentPlacePosition, q);  
						tx.SetParent (this.transform);
						tx.gameObject.name = BlockPrefab.name + System.String.Format ("{0:00}", i) + System.String.Format ("{0:00}", j);
					}
					currentPlacePosition.x += BlockPrefab.transform.localScale.x;
				}
			}
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
}
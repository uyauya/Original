using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapCreator2 : MonoBehaviour
{
	// ワールド生成のための設定値
	public int MAP_SIZE_X;						// マップ横サイズ
	public int MAP_SIZE_Z;						// マップ縦サイズ
	public int MaxHeight;						// 陸ブロックの高さの最大
	public int Smoothness;
	public int WaterHeight;						// 水ブロックの高さ
	public Transform BlockHeight;
	public float EmitterTime = 1.0f;

	public GameObject Prefab_BL;				// 陸ブロック格納
	public GameObject Prefab_Water;				// 水ブロック格納
	public GameObject		player;				// プレイヤーオブジェクト格納用
	public GameObject[] 	Prefab_Player;
	public	GameObject[]	prefab_enemy;		// 敵の格納用のプレファブ配列


	// 起動時一番最初に選んだプレイヤーをマップに作成。（プレイヤーはバトルマネージャースクリプトで判別・管理）
	void Awake(){
		player = Instantiate (Prefab_Player [DataManager.PlayerNo]);
	}

	void Start()
	{
		Prefab_BL.SetActive(false);
		Prefab_Water.SetActive(false);
		Create();
	}

	void Create()
	{
		// Mathf.PerinNoise()に渡す値が同じだと常に同じ地系になるため、毎回地系が変わるようランダム値を生成
		var randX = Random.value * 100000;
		var randY = Random.value * 100000;

		for (var x = 0; x < MAP_SIZE_X; x++)
		{
			for (var z = 0; z < MAP_SIZE_Z; z++)
			{
				// パーリンノイズの計算で得られた値を整数にし、地面の高さとする
				var groundY = Mathf.RoundToInt(Mathf.PerlinNoise((float)x / Smoothness + randX, (float)z / Smoothness + randY) * MaxHeight);
				// groundYの高さまで地面ブロックを積み上げる
				for (var y = 0; y <= groundY; y++)
				{
					var block = Instantiate(Prefab_BL);
					block.SetActive(true);
					block.transform.SetParent(BlockHeight);
					block.transform.localPosition = new Vector3(x, y, z);
				}
				// 地面の高さがwaterHeight未満の場合はwaterHeightまで水ブロックを積み上げる
				for (var y = groundY + 1; y <= WaterHeight; y++)
				{
					var water = Instantiate(Prefab_Water);
					water.SetActive(true);
					water.transform.SetParent(BlockHeight);
					water.transform.localPosition = new Vector3(x, y, z);
				}

				// マップ中央にプレイヤー配置
				if (x == MAP_SIZE_X / 2 && z == MAP_SIZE_Z / 2)
				{
					player.transform.position = new Vector3(x, groundY + 1, z);
				}
			}
		}
	}


}


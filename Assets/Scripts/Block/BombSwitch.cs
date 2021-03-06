﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombSwitch : MonoBehaviour {

	public GameObject BombParticle;        // 爆発パーティクル
	public Transform[] BombPoints;         // 爆発地点（空のオブジェクトをexplosionPointsで配置、子はexplosionPoint）
	public float DestroyTime = 1f;
	private void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player")   // プレイヤーが踏むと動作する
		{
			foreach (Transform BombPos in BombPoints)
			{
				GameObject explosion = Instantiate (BombParticle,               // パーティクルオブジェクトの生成
					BombPos.position, transform.rotation) as GameObject;

				Destroy (explosion, DestroyTime);                                        // ５秒後に消す
			}
		}
	}
}

﻿using UnityEngine;
using System.Collections;

// プレイヤを自動追尾するメインカメラ（正面視点）
public class CameraFollow : MonoBehaviour 
{
	public Transform target;
	public float height;
	private Vector3 offset;
    public BattleManager battleManager;

    void Start ()
	{
        // Playerタグの付いたオブジェクトの位置をtargetとして取得
        //target = GameObject.FindGameObjectWithTag ("Player").transform;
		// カメラとターゲット（プレイヤー)の距離を設定
		offset = transform.position - battleManager.Player.transform.position;
        
	}
		
	void LateUpdate ()
	{
        
        // カメラがターゲット（プレイヤー）を見つけてから追いかける（少し遅れて追いかける）
        transform.position = battleManager.Player.transform.position + offset;

	}
}


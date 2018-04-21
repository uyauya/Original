﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemy1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;
	bool dead = false;
	public Vector3 BasicPoint;		// 出現時の座標（地上からの高さを決める）
	public  float angle = 30f;
	private Vector3 targetPos;		// 軸の場所
	public GameObject target;		// 回転するための中心部（軸）

	void Start () {	
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
		//transform.LookAt(target);
	}

	void Update () {
		//Transform target = GameObject.FindWithTag("Player").transform;
		Transform target = GameObject.FindWithTag("TargetPoint").transform;
		targetPos = target.position;
		//Vector3 Pog = this.gameObject.transform.position;
		//gameObject.transform.position = new Vector3(Pog.x , 3.0f, Pog.z);
		//オブジェクト配置場所の前方×2の場所をターゲット（軸）とする
		transform.position = target.transform.position + (target.transform.forward * 2);
		transform.position += transform.forward;
		//ターゲットを中心に（回る中心の座標、軸、速度）で回す
		transform.RotateAround (target.transform.position + (target.transform.forward * 2), Vector3.up, 1);
		//軸と回すキャラクタの高低差の設定
		this.transform.position = new Vector3 (this.transform.position.x, this.BasicPoint.y +1, this.transform.position.z);
		//transform.Rotate(new Vector3(0, Random.Range(0,360), 0),Space.World);	
		//Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		//gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		enemyBasic.timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange) {
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
			Vector3 axis = transform.TransformDirection(Vector3.up);
			transform.RotateAround(targetPos, axis ,angle * Time.deltaTime);
		}
		
		// ターゲット（プレイヤー）との距離が1以内なら
		if (Vector3.Distance (enemyBasic.target.transform.position, transform.position) <= 1) {
			//ターゲットの方を徐々に向く
			// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
			// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
			transform.position += transform.forward * Time.deltaTime * enemyBasic.EnemySpeed;
			//一定間隔でショット
			enemyBasic.shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if (enemyBasic.shotInterval > enemyBasic.shotIntervalMax) {
				Instantiate (enemyBasic.shot, transform.position, transform.rotation);
				enemyBasic.shotInterval = 0;
			}				
		}
	}
}
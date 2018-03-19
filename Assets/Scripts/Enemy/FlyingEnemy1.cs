using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlyingEnemy1 : MonoBehaviour {

	protected EnemyBasic enemyBasic;
	bool dead = false;
	public Vector3 BasicPoint;
	public  float angle = 30f;
	private Vector3 targetPos;
	public GameObject target;

	void Start () {	
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
		BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 1.0f, this.transform.position.z);
		Transform target = GameObject.FindWithTag("Player").transform;
		targetPos = target.position;
		//transform.LookAt(target);

	}

	void Update () {
		//Vector3 Pog = this.gameObject.transform.position;
		//gameObject.transform.position = new Vector3(Pog.x , 3.0f, Pog.z);
		transform.position = target.transform.position + (target.transform.forward * 2);
		transform.position += transform.forward;
		transform.RotateAround (target.transform.position + (target.transform.forward * 4), Vector3.up, 1);
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
				(enemyBasic.target.transform.position - transform.position), Time.deltaTime * 5);
			transform.position += transform.forward * Time.deltaTime * 20;
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
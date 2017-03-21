using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	GameObject target;
	public GameObject shot;
	float shotInterval = 0;
	float shotIntervalMax = 1.0F;			// 攻撃間隔
	public GameObject exprosion;			// 爆発処理
	public float armorPoint;
	public float armorPointMax = 1000F; 	// 敵の体力
	float damage;							// playerに与えるダメージ
	float timer = 0;	
	int enemyLevel = 0;
	Bullet01 b1;
	public void Damaged(float damagedPoint){
		this.armorPoint -= damagedPoint;	// Playerから受けたダメージの設定
	}
	
	void Start () {	
		//ターゲットを取得
		target = GameObject.Find("PlayerTarget");
		// 敵のHPを最大にする
		armorPoint = armorPointMax;
	}

	void Update () {	
		/*
		//敵の攻撃範囲を設定する
		if( Vector3.Distance (target.transform.position, transform.position) <= 30 ){
			
			//ターゲットの方向を向く
			//transform.LookAt(target.transform);
			
			//スムーズにターゲットの方向を向く
			Quaternion targetRotation = Quaternion.LookRotation (target.transform.position - transform.position);
			transform.rotation = Quaternion.Slerp (transform.rotation, targetRotation, Time.deltaTime * 10);
			
			//弾を発射する
			shotInterval += Time.deltaTime;
			// 次の攻撃待ち時間が一定以上になれば
			if(shotInterval > shotIntervalMax){
				// その場から発射して攻撃待ち時間を０に戻す
				Instantiate(shot, transform.position, transform.rotation);
				shotInterval = 0;
			}
		}
		*/
		
		timer += Time.deltaTime;

		//経過時間に応じてレベルを上げる
		if (timer < 5)
			enemyLevel = 1;
		else if(timer < 10)
			enemyLevel = 2;
		else if(timer < 15 )
			enemyLevel = 3;
		else if(timer >= 15 ){
			enemyLevel = 4;
			//レベル４：攻撃間隔が短くなる
			shotIntervalMax = 0.5F;
		}
		
		//レベル２：プレイヤーが一定範囲に近づいたら攻撃
		if(enemyLevel >= 2) {
			// ターゲット（プレイヤー）との距離が30以内なら
			if (Vector3.Distance (target.transform.position, transform.position) <= 30) {
				
				//ターゲットの方を徐々に向く
				// Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
				// Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				     (target.transform.position - transform.position), Time.deltaTime * 5);

				//一定間隔でショット
				shotInterval += Time.deltaTime;
				// 次の攻撃待ち時間が一定以上になれば
				if (shotInterval > shotIntervalMax) {

					Instantiate (shot, transform.position, transform.rotation);
					shotInterval = 0;
				}
			}else{
					
				//レベル３：距離に関係なくプレイヤーに自分から近づく
				if(enemyLevel >= 3){
					transform.rotation = Quaternion.Slerp 
						(transform.rotation, Quaternion.LookRotation (target.transform.position - transform.position), 
						 Time.deltaTime * 5);
					transform.position += transform.forward * Time.deltaTime * 20;
				}
			}
		}
	}
	
	void OnCollisionEnter(Collision collider) {
		//Debug.Log (collider);

		if (collider.gameObject.tag == "Shot") {
			damage = collider.gameObject.GetComponent<Bullet01> ().damage;
		} else if (collider.gameObject.tag == "Shot2") {
			damage = collider.gameObject.GetComponent<Bullet02> ().damage;
		} else if (collider.gameObject.tag == "Shot3") {
			damage = collider.gameObject.GetComponent<Bullet03> ().damage;
		} else if (collider.gameObject.tag == "Shot5") {
			damage = collider.gameObject.GetComponent<Bullet05> ().damage;
		}
			//Debug.Log ("ダメージ");
			//プレイヤーの弾のダメージを引く
			armorPoint -= damage;
			//Debug.Log ("受け取った");

			//体力が0以下になったら消滅する
			if (armorPoint <= 0){
				Destroy (gameObject);
				// その場で爆発
				Instantiate(exprosion, transform.position, transform.rotation);

				//リザルト用のスコアを加算する
				BattleManager.score ++;
			}
	
	}
}
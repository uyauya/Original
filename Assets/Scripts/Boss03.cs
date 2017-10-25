using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03 : MonoBehaviour {

	private Animator animator;					// アニメーションする時はAnimator取得
	GameObject target;
	public GameObject Boss02shot;				// 弾
	public float ShotInterval;					// ショット間隔
	float shotInterval = 0;						// ショット間隔計測開始
	public float shotIntervalMax = 1.0F;		// ショット間隔計測終了（
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 10000F; 
	float damage;								// playerに与えるダメージ
	float timer = 0;	
	int enemyLevel = 0;
	Bullet01 b1;
	private ModelColorChange modelColorChange;	// 被弾時点滅用（ModelColorChange参照） 
	private bool isInvincible;					// 被弾時の無敵判定
	public GameObject Boss02muzzle;				// ショットの発射口
	public void Damaged(float damagedPoint){	// Playerから受けたダメージの設定
		this.armorPoint -= damagedPoint;		
	}
	public int TargetPosition;
	public float TargetSpeed;
	public float MoveSpeed;						// 移動速度


	void Start () {
		animator = GetComponent< Animator >();		// 《Animator》コンポーネントの取得
		target = GameObject.Find("PlayerTarget");	//ターゲットを取得
		armorPoint = armorPointMax;
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		if (Vector3.Distance (target.transform.position, transform.position) > TargetPosition) {
			return;
		}
	}


	void Update () {
		Vector3 Pog = this.gameObject.transform.position;
		gameObject.transform.position = new Vector3(Pog.x , 0.01f, Pog.z);
		Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
		gameObject.transform.eulerAngles = new Vector3(1 ,Ros.y, 1);
		timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (target.transform.position, transform.position) <= TargetPosition) {

			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				(target.transform.position - transform.position), Time.deltaTime * TargetSpeed);
			transform.position += transform.forward * Time.deltaTime * MoveSpeed;	
		}
		//一定間隔でショット
		shotInterval += Time.deltaTime;

		if (shotInterval > shotIntervalMax) {

			GameObject bossshot = GameObject.Instantiate (Boss02shot, Boss02muzzle.transform.position,Quaternion.identity)as GameObject;
			shotInterval = ShotInterval;
		}
	}


	void OnCollisionEnter(Collision collider) {

		if (collider.gameObject.tag == "Shot") {
			damage = collider.gameObject.GetComponent<Bullet01> ().damage;
		} else if (collider.gameObject.tag == "Shot2") {
			damage = collider.gameObject.GetComponent<Bullet02> ().damage;
		} else if (collider.gameObject.tag == "Shot3") {
			damage = collider.gameObject.GetComponent<Bullet03> ().damage;
		} else if (collider.gameObject.tag == "Shot5") {
			damage = collider.gameObject.GetComponent<Bullet05> ().damage;
		}

		armorPoint -= damage;
		animator.SetTrigger ("Damage");
		StartCoroutine ("DamageCoroutine");

		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			animator.SetBool("dead" , true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject);

			//リザルト用のスコアを加算する
			//BattleManager.score ++;
		}		
	}

	IEnumerator DamageCoroutine () {
		//レイヤーをPlayerDamageに変更
		gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		//while文を10回ループ
		int count = 10;
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", transform.position - (transform.forward * 0f),
			"time", 0.5f, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			//透明にする
			//Debug.Log ("色変える");
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.05秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.05秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		isInvincible = false;
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Boss");
		//iTweenのアニメーション

	}
}

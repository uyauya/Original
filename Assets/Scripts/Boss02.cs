using UnityEngine;
using System.Collections;

public class Boss02 : MonoBehaviour {

	private Animator animator;		// 《Animator》コンポ
	GameObject target;
	public GameObject Boss02shot;
	float shotInterval = 0;
	float shotIntervalMax = 1.0F;	
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 10000F; 
	float damage;							// playerに与えるダメージ
	float timer = 0;	
	int enemyLevel = 0;
	Bullet01 b1;
	private ModelColorChange modelColorChange;
	private bool isInvincible;
	public void Damaged(float damagedPoint){
		this.armorPoint -= damagedPoint;	// Playerから受けたダメージの設定
	}
	
	
	void Start () {
		animator = GetComponent< Animator >();		// 《Animator》コンポーネントの取得
		target = GameObject.Find("PlayerTarget");	//ターゲットを取得
		armorPoint = armorPointMax;
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		if (Vector3.Distance (target.transform.position, transform.position) >= 21) {
			return;
		}
	}
	
	
	void Update () {
		timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		 if (Vector3.Distance (target.transform.position, transform.position) <= 20) {
			
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
			                                       (target.transform.position - transform.position), Time.deltaTime * 5);
			transform.position += transform.forward * Time.deltaTime * 5;
			//Debug.Log("狙い");
		}
		//一定間隔でショット
		shotInterval += Time.deltaTime;
			//Debug.Log("ショット準備");
		if (shotInterval > shotIntervalMax) {
			Instantiate (Boss02shot, transform.position, transform.rotation);
			shotInterval = 0;
			//Debug.Log("ショット出ないよ");
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
		
		armorPoint -= damage;
		animator.SetTrigger ("Damage");
		StartCoroutine ("DamageCoroutine");

		//Debug.Log ("受け取った");
		
		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			animator.SetBool("dead" , true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject);
			
			//リザルト用のスコアを加算する
			BattleManager.score ++;
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
			//Debug.Log ("戻す");
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.05秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		isInvincible = false;
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Boss02");
		//iTweenのアニメーション

	}
}

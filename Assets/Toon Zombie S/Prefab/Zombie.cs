using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {
	private Animator animator;		// 《Animator》コンポーネント用の変数
	GameObject target;
	public float armorPoint;
	public float armorPointMax = 100F; 
	float timer = 0;
	float damage;							// playerに与えるダメージ
	Bullet01 b1;
	public void Damaged(float damagedPoint){
		this.armorPoint -= damagedPoint;	// Playerから受けたダメージの設定
	}
	

	void Start () {
		animator = GetComponent< Animator >();		// 《Animator》コンポーネントの取得
		target = GameObject.Find("PlayerTarget");	//ターゲットを取得
		armorPoint = armorPointMax;
	}
	

	void Update () {
		timer += Time.deltaTime;
		//敵の攻撃範囲を設定する
		if (Vector3.Distance (target.transform.position, transform.position) <= 30) {
				
			//ターゲットの方を徐々に向く
			transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation 
				                                       (target.transform.position - transform.position), Time.deltaTime * 5);
			transform.position += transform.forward * Time.deltaTime * 1;	
		}
	}
	

	void OnCollisionEnter(Collision collider) {
		//Debug.Log (collider);
		
		if (collider.gameObject.tag == "Shot") {
			damage = collider.gameObject.GetComponent<Bullet01> ().damage;
		} else {
			if (collider.gameObject.tag == "Shot2") {
				damage = collider.gameObject.GetComponent<Bullet02> ().damage;
			}
		}

		armorPoint -= damage;
		//Debug.Log(armorPoint);
		
		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			animator.SetBool("dead" , true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject);
			
			//リザルト用のスコアを加算する
			BattleManager.score ++;
		}
		
	}
}


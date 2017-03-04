using UnityEngine;
using System.Collections;

public class Boss02 : MonoBehaviour {

	private Animator animator;		// 《Animator》コンポ
	GameObject target;
	public GameObject shot;
	float shotInterval = 0;
	float shotIntervalMax = 1.0F;	
	public GameObject exprosion;	
	public float armorPoint;
	public float armorPointMax = 10000F; 
	float damage;							// playerに与えるダメージ
	float timer = 0;	
	int enemyLevel = 0;
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
			transform.position += transform.forward * Time.deltaTime * 5;	
		}
		//一定間隔でショット
		shotInterval += Time.deltaTime;
		
		if (shotInterval > shotIntervalMax) {
			Instantiate (shot, transform.position, transform.rotation);
			shotInterval = 0;
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
		Debug.Log(armorPoint);
		Debug.Log(damage);
		//Debug.Log ("受け取った");
		
		//体力が0以下になったら消滅する
		if (armorPoint <= 0){
			animator.SetBool("dead" , true);		// 《Animator》の変数deadを true に変更.
			Destroy (gameObject);
			
			//リザルト用のスコアを加算する
			BattleManager.score ++;
		}
		
	}
}

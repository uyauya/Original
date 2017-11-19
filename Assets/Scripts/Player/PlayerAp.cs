using UnityEngine;
using System.Collections;
using UnityEditor; 
using UnityEngine.UI;

// TODO ※プレイヤーのアニメーション処理
public class PlayerAp : MonoBehaviour {

	public static int armorPoint;		// プレイヤー体力
	public int armorPointMax;			// プレイヤー体力最大値
	//int damage = 100;					// 敵から受けるダメージ
	public int enemyAttack;
	public Text armorText;
	int displayArmorPoint;				
	public Color myGreen;				// RGBA(000,240,000,255) ※Aは透明度
	public Color myWhite;				// RGBA(255,255,255,255)
	public Color myYellow;				// RGBA(255,206,000,255)
	public Color myRed;					// RGBA(219,000,000,255)
	public Image gaugeImage;
	private ModelColorChange modelColorChange;
	private bool isInvincible;			// 無敵処理（ダメージ受けた際に使用）
	public float InvincibleTime;		// 無敵時間
	private Animator animator;			// Animator（PlayerMotion)取得
	public float KnockBackRange;		// ノックバック距離（ダメージ受けた際に使用）
	public int PlayerNo;				// プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）
	public Transform muzzle;			// ショット発射口位置をTransformで位置取り
	public Transform EffectPoint;		// 回復等エフェクト発生元の位置取り
	public GameObject DamagePrefab;		// ダメージエフェクト格納場所
	public GameObject DamageObject;
	public GameObject HpHealPrefab;		// アーマーポイント回復エフェクト格納場所
	public GameObject HpHealObject;
	public GameObject boddy_summer;
	public int attackPoint;
	public int BigAttack;
	public bool isBig;

	/*[CustomEditor(typeof(PlayerAp))]
	public class PlayerApEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
		PlayerAp PL = target as PlayerAp;
			PL.armorPointMax = EditorGUILayout.IntField( "最大HP", PL.armorPointMax);
			PL.InvincibleTime = EditorGUILayout.FloatField( "無敵時間", PL.InvincibleTime);
			PL.KnockBackRange = EditorGUILayout.FloatField( "ノックバック距離", PL.KnockBackRange);
		}
	}*/

	void Start () {	
		armorPoint = armorPointMax;
		displayArmorPoint = armorPoint;
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		animator = GetComponent<Animator> ();
		gaugeImage = GameObject.Find ("ApGauge").GetComponent<Image> ();
		armorText = GameObject.Find ("TextAp").GetComponent<Text> ();
		// Enemyタグの付いたオブジェクトのEnemyBasicの敵の攻撃値(EnemyAttack)をenemyAttackと呼ぶ
		//enemyAttack= GameObject.FindWithTag("Enemy").GetComponent<EnemyBasic>().EnemyAttack;
		boddy_summer = GameObject.Find("_body_summer");
		attackPoint = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().AttackPoint;
		isBig = false;
	}


	void Update () {
		
		//現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
		if (displayArmorPoint != armorPoint) 
			displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorPoint, 0.1F);
		
		//現在の体力と最大体力をUI Textに表示する
		armorText.text = string.Format("{0:0000} / {1:0000}", displayArmorPoint, armorPointMax);
		//残り体力の割合により文字の色を変える
		float percentageArmorpoint = (float)displayArmorPoint / armorPointMax;
		// myWhiteなどにして色を任意で指定できるようにする
		// armorTesが数値、gougeImageがゲージの色
		// ユーザーインターフェース（UI)の色を変える場合、画像の色は白一色にする
		//　白以外の場合、指定した色と混ざる為、指定した色にならなくなる
		//if( percentageArmorpoint > 0.5F){
		if( armorPoint > 4000){
			armorText.color = myGreen;
			gaugeImage.color = myGreen;
			//gaugeImage.color = new Color(0.25F, 0.7F, 0.6F);
		//}else if( percentageArmorpoint > 0.3F){
		}else if( armorPoint > 2900){
			armorText.color = myWhite;
			gaugeImage.color = myWhite;
		}else if( armorPoint > 2000){
			armorText.color = myYellow;
			gaugeImage.color = myYellow;
		}else{
			armorText.color = myRed;
			gaugeImage.color = myRed;
		}
		
		//ゲージの長さを体力の割合に合わせて伸縮させる
		gaugeImage.transform.localScale = new Vector3(percentageArmorpoint, 1, 1);
	}

	private void OnCollisionEnter(Collision collider) {
		
		//ShotEnemyの弾と衝突したらダメージ
		//ぶつかった時にコルーチンを実行（下記IEnumerator参照）
		if (collider.gameObject.tag == "ShotEnemy") {
			
			//armorPoint -= damage;
			armorPoint -= enemyAttack;
			armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
			DamageObject = Instantiate (DamagePrefab, EffectPoint.position, Quaternion.identity);
			DamageObject.transform.SetParent (EffectPoint);
			animator.SetTrigger ("Damage");
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(36,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(37,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(38,gameObject);
			}
			StartCoroutine ("DamageCoroutine");
		//Enemyと接触したらダメージ
		//ぶつかった時にコルーチンを実行（下記IEnumerator参照）
		} else if (collider.gameObject.tag == "Enemy") {
			// Enemyタグの付いたオブジェクトのEnemyBasicの敵の攻撃値(EnemyAttack)をenemyAttackと呼ぶ
			//enemyAttack= GameObject.FindWithTag("Enemy").GetComponent<EnemyBasic>().EnemyAttack;
			enemyAttack= collider.gameObject.GetComponent<EnemyBasic>().EnemyAttack;
			if (isBig == true) {
				armorPoint -= 0;
			} else {
				//armorPoint -= damage;
				armorPoint -= enemyAttack;
				armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
				DamageObject = Instantiate (DamagePrefab, EffectPoint.position, Quaternion.identity);
				DamageObject.transform.SetParent (EffectPoint);
				animator.SetTrigger ("Damage");
				if (PlayerNo == 0) {
					SoundManager.Instance.Play (21, gameObject);
				}
				if (PlayerNo == 1) {
					SoundManager.Instance.Play (22, gameObject);
				}
				if (PlayerNo == 2) {
					SoundManager.Instance.Play (23, gameObject);
				}
				StartCoroutine ("DamageCoroutine");
			}
		}

		//Itemタグをつけたもの（RedSphere）を取ったら体力1000回復
		else if (collider.gameObject.tag == "Item") {
			HpHealObject = Instantiate (HpHealPrefab, EffectPoint.position, Quaternion.identity);
			HpHealObject.transform.SetParent (EffectPoint);
			animator.SetTrigger ("ItemGet");
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(18,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(19,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(20,gameObject);
			}
			armorPoint += 1000;
			// 体力上限以上には回復しない。
			armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
		}

		//Itemタグをつけたもの（YellowSphere）を取ったら無敵＆巨大化
		else if (collider.gameObject.tag == "Item4") {
			HpHealObject = Instantiate (HpHealPrefab, EffectPoint.position, Quaternion.identity);
			HpHealObject.transform.SetParent (EffectPoint);
			animator.SetTrigger ("ItemGet");
			if (PlayerNo == 0) {
				SoundManager.Instance.Play(18,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play(19,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play(20,gameObject);
			}
			StartCoroutine ("BigCoroutine");
		}
	}

	// Itweenを使ってコルーチン作成（Itweenインストール必要あり）
	IEnumerator DamageCoroutine ()
	{
		//レイヤーをPlayerDamageに変更
		//gameObject.layer = LayerMask.NameToLayer("PlayerDamage");
		//while文を10回ループ
		int count = 10;
		iTween.MoveTo(gameObject, iTween.Hash(
			"position", transform.position - (transform.forward * KnockBackRange),
			"time", InvincibleTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			//透明にする
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		isInvincible = false;
		//レイヤーをPlayerに戻す
		//gameObject.layer = LayerMask.NameToLayer("Player");
		//iTweenのアニメーション
	}

	/*IEnumerator BigCoroutine ()
	{
		boddy_summer = GameObject.Find("_body_summer");
		// BigPlayerにタグ変更
		gameObject.tag = "BigPlayer";
		// 巨大化
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 3, "y", 3, "z", 3, "time", 3f));
		// 無敵化
		isInvincible = true;
		float duration = 5.0f; //無敵時間
		float blinkedTime = duration;
		float blinkDuration = 0.5f; //点滅の切り替わるまでの期間
		　while (blinkedTime > 0.0f) {
			yield return new WaitForSeconds (blinkDuration);
			blinkedTime -= blinkDuration;
			boddy_summer.GetComponent <SkinnedMeshRenderer>().enabled = !boddy_summer.GetComponent <SkinnedMeshRenderer>().enabled;
		}
		boddy_summer.GetComponent <SkinnedMeshRenderer>().enabled = true;
		// 元のサイズに縮小
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 1, "y", 1, "z", 1, "time", 3f));
		// Playerタグに戻す
		gameObject.tag = "Player";
		// 無敵解除
		isInvincible = false;
	}*/

	IEnumerator BigCoroutine ()
	{
		//gameObject.tag = "BigPlayer";
		// 巨大化
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 3, "y", 3, "z", 3, "time", 3f,"easetype", iTween.EaseType.linear));
		BigAttack = 10000;
		isBig = true;

		Debug.Log (BigAttack);
		int count = 1000;
		iTween.MoveTo(gameObject, iTween.Hash(
			"time", InvincibleTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			//透明にする
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		// 元のサイズに縮小
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 1, "y", 1, "z", 1, "time", 3f));
		isBig = false;
		BigAttack = 0;
		// Playerタグに戻す
		//gameObject.tag = "Player";
		// 無敵解除
		isInvincible = false;
	}
}

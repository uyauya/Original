
using UnityEngine;
using System.Collections;
using UnityEditor; 
using UnityEngine.UI;

// TODO ※プレイヤーのアニメーション処理
public class PlayerAp : MonoBehaviour {

	public static float armorPoint;		// プレイヤー体力
	public int enemyAttack;
	public Text armorText;
	float displayArmorPoint;				
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
	public float force;
	public float maxForce;
	public int BigAttack;
	public bool isBig;
	public float HealApPoint = 1000;

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
		armorPoint = DataManager.ArmorPointMax;
		displayArmorPoint = armorPoint;
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		animator = GetComponent<Animator> ();
		gaugeImage = GameObject.Find ("ApGauge").GetComponent<Image> ();
		armorText = GameObject.Find ("TextAp").GetComponent<Text> ();
		boddy_summer = GameObject.Find("_body_summer");
		attackPoint = DataManager.AttackPoint;
		isBig = false;
		gameObject.layer = LayerMask.NameToLayer("Player");
	}


	void Update () {
		
		//現在の体力と表示用体力が異なっていれば、現在の体力になるまで加減算する
		if (displayArmorPoint != armorPoint) 
			displayArmorPoint = (int)Mathf.Lerp(displayArmorPoint, armorPoint, 0.1F);
		
		//現在の体力と最大体力をUI Textに表示する
		//armorText.text = string.Format("{0:0000} / {1:0000}", displayArmorPoint, DataManager.ArmorPointMax);
		armorText.text = string.Format("{0:0000} / {1:0000}", armorPoint, DataManager.ArmorPointMax);
		//残り体力の割合により文字の色を変える
		float percentageArmorpoint = (float)displayArmorPoint / DataManager.ArmorPointMax;
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
		force = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().Force;
		maxForce = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().MaxForce;
		//EnemyやEnemyの弾と衝突したらダメージ
		//ぶつかった時にコルーチンを実行（下記IEnumerator参照）
		if (collider.gameObject.tag == "ShotEnemy"|| collider.gameObject.tag == "Enemy") {
			if (collider.gameObject.tag == "ShotEnemy") {
				enemyAttack = 100;
			}
			if (collider.gameObject.tag == "Enemy") {
				enemyAttack = collider.gameObject.GetComponent<EnemyBasic>().EnemyAttack;
			}
			// 巨大化していたらダメージなし
			if (isBig == true) {
				armorPoint -= 0;
			} else {
				//巨大化していなかったら（通常なら）
				armorPoint -= enemyAttack;
				armorPoint = Mathf.Clamp (armorPoint, 0, DataManager.ArmorPointMax);
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
				StartCoroutine ("EnemyDamageCoroutine");
			}
		
		//速度最大で壁と接触したらダメージ
		//ぶつかった時にコルーチンを実行（下記IEnumerator参照）
		} else if (collider.gameObject.tag == "Wall") {
			if (isBig == true) {
				armorPoint -= 0;
			} else {
				if (force >= maxForce) {
					//Debug.Log (force);
					//カメラに付けているShakeCameraのShakeを呼び出す（激突時の衝撃）
					Camera.main.gameObject.GetComponent<ShakeCamera>().Shake();
					//Debug.Log ("激突");
					armorPoint -= 100;
					DamageObject = Instantiate (DamagePrefab, EffectPoint.position, Quaternion.identity);
					DamageObject.transform.SetParent (EffectPoint);
					animator.SetTrigger ("Damage");
					if (PlayerNo == 0) {
						SoundManager.Instance.Play(24,gameObject);	
						SoundManager.Instance.PlayDelayed (27, 0.2f, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play(25,gameObject);	
						SoundManager.Instance.PlayDelayed (28, 0.2f, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play(26,gameObject);	
						SoundManager.Instance.PlayDelayed (29, 0.2f, gameObject);
					}
					StartCoroutine ("WallDamageCoroutine");
				}
			}

		//Itemタグをつけたもの（RedSphere）を取ったら体力1000回復
		} else if (collider.gameObject.tag == "Item") {
			// 既にarmorPointがMaxだったら何もしない
			if (armorPoint == DataManager.ArmorPointMax) return;
			// armorPointがMaxになったら声出し
			if (armorPoint  < DataManager.ArmorPointMax) {
				if (armorPoint + HealApPoint < DataManager.ArmorPointMax) {
					if (PlayerNo == 0) {
						SoundManager.Instance.Play (18, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play (19, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play (20, gameObject);
					}
				} else if (armorPoint + HealApPoint >= DataManager.ArmorPointMax) {
					if (PlayerNo == 0) {
						SoundManager.Instance.PlayDelayed (39, 1.1f, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.PlayDelayed (40, 1.1f, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.PlayDelayed (41, 1.1f, gameObject);
					}
				}
			}
			// プレイヤオブジェクトにGameObject→CreateEmptyでEffectPointという名前にして追加し
			// 回復エフェクト発生時エフェクト出したい場所に調整する
			HpHealObject = Instantiate (HpHealPrefab, EffectPoint.position, Quaternion.identity);
			HpHealObject.transform.SetParent (EffectPoint);
			// armorPointにHealApPoint数値加算
			armorPoint += HealApPoint;
			// 体力上限以上には回復しない。
			armorPoint = Mathf.Clamp (armorPoint, 0, DataManager.ArmorPointMax);
			Debug.Log (armorPoint);
			//armorPoint = Mathf.Min (armorPoint + HealApPoint, armorPointMax);
			Debug.Log (armorPoint);
			animator.SetTrigger ("ItemGet");
		}

		//Itemタグをつけたもの（YellowSphere）を取ったら無敵＆巨大化
		else if (collider.gameObject.tag == "Item4") {	
			HpHealObject = Instantiate (HpHealPrefab, EffectPoint.position, Quaternion.identity);
			HpHealObject.transform.SetParent (EffectPoint);
			animator.SetTrigger ("ItemGet");
			if (PlayerNo == 0) {
				SoundManager2.Instance.Play(6,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager2.Instance.Play(6,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager2.Instance.Play(6,gameObject);
			}
			// BigCoroutine開始（下記参照）
			StartCoroutine ("BigCoroutine");
		}
	}

	// Itweenを使ってコルーチン作成（Itweenインストール必要あり）
	// 敵接触時の点滅
	IEnumerator EnemyDamageCoroutine ()
	{
		// プレイヤのレイヤーをInvincibleに変更
		// Edit→ProjectSetting→Tags and LayersでInvicibleを追加
		// Edit→ProjectSetting→Physicsで衝突させたくない対象と交差している所の✔を外す
		// ここではEnemyと衝突させたくない（すり抜ける）為、Enemeyのレイヤーも追加
		// EnemeyとPlayerの交差してる✔を外す（プレイヤのLayerをPlayer、EnemyのLayerをEnemyに設定しておく）
		gameObject.layer = LayerMask.NameToLayer("Invincible");
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
		gameObject.layer = LayerMask.NameToLayer("Player");
		//iTweenのアニメーション
	}

	// 壁接触時の点滅
	IEnumerator WallDamageCoroutine ()
	{
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
	}

	IEnumerator BigCoroutine ()
	{
		// 巨大化
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 3, "y", 3, "z", 3, "time", 3f,"easetype", iTween.EaseType.linear));
		// BigAttack数値をプレイヤ自体に追加（敵一撃死用）
		BigAttack = 10000;
		isBig = true;

		// 巨大化継続時間を設定
		int count = 100;
		iTween.MoveTo(gameObject, iTween.Hash(
			"time", InvincibleTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		isInvincible = true;
		while (count > 0){
			//点滅時の色を設定（ModelColorChangeスクリプト参照）
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (255,255,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		// 元のサイズに縮小
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 1, "y", 1, "z", 1, "time", 3f));
		isBig = false;
		BigAttack = 0;
		// isInvincible解除
		isInvincible = false;
	}
}

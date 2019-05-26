﻿
using UnityEngine;
using System.Collections;
using UnityEditor; 
using UnityEngine.UI;

// TODO ※プレイヤーのアニメーション処理
public class PlayerAp : MonoBehaviour {

    Rigidbody rigidbody;
    public static float armorPoint;			// プレイヤー体力
	public static int enemyAttack;					// 敵の攻撃値
	public float poisonAttack;				// 毒
	public Text armorText;
	float displayArmorPoint;				// 画面表示用HPゲージ
	//デフォルトカラー設定inspector→ColorでRGBAのそれぞれの値を255で割った数値を入れる
	//RGBA ※Aは透明度(0に近くなるほど透明化)
	public Color MyGreen  = new Color (0.48f,0.97f,0.08f,1);
	public Color MyWhite  		= new Color (1,1,1,1);
	public Color MyYellow = new Color (0.81f,0.99f,0,1);
	public Color MyRed	  = new Color (1,0.16f,0.16f,1);
	public Color DamageColor 	= new Color (0.96f,0.06f,0.24f,0.98f);  //ダメージ点滅色
	public Color InvisibleColor = new Color (1,1,1,0);  //無敵状態時色
	public Color PoisonColor 	= new Color (1,0.1f,0.91f,0.47f);  //毒ダメージ色
    public Image gaugeImage;
	private ModelColorChange modelColorChange;		
	public float FlashTime =10.0f;			// 点滅時間
	public static Animator Panimator;				// Animator（PlayerMotion)取得
	public float KnockBackRange = 2.0f;		// ノックバック距離（ダメージ受けた際に使用）
	public int PlayerNo;					// プレイヤーNo取得用(0でこはく、1でゆうこ、2でみさき）
	public Transform muzzle;				// ショット発射口位置をTransformで位置取り
	public Transform EffectPoint;			// 回復等エフェクト発生元の位置取り
	public GameObject DamagePrefab;			// ダメージエフェクト格納場所
	public GameObject DamageObject;			// 被ダメージ時エフェクト発生用
	public GameObject HpHealPrefab;			// アーマーポイント回復エフェクト格納場所
	public GameObject HpHealObject;			// 回復時エフェクト発生用
	public GameObject boddy_summer;
	public int attackPoint;					// 攻撃値
	public float force;						// 移動速度
	public float maxForce;					// 移動速度最大
	public int BigAttack = 10000;			// 巨大化した時の攻撃値
	public bool isBig;						// 巨大化しているかどうか
	public float HealApPoint = 1000;		// アイテム取得時の回復量
	public int WallDamage = 100;			// 壁激突時のダメージ
    public float PDamagePoint = 100;
    public float FDamagePoint = 10;
    public float HealPoint = 10;

    /*[CustomEditor(typeof(PlayerAp))]
	public class PlayerApEditor : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
		PlayerAp PL = target as PlayerAp;
			PL.FlashTime	  = EditorGUILayout.FloatField( "点滅時間", PL.FlashTime);
			PL.InvincibleTime = EditorGUILayout.FloatField( "無敵時間", PL.InvincibleTime);
			PL.KnockBackRange = EditorGUILayout.FloatField( "ノックバック距離", PL.KnockBackRange);
			PL.BigAttack	  = EditorGUILayout.FloatField( "巨大化した時の攻撃値");
			PL.HealApPoint	  = EditorGUILayout.FloatField( "アイテム取得時の回復量");
		}
	}*/

    void Start () {
        rigidbody = GetComponent<Rigidbody>();
        armorPoint = DataManager.ArmorPointMax;
		// 画面上のarmorPointとPlayerApのarmorPointを連動させる
		displayArmorPoint = armorPoint;
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		Panimator = GetComponent<Animator> ();
		gaugeImage = GameObject.Find ("ApGauge").GetComponent<Image> ();
		armorText = GameObject.Find ("TextAp").GetComponent<Text> ();
		boddy_summer = GameObject.Find("_body_summer");
		attackPoint = DataManager.AttackPoint;
		isBig = false;
		gameObject.layer = LayerMask.NameToLayer("Player");
		Transform muzzle = GameObject.FindWithTag ("Player").transform.Find("muzzle");
		Transform EffectPoint = GameObject.FindWithTag ("Player").transform.Find("EffectPoint");
		GameObject DamagePrefab = GameObject.Find("MagicAttack_7");
		GameObject HpHealPrefab = GameObject.Find("Aura2");
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
		if( armorPoint > 4000){				// armorPointが4000未満でゲージ色変化
			armorText.color = MyGreen;
			gaugeImage.color = MyGreen;
		}else if( armorPoint > 2900){
			armorText.color = MyWhite;
			gaugeImage.color = MyWhite;
		}else if( armorPoint > 2000){
			armorText.color = MyYellow;
			gaugeImage.color = MyYellow;
		}else{
			armorText.color = MyRed;
			gaugeImage.color = MyRed;
		}
		
		//ゲージの長さを体力の割合に合わせて伸縮させる
		gaugeImage.transform.localScale = new Vector3(percentageArmorpoint, 1, 1);
	}

	//衝突判定
	private void OnCollisionEnter(Collision collider) {
		//Debug.Log (collider.gameObject.name);
		//プレイヤの速度判定（壁衝突時に使う）
		force = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().Force;
		maxForce = GameObject.FindWithTag ("Player").GetComponent<PlayerController> ().MaxForce;
		//Enmey(敵)、もしくはShotEnemy(敵の弾)のタグが付いたものに衝突したら
		if (collider.gameObject.tag == "ShotEnemy" || collider.gameObject.tag == "Enemy") {
			//ShotEnemyタグ付きと接触し、
			if (collider.gameObject.tag == "ShotEnemy" && collider.gameObject.GetComponent<EnemyBasic> () != null) {
				enemyAttack = collider.gameObject.GetComponent<EnemyBasic> ().EnemyAttack;
			}
			if (collider.gameObject.tag == "ShotEnemy" && collider.gameObject.GetComponent<BossBasic>() != null) {
				enemyAttack = collider.gameObject.GetComponent<BossBasic> ().EnemyAttack;
			}
			if (collider.gameObject.tag == "Enemy" && collider.gameObject.GetComponent<EnemyBasic>() != null) {
				enemyAttack = collider.gameObject.GetComponent<EnemyBasic> ().EnemyAttack;
			} 	
			if (collider.gameObject.tag == "Enemy" && collider.gameObject.GetComponent<BossBasic>() != null) {
				enemyAttack = collider.gameObject.GetComponent<BossBasic> ().EnemyAttack;
			} 	

		// 巨大化もしくはダッシュしていたらダメージなし(armorPoint差し引きを0にする)
		if (isBig == true) {
			armorPoint -= 0;
		} else if (FullDash.isDash == true){
			StartCoroutine ("DashCoroutine");	
		} else {
			//巨大化していなかったら（通常なら）
			armorPoint -= enemyAttack;	// enemyAttack値差し引く（ダメージ）
			armorPoint = Mathf.Clamp (armorPoint, 0, DataManager.ArmorPointMax);
			//EffectPointをセットした場所にDamagePrefabに格納しているDamageObjectを発生
			DamageObject = Instantiate (DamagePrefab, EffectPoint.position, Quaternion.identity);
			//SetParentにしてプレイヤが動いてもDamageObjectがプレイヤーに追随するようにする
			DamageObject.transform.SetParent (EffectPoint);
			//アニメーターをDamageに切り替え
			Panimator.SetTrigger ("Damage");
			if ((PlayerNo == 0)|| (PlayerNo == 3)) {
				SoundManager.Instance.Play (21, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play (22, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play (23, gameObject);
			}
                //コルーチン処理（下記参照）
                //rigidbody.AddForce(transform.forward * -5f);
                StartCoroutine ("EnemyDamageCoroutine");
                //Debug.Log("ダメージ");
			}
		}
		//速度最大で壁と接触したらダメージ
		//ぶつかった時にコルーチンを実行（下記IEnumerator参照）
		if (collider.gameObject.tag == "Wall") {
			if (isBig == true) {
				armorPoint -= 0;
			} else {
                //プレイヤ速度がmaxForce値以上ならダメージ
                if (force >= maxForce) 
                {
                    //カメラに付けているShakeCameraのShakeを呼び出す（激突時の衝撃）
                    Camera.main.gameObject.GetComponent<ShakeCamera> ().Shake ();
					//Debug.Log ("激突");
					armorPoint -= WallDamage;
					DamageObject = Instantiate (DamagePrefab, EffectPoint.position, Quaternion.identity);
					DamageObject.transform.SetParent (EffectPoint);
					Panimator.SetTrigger ("Damage");
                    if ((PlayerNo == 0) || (PlayerNo == 3))
                    {
                        SoundManager.Instance.Play (24, gameObject);	
						SoundManager.Instance.PlayDelayed (27, 0.2f, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play (25, gameObject);	
						SoundManager.Instance.PlayDelayed (28, 0.2f, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play (26, gameObject);	
						SoundManager.Instance.PlayDelayed (29, 0.2f, gameObject);
					}
					//コルーチン処理（下記参照）
					StartCoroutine ("WallDamageCoroutine");
				}
			}
		}

		//Itemタグをつけたもの（RedSphere）を取ったら体力1000回復
		if (collider.gameObject.tag == "Item") {
			// 既にarmorPointがMaxだったら何もしない
			if (armorPoint == DataManager.ArmorPointMax) return;
			// armorPointがMaxになったら声出し
			if (armorPoint  < DataManager.ArmorPointMax) {
				if (armorPoint + HealApPoint < DataManager.ArmorPointMax) {
                    if ((PlayerNo == 0) || (PlayerNo == 3))
                    {
                        SoundManager.Instance.Play (18, gameObject);
					}
					if (PlayerNo == 1) {
						SoundManager.Instance.Play (19, gameObject);
					}
					if (PlayerNo == 2) {
						SoundManager.Instance.Play (20, gameObject);
					}
				} else if (armorPoint + HealApPoint >= DataManager.ArmorPointMax) {
                    if ((PlayerNo == 0) || (PlayerNo == 3))
                    {
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
			//Debug.Log (armorPoint);
			Panimator.SetTrigger ("ItemGet");
		}

		//Itemタグをつけたもの（YellowSphere）を取ったら無敵＆巨大化
		if (collider.gameObject.tag == "Item4") {	
			HpHealObject = Instantiate (HpHealPrefab, EffectPoint.position, Quaternion.identity);
			HpHealObject.transform.SetParent (EffectPoint);
			Panimator.SetTrigger ("ItemGet");
			if ((PlayerNo == 0)|| (PlayerNo == 3)) {
				SoundManager2.Instance.Play(6,gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager2.Instance.Play(6,gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager2.Instance.Play(6,gameObject);
			}
			// コルーチン処理（下記参照）
			StartCoroutine ("BigCoroutine");
		}
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Fire")
        {
            if (armorPoint > 10)
            {
                //Debug.Log("火傷");
                armorPoint -= FDamagePoint;
                StartCoroutine("FireDamageCoroutine");
            }
            if (armorPoint <= 10)
            {
                return;
            }
        }
        if (other.tag == "Heal")
        {
            if (armorPoint <= DataManager.ArmorPointMax)
            {
                //Debug.Log("回復");
                armorPoint += HealPoint;
            }
            if (armorPoint >= DataManager.ArmorPointMax)
            {
                armorPoint = DataManager.ArmorPointMax;
            }
        }
    }

	private void OnTriggerEnter(Collider other)
	{
        if (other.tag == "Poison")
        {
            //Debug.Log("毒");
            if (armorPoint > 10)
            { 
                armorPoint -= PDamagePoint;
                StartCoroutine("PoisonDamageCoroutine");
            }
            if (armorPoint <= 10)
            {
                return;
            }
        }
    }

    // Itweenを使ってコルーチン作成（Itweenインストール必要あり）
    // 敵接触時の点滅（オブジェクトの色をStandardなどにしておかないと点滅しない場合がある）
    IEnumerator EnemyDamageCoroutine ()
	{
		// プレイヤのレイヤーをInvincibleに変更
		// Edit→ProjectSetting→Tags and LayersでInvicibleを追加
		// Edit→ProjectSetting→Physicsで衝突させたくない対象と交差している所の✔を外す
		// ここではEnemyと衝突させたくない（すり抜ける）為、Enemeyのレイヤーも追加
		// EnemeyとPlayerの交差してる✔を外す（プレイヤのLayerをPlayer、EnemyのLayerをEnemyに設定しておく）
		gameObject.layer = LayerMask.NameToLayer("Invincible");
		//while文を10回ループ
		int count = 4;
        //iTween.MoveTo(gameObject, iTween.Hash("z", -0.2f));
        iTween.MoveTo(gameObject, iTween.Hash(
            //KnockBackRange値だけ後に吹っ飛ぶ
            "position", transform.position - (transform.forward * KnockBackRange),
            //"position", transform.position - (transform.forward * 5f),
            "time", FlashTime, // 点滅時間（秒）
            "easetype", iTween.EaseType.linear
		));
        //Debug.Log("後退");
        while (count > 0){
			//透明にする
			modelColorChange.ColorChange(DamageColor);
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		//レイヤーをPlayerに戻す
		gameObject.layer = LayerMask.NameToLayer("Player");
	}

	// 壁接触時の点滅
	IEnumerator WallDamageCoroutine ()
	{
		//while文を10回ループ
		int count = 4;
        //rigidbody.transform.position -= transform.forward * 2;
        iTween.MoveTo(gameObject, iTween.Hash(
            "position", transform.position - (transform.forward * KnockBackRange),
            "time", FlashTime, // 点滅時間（秒）
			"easetype", iTween.EaseType.linear
		));
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
	}

	// 毒床触時の点滅
	IEnumerator PoisonDamageCoroutine ()
	{
        gameObject.layer = LayerMask.NameToLayer("Invincible");
        //while文を10回ループ
        int count = 4;
		iTween.MoveTo(gameObject, iTween.Hash(
			//"position", transform.position - (transform.forward * KnockBackRange),
			"time", FlashTime, // 点滅時間（秒）
			"easetype", iTween.EaseType.linear
		));
		while (count > 0){
			//透明にする
			modelColorChange.ColorChange(new Color (0.8f,0,0,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (0.8f,1,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
		}
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    // 炎床触時の点滅
    IEnumerator FireDamageCoroutine()
    {
        //while文を10回ループ
        int count = 4;
        iTween.MoveTo(gameObject, iTween.Hash(
            //"position", transform.position - (transform.forward * KnockBackRange),
            "time", FlashTime, // 点滅時間（秒）
            "easetype", iTween.EaseType.linear
        ));
        while (count > 0)
        {
            //透明にする
            modelColorChange.ColorChange(new Color(0.6f, 0, 0, 1));
            //0.1秒待つ
            yield return new WaitForSeconds(0.1f);
            //元に戻す
            modelColorChange.ColorChange(new Color(0.6f, 1, 1, 1));
            //0.1秒待つ
            yield return new WaitForSeconds(0.1f);
            count--;
        }
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
			"time", FlashTime, // 点滅時間（秒）
			"easetype", iTween.EaseType.linear
		));
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
		// 元のサイズに縮小して巨大化時の攻撃を無効にする
		iTween.ScaleTo (gameObject, iTween.Hash ("x", 1, "y", 1, "z", 1, "time", 3f));
		isBig = false;
		BigAttack = 0;
	}

	IEnumerator DashCoroutine ()
	{
		gameObject.layer = LayerMask.NameToLayer("Invincible");
		int count = 10;
		while (count > 0){
			modelColorChange.ColorChange(DamageColor);
			yield return new WaitForSeconds(0.1f);
			modelColorChange.ColorChange(new Color (1,1,1,1));
			yield return new WaitForSeconds(0.1f);
			count--;
		}
		gameObject.layer = LayerMask.NameToLayer("Player");
	}
}

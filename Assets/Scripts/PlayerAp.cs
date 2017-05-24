using UnityEngine;
using System.Collections;

using UnityEngine.UI;

public class PlayerAp : MonoBehaviour {

	public static int armorPoint;
	int armorPointMax = 5000;
	int damage = 100;
	public Text armorText;
	int displayArmorPoint;
	public Color myWhite;
	public Color myYellow;
	public Color myRed;
	public Image gaugeImage;
	private ModelColorChange modelColorChange;
	private bool isInvincible;
	private Animator animator;
	//string inputCommands = "";			//コマンド
	//bool commandEnable = true;			//コマンド判定
	//int recCommandLength = 100;			//コマンド許容時間

	void Start () {	
		armorPoint = armorPointMax;
		displayArmorPoint = armorPoint;
		modelColorChange = gameObject.GetComponent<ModelColorChange>();
		animator = GetComponent<Animator> ();
		gaugeImage = GameObject.Find ("ApGauge").GetComponent<Image> ();
		armorText = GameObject.Find ("TextAp").GetComponent<Text> ();
		//inputCommands = inputCommands.Padforward(100);	// キャラクタが前向きの時
		//StartCoroutine("confirmCommand");					// コマンドコルーチン採用
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
		if( percentageArmorpoint > 0.5F){
			armorText.color = myWhite;
			gaugeImage.color = new Color(0.25F, 0.7F, 0.6F);
		}else if( percentageArmorpoint > 0.3F){
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
		if (collider.gameObject.tag == "ShotEnemy") {
			armorPoint -= damage;
			armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
			animator.SetTrigger ("Damage");
			//Enemyと接触したらダメージ
		} else if (collider.gameObject.tag == "Enemy") {
			armorPoint -= damage;
			armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
			animator.SetTrigger ("Damage");
		}
		//Enemyとぶつかった時にコルーチンを実行（下記IEnumerator参照）
		if (collider.gameObject.tag == "Enemy") {
			StartCoroutine ("DamageCoroutine");
		}
		//ShotEnemyとぶつかった時にコルーチンを実行（下記IEnumerator参照）
		else if (collider.gameObject.tag == "ShotEnemy") {
			StartCoroutine ("DamageCoroutine");
		}
		//Itemタグをつけたもの（RedSphere）を取ったら体力1000回復
		else if (collider.gameObject.tag == "Item") {
			armorPoint += 1000;
			// 体力上限以上には回復しない。
			armorPoint = Mathf.Clamp (armorPoint, 0, armorPointMax);
		}
	}

	// Itweenを使ってコルーチン作成（Itweenインストール必要あり）
	IEnumerator DamageCoroutine ()
	{
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
		gameObject.layer = LayerMask.NameToLayer("Player");
		//iTweenのアニメーション

	}

	//IEnumerator("confirmCommand");				
	//	while (true){
	//Axis
	//		if(commandEnable){				//方向キーとショット（fire）が入ってない状態で
	//			getAxis();
	//			getFire();
	//		}else{
	//			inputCommands += " ";　 	//なおかつコマンドが入ってない状態で
	//		}
	//		confirmCommand();				//コマンド開始
	//		yield return null;
	//	}//end While
	//}

	//void getAxis(){						//制限時間内にコマンド入力
	//	if(Input.GetAxisRaw("Vertical") > 0.9){
	//		if(Input.GetAxisRaw("Horizontal") > 0.9){Input.GetButtonDown("");}
	//		else if (Input.GetAxisRaw("Horizontal") < -0.9){Input.GetButtonDown("");}
	//		else {Input.GetButtonDown("");}
	//	}
	//	if(inputCommands.Length > recCommandLength){
	//		inputCommands = inputCommands.Remove(0,1);
	//	}    
	//}
	//IEnumerator DefenseCoroutine ()
	//{
	//	gameObject.layer = LayerMask.NameToLayer("PlayerDefense");
	//	int count = 1;
	//	iTween.MoveTo(gameObject, iTween.Hash(
	//		"position", transform.position - (transform.forward * 0f),
	//		"time", 0.5f, 
	//		"easetype", iTween.EaseType.linear
	//	));
	//	isInvincible = true;
	//	while (count > 0){
	//		modelColorChange.ColorChange(new Color (1,0,0,1));
	//		yield return new WaitForSeconds(0.1f);
	//		count--;
	//	}
	//	isInvincible = false;
	//	gameObject.layer = LayerMask.NameToLayer("Player");

	//}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B2FloorBlock : MonoBehaviour {

	private ModelColorChange modelColorChange;
	public float DestroyTime = 0.1f;			//消滅するまでの時間
	public float FlashTime = 1.0f;				//点滅時間
	public float WaitTime = 2.0f;				//B1FloorBlockが消え始めるまでの時間

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// プレイヤタグの付いたオブジェクトが接触したらFallをFallTime後に起動
	void OnCollisionEnter(Collision collision){
		if(collision.gameObject.CompareTag("Player")){
			StartCoroutine ("DestroyCoroutine");
		}
	}

	// 壁接触時の点滅
	IEnumerator DestroyCoroutine ()
	{
		//while文を10回ループ
		int count = 4;
		iTween.MoveTo(gameObject, iTween.Hash(
			"time", FlashTime, // 好きな時間（秒）
			"easetype", iTween.EaseType.linear
		));
		while (count > 0){
			//数秒してから点滅開始
			yield return new WaitForSeconds(WaitTime);
			//透明にする
			modelColorChange.ColorChange(new Color (1,0,0,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			//元に戻す
			modelColorChange.ColorChange(new Color (1,1,1,1));
			//0.1秒待つ
			yield return new WaitForSeconds(0.1f);
			count--;
			Destroy(gameObject, DestroyTime);
		}
	}
}

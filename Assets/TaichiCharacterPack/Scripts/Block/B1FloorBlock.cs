using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤが接触したら落下するブロック
// RigidBodyのisKinematicにチェックをいれておく
public class B1FloorBlock : MonoBehaviour {
	private ModelColorChange modelColorChange;
	public float FalllTime = 0.5f;				//FloorBlockが始めるまでの時間
	public float DestroyTime = 2.0f;			//消滅するまでの時間
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
			Invoke("Fall", FalllTime);
		}
	}

	// isKinematic(固定）のチェックを外して落下させ、DestroyTime後に消滅
	void Fall(){
		GetComponent<Rigidbody>().isKinematic = false;
		StartCoroutine ("FloorDestroyCoroutine");
	}

	// 壁接触時の点滅
	IEnumerator FloorDestroyCoroutine ()
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

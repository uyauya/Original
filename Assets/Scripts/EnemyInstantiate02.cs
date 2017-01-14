using UnityEngine;
using System.Collections;

public class EnemyInstantiate02 : MonoBehaviour {

	public GameObject enemy02;    //敵オブジェクト
	public Transform ground;    //地面オブジェクト
	public float count = 1;     //一度に何体のオブジェクトをスポーンさせるか
	public float interval = 10;  //何秒おきに敵を発生させるか
	private float timer;        //経過時間

	void Start () {
		Spawn();    //初期スポーン
	}
	

	void Update () {
		timer += Time.deltaTime;    //経過時間加算
		if(timer >= interval){
			Spawn();    //スポーン実行
			timer = 0;  //初期化
		}
	}
	
	void Spawn () {
		for(int i = 0; i < count; i++) {
			float x = Random.Range(-15f,15f);
			float z = Random.Range(-15f,15f);
			Vector3 pos = new Vector3(x, 5, z) + ground.position;
			GameObject.Instantiate(enemy02, pos, Quaternion.identity);
		}
	}
}


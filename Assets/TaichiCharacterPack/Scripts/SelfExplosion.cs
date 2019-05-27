using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//自爆して周囲にダメージを周囲を吹き飛ばす（吹き飛ばす力をランダムにする）
public class SelfExplosion : MonoBehaviour
{
	public float DestroyTime = 1.0f;

	// Start is called before the first frame update
    void Start()
    {
		//ランダムな吹き飛ぶ力を加える
		Vector3 force = Vector3.up * 1000.0f + Random.insideUnitSphere * 300f;
		GetComponent<Rigidbody>().AddForce(force);

		//ランダムに吹き飛ぶ回転力を加える
		Vector3 torque = new Vector3(Random.Range(-10000.0f,10000.0f),Random.Range(-10000.0f,10000.0f),
			Random.Range(-10000.0f,10000.0f));
		GetComponent<Rigidbody>().AddForce(torque);

		//1秒後に自分を消去
		Destroy(gameObject,DestroyTime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss03Bullet : MonoBehaviour
{
    public float DestroyTime = 5;		// 射出後消滅するまでの時間
    public GameObject explosion;		// 弾の爆発

    // Start is called before the first frame update
    void Start()
    {
        //出現後一定時間(DestroyTime)で自動的に消滅させる
        Destroy(gameObject, DestroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        //ボスが死んだら弾も消滅
        if (BossBasic.BossDead == true)
		{
            Debug.Log("死亡");
            Destroy (gameObject);
			
		}
    }

    private void OnCollisionEnter(Collision collider)
    {

        //プレイヤータグの付いたオブジェクトと衝突したら爆発して消滅する
        if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Shot")
        {
            Debug.Log("衝突");
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
    
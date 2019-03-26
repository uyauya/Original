using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAttack : MonoBehaviour
{
	
	private AudioSource audioSource;
	public GameObject HitEffectPrefab;				
	public GameObject HitEffectObject;
	public Transform EffectPoint;				//エフェクト発生元の位置取り
	public static bool Hit = false;				//敵に当たったかどうか

    // Start is called before the first frame update
    void Start()
    {
		audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnCollisionEnter (Collision collider)
	{
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "ShotEnemy") {
			HitEffectObject = Instantiate (HitEffectPrefab, EffectPoint.position, Quaternion.identity);
			HitEffectObject.transform.SetParent (EffectPoint);
			Hit = true;
		}
	}
}

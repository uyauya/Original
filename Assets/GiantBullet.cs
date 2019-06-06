using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBullet : MonoBehaviour
{
	public float DestroyTime = 5;		
	public GameObject explosion;		

	// Start is called before the first frame update
	void Start()
	{
		Destroy(gameObject, DestroyTime);
	}

	// Update is called once per frame
	void Update()
	{
		if (BossBasic.BossDead == true)
		{
			Destroy (gameObject);

		}
	}

	private void OnCollisionEnter(Collision collider)
	{
		if (collider.gameObject.tag == "Player" || collider.gameObject.tag == "Shot")
		{
			Destroy(gameObject);
			Instantiate(explosion, transform.position, transform.rotation);
		}
	}

}

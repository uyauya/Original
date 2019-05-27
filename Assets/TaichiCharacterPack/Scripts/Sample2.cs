using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample2 : MonoBehaviour 
{


	// 操作可能フラグ
	private bool isControllable = true;

	private void Update()
	{ 
		if (!isControllable)
		{
			return;
		}

		var rigidbody = GetComponent<Rigidbody2D>();
		if (Input.GetAxis("Horizontal") < 0)
		{
			rigidbody.velocity = new Vector2(-1f, rigidbody.velocity.y);
		}
		else if (Input.GetAxis("Horizontal") > 0)
		{
			rigidbody.velocity = new Vector2(1f, rigidbody.velocity.y);
		}
		else
		{
			rigidbody.velocity = new Vector2(0f, rigidbody.velocity.y);
		}

		if (Input.GetButtonDown("Jump"))
		{
			rigidbody.AddForce(new Vector2(0f, 250f));
		}
	}

	/// <summary>
	/// 他のオブジェクトと衝突した際の処理
	/// </summary>
	/// <param name= "coll">Coll.</param>
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if ("Enemy" == coll.gameObject.tag)
		{
			Debug.Log("ここきてる？？");
			StartCoroutine(CollisionToEnemyCoroutine(coll));
		}
	}

	/// <summary>
	/// Enemyと当たった時のリアクション
	/// </summary>
	/// <returns>The to enemy coroutine.</returns>
	/// <param name="coll">Coll.</param>
	private IEnumerator CollisionToEnemyCoroutine(Collision2D coll)
	{
		// 操作不能になる
		isControllable = false;
		Debug.Log("ここきてる？？");
		var rigidbody = GetComponent<Rigidbody2D>();
		// 吹き飛ばされる
		rigidbody.AddForce(new Vector2(transform.position.x < coll.transform.position.x? -100f: 100f, 100f));
		yield return new WaitForSeconds(0.5f);
		// 0.5秒後、操作可能に
		isControllable = true;
	}
}
	


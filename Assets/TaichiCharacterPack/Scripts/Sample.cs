using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sample : MonoBehaviour {

		public GameObject bulletPrefab;
		private GameObject hiyoko;
		private bool isMovable = true;
		private bool isJumpable = true;
		private float velocityX = 0f; 

		private void Start()
		{
			// ひよこちゃんを追っかけさせるため、ゲームオブジェクトを取得
			hiyoko = GameObject.Find("Hiyoko");
			StartCoroutine(ChaseCoroutine());
			StartCoroutine(ShootIfPossibleCoroutine());
		}
		　　
		private void Update()
		{
			// 移動させるための力を加える
			var rigidbody = GetComponent<Rigidbody2D>();
			rigidbody.velocity = new Vector2(isMovable ? velocityX : 0f, rigidbody.velocity.y);

			// ジャンプ処理
			if (hiyoko.transform.position.y > transform.position.y&& rigidbody.velocity.y == 0f)
			{
				　// ひよこちゃんが自分より高い位置に居るならジャンプする
				　JumpIfPossible();
			}
		}

		/// <summary>
		/// ひよこちゃんを追っかけるためのコルーチンです。
		/// </summary>
		/// <returns>The coroutine.</returns>
		private IEnumerator ChaseCoroutine()
		{
			while (true)
			{
				// ひよこちゃんが左右どちらに居るかをチェック、進行方 向を決めて追いかける
				velocityX = hiyoko.transform.position.x > transform.position.x ? 0.5f : -0.5f;

				// 次の方向転換は3秒後
				yield return new WaitForSeconds(3f);
			}
		}

		/// <summary>
		/// 弾を発射するかどうか制御するコルーチンです。
		/// </summary>
		/// <returns>The coroutine.</returns>
		private IEnumerator ShootIfPossibleCoroutine()
		{
			while (true)
			{
				if (Mathf.Abs(hiyoko.transform.position.x - transform.position.x) > 1.5f)
				{
					// ひよこちゃんと一定以上の距離が離れているならば、進行方向に向かって弾を発射
					StartCoroutine(ShootBulletAndDestroyCoroutine());
					// クールダウンで2秒立ち止まる
					isMovable = false;
					yield return new WaitForSeconds(2f);
					isMovable = true ;
				}
				else
				{
					yield return new WaitForSeconds(0.1f);
				}
			}
		}

		/// <summary>
		/// 弾発射から消滅までを制御するコルーチンです。
		/// </ summary>
		/// <returns>The bullet and destroy coroutine.</returns>
		private IEnumerator ShootBulletAndDestroyCoroutine()
		{
			var bullet = Instantiate(bulletPrefab);
			bullet.transform.position = transform.position + new Vector3(0f, 0.3f);
			// 発射
			bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(velocityX* 3f, 5f); 
			// 発射してから5秒待つ
			yield return new WaitForSeconds(5f);
			// 発射した弾を削除
			Destroy(bullet);
		}

		/// <summary >
		/// ジャンプ可能であればジャンプします。
		/// </summary>
		private void JumpIfPossible()
		{
			if (!isJumpable)
			{
				return;
			}
			var rigidbody = GetComponent<Rigidbody2D>();
			rigidbody.AddForce( new Vector2(0f, 250f));
			isJumpable = false;
			StartCoroutine(JumpCooldownCoroutine());
		}

		/// <summary>
		///ジャンプ後、次にジャンプ可能になるまでのクールダウンを制御します。
		/// </summary>
		/// <returns>The cooldown coroutine.</returns>
		private IEnumerator JumpCooldownCoroutine()
		{
			yield return new WaitForSeconds(3f);
			isJumpable = true;
		}
	}


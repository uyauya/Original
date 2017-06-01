using UnityEngine;
using System.Collections;

public class Boss02Shot : MonoBehaviour {

	public GameObject explosion;
	public float amplitude = 0.01f;		// 振幅
	private int frameCnt = 0;			// フレームカウント
	private Vector3 _dir;
	private float _speed;
	public GameObject SmallBoss02;
	private float Interval=0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Interval > 0.5f) {
			// スピードをランダムにする
			float x = Random.Range (-0.5f,0.5f);
			float y = Random.Range (-0.5f,0.5f);
			float z = Random.Range (-0.5f,0.5f);
			gameObject.transform.position = new Vector3 (x, y, z);
			_speed = Random.Range (1, 9) / 10f;
			// 方向をランダムにする
			x = Random.Range (-0.1f, 0.1f);
			y = Random.Range (-0.1f, 0.1f);
			z = Random.Range (-0.1f, 0.1f);
			_dir = new Vector3 (x, y, z);
		}
		Interval += Time.deltaTime;
		//現後一定時間で自動的に消滅させる
		Destroy(gameObject, 4.0F);
		//弾を前進させる
		gameObject.transform.Translate (_dir * _speed);
	}

	private void OnCollisionEnter(Collision collider) {
		
		//プレイヤーと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			if (Random.Range (0, 6) == 0) {
				Instantiate (SmallBoss02, transform.position, transform.rotation);
			}
		} else if (collider.gameObject.tag == "Shot") {
			Destroy (gameObject);
			Instantiate (explosion, transform.position, transform.rotation);
			if (Random.Range (0, 6) == 0) {
				Instantiate (SmallBoss02, transform.position, transform.rotation);
			}
		}
	}

	IEnumerator Boss02ShotCoroutine () {
		frameCnt += 1;
		if (10000 <= frameCnt) {
			frameCnt = 0;
		}
		if (0 == frameCnt % 2) {
			// 上下に振動させる
			float posYSin = Mathf.Sin (2.0f * Mathf.PI * (float)(frameCnt % 200) / (200.0f - 1.0f));
			iTween.MoveAdd (gameObject, new Vector3 (0, amplitude * posYSin, 0), 0.0f);
			yield return new WaitForSeconds (0.1f);
		}
	}
}


using UnityEngine;
using System.Collections;

public class Boss01Shot : MonoBehaviour {

	public GameObject explosion;
	public float amplitude = 0.01f;		// ショットの揺れ具合
	private int frameCnt = 0;			// ショットをランダムで揺らすための時間取り
	private Vector3 _dir;				// ショット方向
	private float _speed;				// ショット速度
	public float DestroyTime = 5;		// 発射してから消滅するまでの時間
	public GameObject SmallBoss01;		// ショット消滅後、出てくる敵
	public float XspeedS = -0.1f;		// X方向最低速度
	public float XspeedL = 0.1f;		// X方向最高速度	
	public float YspeedS = -0.1f;
	public float YspeedL = 0.1f;
	public float ZspeedS = -0.1f;
	public float ZspeedL = 0.1f;
	public float XdirectionS = -0.1f;	// X方向範囲（ここから）
	public float XdirectionL = 0.1f;	// X方向範囲（ここまで）
	public float YdirectionS = -0.1f;
	public float YdirectionL = 0.1f;
	public float ZdirectionS = -0.1f;
	public float ZdirectionL = 0.1f;
	public int Insta = 6;				// 確率（smallBoss02発生確率用）
	private float Interval = 0;


	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		//if (Interval > 0.5f)
		{
			// スピードをランダムにする
			float x = Random.Range (XspeedS,XspeedL);
			float y = Random.Range (YspeedS,YspeedL);
			float z = Random.Range (ZspeedS,ZspeedL);
			gameObject.transform.localPosition += new Vector3 (x, y, z);
			_speed = Random.Range (1, 9) / 10f;
			// 方向をランダムにする
			x = Random.Range (XdirectionS,XdirectionL);
			y = Random.Range (YdirectionS,YdirectionL);
			z = Random.Range (ZdirectionS,ZdirectionL);
			_dir = new Vector3 (x, y, z);
		}
		//Interval += Time.deltaTime;
		// 現後一定時間(DestroyTime)で自動的に消滅させる
		Destroy(gameObject, DestroyTime);
		// 弾を(_dir方向 * _speed速度で)前進させる
		gameObject.transform.Translate (_dir * _speed);
		// ショットコルーチン開始（下記参照）
		StartCoroutine ("Boss01ShotCoroutine");
	}

	private void OnCollisionEnter(Collision collider) {

		//プレイヤータグの付いたオブジェクトと衝突したら爆発して消滅する
		if (collider.gameObject.tag == "Player") {
			Instantiate (explosion, transform.position, transform.rotation);
			// ランダム（Insta数分の１でその場にSmallBoss02発生）
			// 数値は0から始まるので(0, Insta)となる
			if (Random.Range (0, Insta) == 0) {
				Instantiate (SmallBoss01, transform.position, transform.rotation);
			}
			Destroy (gameObject);

		} else if (collider.gameObject.tag == "Shot") {
			Instantiate (explosion, transform.position, transform.rotation);
			if (Random.Range (0, Insta) == 0) {
				Instantiate (SmallBoss01, transform.position, transform.rotation);
			}
			Destroy (gameObject);
		}
	}

	IEnumerator Boss01ShotCoroutine () {

		frameCnt += 1;
		// フレームカウントリセット用
		if (10000 <= frameCnt) {
			frameCnt = 0;
		}
		if (0 == frameCnt % 700) {
			// 上下に振動させる
			float posYSin = Mathf.Sin (2.0f * Mathf.PI * (float)(frameCnt % 200) / (200.0f - 1.0f) * 0.1f);
			iTween.MoveAdd (gameObject, new Vector3 (0, amplitude * posYSin, 0), 0.0f);
			yield return new WaitForSeconds (0.1f);
		}
	}
}


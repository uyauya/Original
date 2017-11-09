﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プレイヤー用ガード（レバー進行方向2回押しでガード壁出し）
// レバー進行方向2回押し＋Fire3でエネルギー吸収（回復）壁出し
public class DiffenceAbsorb : MonoBehaviour {

	public GameObject DiffenceWall;				// ガード用壁
	public GameObject AbsorbWall;				// エネルギー吸収用壁
	public Transform muzzle;					// 壁発射元
	private Animator animator;
	private AudioSource audioSource;
	private Rigidbody rb;
	private Vector3 velocity = Vector3.zero;
	private Vector3 input = Vector3.zero;
	public bool diffence = false;        	  	//　ガードしているか
	public bool push = false;          		  	//　最初に移動ボタンを押したかどうか
	public float nextButtonDownTime;    	  	//　次に移動ボタンが押されるまでの時間
	private float nowTime = 0f;         	  	//　最初に移動ボタンが押されてからの経過時間
	public float limitAngle;            	  	//　最初に押した方向との違いの限度角度
	private Vector2 direction = Vector2.zero;   //　移動キーの押した方向
	private Pause pause;
	public int PlayerNo;
	public int boostPoint;						// ブーストポイント
	public Transform EffectPoint;				// 回復等エフェクト発生元の位置取り
	public GameObject BpHealPrefab;				// ブーストポイント回復エフェクト格納場所
	public GameObject BpHealObject;

	void Start()
	{
		audioSource = gameObject.GetComponent<AudioSource>();
		animator = GetComponent<Animator> ();
		rb = GetComponent<Rigidbody>();
		pause = GameObject.Find ("Pause").GetComponent<Pause> ();
	}


	void Update()
	{
		velocity = Vector3.zero;
		if (pause.isPause == false) {
			//　ガードしていないとき
			if (!diffence) {
				//　移動キーを押した
				if ((Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical"))) {
					//　最初に1回押していない時は押した事にする
					if (!push) {
						push = true;
						//　最初に移動キーを押した時にその方向ベクトルを取得
						direction = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
						nowTime = 0f;
						//　2回目のボタンだったら1→２までの制限時間内だったらガード
					} else if ((Input.GetButtonDown ("Horizontal") || Input.GetButtonDown ("Vertical"))) {
						//　2回目に移動キーを押した時の方向ベクトルを取得
						var nowDirection = new Vector2 (Input.GetAxisRaw ("Horizontal"), Input.GetAxisRaw ("Vertical"));
						//Debug.LogFormat("Vector2.Angle:{0} LimitAngle:{1} Time.time:{2} nowTime:{3} nextButtonDownTime:{4}",Vector2.Angle(nowDirection, direction),limitAngle,Time.time,nowTime,nextButtonDownTime);
						//　押した方向がリミットの角度を越えていない　かつ　制限時間内に移動キーが押されていればガード
						if (Vector2.Angle (nowDirection, direction) < limitAngle
						   && nowTime <= nextButtonDownTime) {							
							//&& Time.time - nowTime < nextButtonDownTime)
							//Debug.LogFormat ("出る時：Vector2.Angle:{0} LimitAngle:{1} Time.time:{2} nowTime:{3} nextButtonDownTime:{4}", Vector2.Angle (nowDirection, direction), limitAngle, Time.time, nowTime, nextButtonDownTime);
							diffence = true;
							if (PlayerNo == 0) {
								SoundManager.Instance.Play(0,gameObject);
							}
							if (PlayerNo == 1) {
								SoundManager.Instance.Play(1,gameObject);
							}
							if (PlayerNo == 2) {
								SoundManager.Instance.Play(2,gameObject);
							}
							Diffencer ();
							Debug.Log ("Diffence");
							diffence = false;
							push = false;
						} else if (nowTime > nextButtonDownTime) {
							diffence = false;
							push = false;
						}

						if (Vector2.Angle (nowDirection, direction) < limitAngle && (Input.GetButton ("Fire3"))
						   && nowTime <= nextButtonDownTime) {
							diffence = true;
							if (PlayerNo == 0) {
								SoundManager.Instance.Play(0,gameObject);
							}
							if (PlayerNo == 1) {
								SoundManager.Instance.Play(1,gameObject);
							}
							if (PlayerNo == 2) {
								SoundManager.Instance.Play(2,gameObject);
							}
							Absorb ();
							Debug.Log ("Absorb");
							diffence = false;
							push = false;
						} else if (nowTime > nextButtonDownTime) {
							diffence = false;
							push = false;
						}
					} else {
					}
				}
			}

			//　最初の移動キーを押していれば時間計測
			//　最初の移動キーを押していれば時間計測
			if (push) {
				//　時間計測
				nowTime += Time.deltaTime;

				if (nowTime > nextButtonDownTime) {
					push = false;
				}
			}

			input = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		}
	}

	void Diffencer() {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject diffenceObject = GameObject.Instantiate (DiffenceWall)as GameObject;
		//　弾丸をmuzzleから発射(muzzleはCreateEmptyでmuzzleと命名し、プレイヤーの発射したい位置に設置)
		diffenceObject.transform.position = muzzle.position;
	}
	void Absorb() {
		// Bullet01のゲームオブジェクトを生成してbulletObjectとする
		GameObject absorbObject = GameObject.Instantiate (AbsorbWall)as GameObject;
		// Diffencerと重ならないようAbsorbを少し前に置く
		absorbObject.transform.position = muzzle.position + new Vector3 (0, 0, 0.2f);
	}

	private void OnCollisionEnter (Collision collider)
	{
		// アイテム２タグの物に接触したらブーストポイント回復
		if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "ShotEnemy") {
			BpHealObject = Instantiate (BpHealPrefab, EffectPoint.position, Quaternion.identity);
			BpHealObject.transform.SetParent (EffectPoint);
			animator.SetTrigger ("Absorb");
			if (PlayerNo == 0) {
				SoundManager.Instance.Play (18, gameObject);
			}
			if (PlayerNo == 1) {
				SoundManager.Instance.Play (19, gameObject);
			}
			if (PlayerNo == 2) {
				SoundManager.Instance.Play (20, gameObject);
			}
			boostPoint += 100;
		}
	}
} 
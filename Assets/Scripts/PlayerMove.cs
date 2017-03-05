using UnityEngine;
using System.Collections;

using UnityEngine.UI;
using UnityStandardAssets.ImageEffects;

public class PlayerMove : MonoBehaviour
{

	public float speed = 3.0F;
	public float jumpSpeed = 8.0F;
	public float gravity = 9.8F;
	private Vector3 moveDirection = Vector3.zero;
	int boostPoint;
	public int boostPointMax = 1000;
	public Image gaugeImage;
	Vector3 moveSpeed;
	const float addNormalSpeed = 10F;
	//通常時の加算速度
	const float addBoostSpeed = 10;
	//ブースト時の加算速度
	const float moveSpeedMax = 40;
	//通常時の最大速度
	const float boostSpeedMax = 80;
	//ブースト時の最大速度
	private Animator animator;
	bool isBoost;

	void Start ()
	{    // ゲーム起動時の処理
		boostPoint = boostPointMax;
		moveSpeed = Vector3.zero;
		isBoost = false;
		animator = GetComponent<Animator> ();
	}

	void Update ()
	{

		//プレイヤーを移動させる
		// CharacterControllerをこれ以降controllerと略す,CharacterControllerを使えるようにする
		CharacterController controller = GetComponent<CharacterController> ();
		// 地面にいれば進行方向をュートラルにする
		if (controller.isGrounded)
			moveDirection.y = 0;

		//ブーストボタンが押されていればフラグを立てブーストポイントを消費
		if (Input.GetButton ("Boost") && boostPoint > 10) {
			boostPoint -= 10;
			isBoost = true;
		} else {
			isBoost = false;
		}
		Vector3 targetSpeed = Vector3.zero;        //目標速度
		Vector3 addSpeed = Vector3.zero;        //加算速度

		//左右移動時の目標速度と加算速度
		if (Input.GetAxis ("Horizontal") == 0) {

			//押していないときは目標速度を0にする
			targetSpeed.x = 0;

			//設置しているときと空中にいるときは減速値を変える
			// 地上時
			if (controller.isGrounded)
				addSpeed.x = addNormalSpeed;
			// 地上以外時
			else
				addSpeed.x = addNormalSpeed / 4;
		} else {

			//通常時とブースト時で変化
			if (isBoost) {
				// ブースト時
				targetSpeed.x = boostSpeedMax;
				addSpeed.x = addBoostSpeed;
			} else {
				// 通常時
				targetSpeed.x = moveSpeedMax;
				addSpeed.x = addNormalSpeed;
			}

			targetSpeed.x *= Mathf.Sign (Input.GetAxis ("Horizontal"));
		}

		//左右移動の速度
		moveSpeed.x = Mathf.MoveTowards (moveSpeed.x, targetSpeed.x, addSpeed.x);
		moveDirection.x = moveSpeed.x;

		//前後移動の目標速度と加算速度
		if (Input.GetAxis ("Vertical") == 0) {
			// ニュートラル
			targetSpeed.z = 0;
			// 地上時
			if (controller.isGrounded)
				addSpeed.z = addNormalSpeed;
			// 地上以外（空中）
			else
				addSpeed.z = addNormalSpeed / 4;
		} else {
			// ブースト時
			if (isBoost) {
				targetSpeed.z = boostSpeedMax;
				addSpeed.z = addBoostSpeed;
				// 通常時
			} else {
				targetSpeed.z = moveSpeedMax;
				addSpeed.z = addNormalSpeed;
			}

			targetSpeed.z *= Mathf.Sign (Input.GetAxis ("Vertical"));
		}

		//水平移動の速度
		moveSpeed.z = Mathf.MoveTowards (moveSpeed.z, targetSpeed.z, addSpeed.z);
		moveDirection.z = moveSpeed.z;
		moveDirection = transform.TransformDirection (moveDirection);

		//ジャンプキーによる上昇
		if (Input.GetButtonDown ("Jump")) {
			// ジャンプの最大値より上に上昇しない（一定以上なら上昇ゼロ）
			if (transform.position.y > 20)
				moveDirection.y = 0;
			// ジャンプの最大値までは上昇
			moveDirection.y += gravity * Time.deltaTime;
		} else if (Input.GetButton ("Jump") && (Input.GetButton ("Boost") && boostPoint > 20)) {
			// ジャンプの最大値より上に上昇しない（一定以上なら上昇ゼロ）
			animator.SetBool("BoostUp", Input.GetButton ("Jump"));
			if (transform.position.y > 120)
				moveDirection.y = 0;
			// ジャンプの最大値までは上昇
			moveDirection.y += gravity * Time.deltaTime;
			boostPoint -= 10;
		} else {
			// それ以外の場合は重力にそって落下
			moveDirection.y -= gravity * Time.deltaTime;
		}
		// ブーストやジャンプが入力されていなければブースとポイントが徐々に回復
		if (!Input.GetButton ("Boost") && !Input.GetButton ("Jump"))
			boostPoint += 5;
		// ブーストポイントが最大以上にはならない
		boostPoint = Mathf.Clamp (boostPoint, 0, boostPointMax);
		// コントローラの進行方向にそって動く
		controller.Move (moveDirection * Time.deltaTime);

		//移動速度に合わせてモーションブラーの値を変える（MainCameraにCameraMotionBlurスクリプトを追加)
		//MainCameraのInspectorのCameraMotionBlurのExcludeLayersでプレイヤーと敵を選択して
		//プレイヤーと的にはモーションブラーがかからないようにする
		float motionBlurValue = Mathf.Max (Mathf.Abs (moveSpeed.x), Mathf.Abs (moveSpeed.z)) / 20;
		motionBlurValue = Mathf.Clamp (motionBlurValue, 0, 5);
		Camera.main.GetComponent<CameraMotionBlur> ().velocityScale = motionBlurValue;

		//ブーストゲージの伸縮
		// ゲージの最大以上には上がらない
		gaugeImage.transform.localScale = new Vector3 ((float)boostPoint / boostPointMax, 1, 1);
	}

	// アイテム２タグの物に接触したらブーストポイント回復
	private void OnCollisionEnter (Collision collider)
	{
		if (collider.gameObject.tag == "Item2") {
			animator.SetTrigger ("ItemGet");
			boostPoint += 500;
			// ブーストポイントが最大以上にはならない
			boostPoint = Mathf.Clamp (boostPoint, 0, boostPointMax);
		}
	}

}

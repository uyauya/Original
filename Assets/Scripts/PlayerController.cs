using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour {

    private Animator animator;
    // 移動時に加える力
    private float force = 120.0f;

    public float speed = 3.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 9.8F;
    private Vector3 moveDirection = Vector3.zero;
    int boostPoint;
    public int boostPointMax = 1000;
    public Image gaugeImage;
    Vector3 moveSpeed;
    const float addNormalSpeed = 1;
    //通常時の加算速度
    const float addBoostSpeed = 2;
    //ブースト時の加算速度
    const float moveSpeedMax = 4;
    //通常時の最大速度
    const float boostSpeedMax = 8;
    //ブースト時の最大速度
    private int JumpCount;
    bool isBoost;

    Vector3 targetSpeed = Vector3.zero;        //目標速度
    Vector3 addSpeed = Vector3.zero;        //加算速度


    void Start()
    {
        animator = GetComponent<Animator>();
        boostPoint = boostPointMax;
        moveSpeed = Vector3.zero;
        isBoost = false;
    }


    void Update()
    {

        //ブーストボタンが押されていればフラグを立てブーストポイントを消費
        if (Input.GetButton("Boost") && boostPoint > 10)
        {
            boostPoint -= 10;
            isBoost = true;
        }
        else
        {
            isBoost = false;
        }

        //通常時とブースト時で変化
        if (isBoost)
        {
            // ブースト時
            targetSpeed.x = boostSpeedMax;
            addSpeed.x = addBoostSpeed;

            targetSpeed.z = boostSpeedMax;
            addSpeed.z = addBoostSpeed;
        }
        else
        {
            // 通常時
            targetSpeed.x = moveSpeedMax;
            addSpeed.x = addNormalSpeed;

            targetSpeed.z = moveSpeedMax;
            addSpeed.z = addNormalSpeed;
        }



        //モーションを切り替える
        if (Input.GetAxis("Horizontal") > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 90, 0), Time.deltaTime * 5.0f);
            animator.SetBool("Move", true);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -90, 0), Time.deltaTime * 5.0f);
            animator.SetBool("Move", true);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

        }
        else if (Input.GetAxis("Vertical") > 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5.0f);
            animator.SetBool("Move", true);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, -180, 0), Time.deltaTime * 5.0f);
            animator.SetBool("Move", true);
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

        }
        else
        {
            animator.SetBool("Move", false);
        }

        //ジャンプモーションに切り替える
        animator.SetBool("Jump", Input.GetButton("Jump"));

        //ブーストキーが押されたらにパラメータを切り替える
        animator.SetBool("Boost", Input.GetButton("Boost"));

    }

}

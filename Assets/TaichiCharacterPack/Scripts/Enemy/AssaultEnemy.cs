using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//フヨフヨ漂いながら移動する（収縮する）、ヒットさせるとしぼんでいく
public class AssaultEnemy : MonoBehaviour
{
    protected EnemyBasic enemyBasic;
    Rigidbody rigidbody;
    private float chargeTime = 5.0f;    //方向転換するまでの時間制限
    private float timeCount;
    public Vector3 BasicPoint;          // 出現時の座標（地上からの高さを決める）
    bool damageSet;                     //被ダメージ処理、一時的に移動不可(下記参照)
    public float DamageTime = 0.5f;     //ダメージ処理(硬直)時間
    public float AssaultTime = 0.5f;
    bool freezeSet;                     //フリーズ処理、一時的に移動不可
    public float FreezeTime = 1.0f;     //フリーズ処理(硬直)時間
    public float LastEnemySpeed;	 	//ダメージ、フリーズ処理する前の敵の基本スピード
    int LayerMask = ~(1 << 8);		    //8はlayerのPlayer。　playerにはRayCastHitしない
    public bool RighrtMove = false;
    public bool LeftMove = false;
    public bool UpMove = false;
    public float RandomMoeCount = 0;

    void Start()
    {
        enemyBasic = gameObject.GetComponent<EnemyBasic>();
        enemyBasic.Initialize();
        rigidbody = GetComponent<Rigidbody>();
        BasicPoint = new Vector3(this.transform.position.x, this.transform.position.y + 2.0f, this.transform.position.z);
    }

    void Update()
    {
        damageSet = GetComponent<EnemyBasic>().DamageSet;
        freezeSet = GetComponent<EnemyBasic>().FreezeSet;
        rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        Vector3 Ros = this.gameObject.transform.rotation.eulerAngles;
        gameObject.transform.eulerAngles = new Vector3(1, Ros.y, 1);
        enemyBasic.timer += Time.deltaTime;
        timeCount += Time.deltaTime;

        rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 1.0f))
        {
            RandomMoeCount -= Time.deltaTime;
            if (RandomMoeCount <= 0)
            {
                RandomMove();
                RandomMoeCount = 5;
            }
        }
        enemyBasic.timer += Time.deltaTime;
        // 上下移動
        // 上記同様Y軸にSin式を入れ上下させる
        this.transform.position = new Vector3(this.transform.position.x, this.BasicPoint.y + Mathf.Sin(Time.time),
            this.transform.position.z);
        //敵の攻撃範囲を設定する
        //相手の位置と自分の位置の差がTargetRange以内なら
        if (Vector3.Distance(enemyBasic.target.transform.position, transform.position) <= enemyBasic.TargetRange)
        {
            //ターゲットの方を徐々に向く
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
            rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        }
        //Playerが近くにいなければ敵の移動方向をランダムで変える
        else if (timeCount > chargeTime)
        {
            Vector3 course = new Vector3(0, Random.Range(0, 360), 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
            timeCount = 0;
        }
        // ターゲット（プレイヤー）との距離がSearch以内なら
        if (Vector3.Distance(enemyBasic.target.transform.position, transform.position) <= enemyBasic.Search)
        {
            //ターゲットの方を徐々に向く
            // Quaternion.LookRotation(A位置-B位置）でB位置からA位置を向いた状態の向きを計算
            // Quaternion.Slerp（現在の向き、目標の向き、回転の早さ）でターゲットにゆっくり向く
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate);
            rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed);
        }

        if (EnemyTargetRange.isAttackDesision == true)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation
                (enemyBasic.target.transform.position - transform.position), Time.deltaTime * enemyBasic.EnemyRotate * 10.0f);
            rigidbody.velocity = (transform.forward * enemyBasic.EnemySpeed * 5.0f);
        }

        if (EnemyTargetRange.isAttackDesision == false)
        {
            return;
        }

        //damageSet時、スピードが0なら何もしない。0でないならDamageSetCoroutine起動（下記参照）
        if (damageSet == true)
        {
            if (LastEnemySpeed == 0)
            {
                return;
            }
            else
            {
                StartCoroutine("DamageSetCoroutine");
            }
        }
        //freezeSet時、スピードが0なら何もしない。0でないならDamageSetCoroutine起動（下記参照）
        if (freezeSet == true)
        {
            if (LastEnemySpeed == 0)
            {
                return;
            }
            else
            {
                StartCoroutine("FreezeSetCoroutine");
            }
        }

    }

    public void RandomMove()
    {
        int num = Random.Range(0, 8);
        if (num <= 2)
        {
            RighrtMove = true;
            LeftMove = false;
            UpMove = false;
        }
        else if (num > 3 && num <= 5)
        {
            LeftMove = true;
            RighrtMove = false;
            UpMove = false;
        }
        else if (num > 6)
        {
            UpMove = true;
            RighrtMove = false;
            LeftMove = false;
        }
    }


    //攻撃が当たったらDamageTime分だけSpeedをゼロにする（動きを止める）
    IEnumerator DamageSetCoroutine()
    {
        enemyBasic.DamageSet = false;
        LastEnemySpeed = enemyBasic.EnemySpeed;         //直前の動きの速さをLastEnemySpeedとして保存
        enemyBasic.EnemySpeed = 0;                      //スピードを0にする（硬直処理）
        yield return new WaitForSeconds(DamageTime);    //DamageTimeが経過したら
        enemyBasic.EnemySpeed = LastEnemySpeed;         //LastEnemySpeedに戻して再び移動可能にする
                                                        //Debug.Log (LastEnemySpeed);
    }

    //攻撃が当たったらFreezeTime分だけSpeedをゼロにする（動きを止める）
    IEnumerator FreezeSetCoroutine()
    {
        freezeSet = false;
        float LastEnemySpeed = enemyBasic.EnemySpeed;
        enemyBasic.EnemySpeed = 0;
        yield return new WaitForSeconds(FreezeTime);
        enemyBasic.EnemySpeed = LastEnemySpeed;
    }

}


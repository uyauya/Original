using UnityEngine;
using System.Collections;
using UnityEditor;

// PlayerShoot04から発射する特殊弾のShotsssオブジェクト用
public class Bullet04 : MonoBehaviour {

	public GameObject explosion;		// 着弾時のエフェクト
	public float damage;				// 弾の威力
	public float BulletSpeed;			// 弾のスピード
	Enemy enemy;
	PlayerShoot04 Plshoot04;			// 発射元
	public float DestroyTime = 9;		// 発射されてから消滅するまでの時間
	public float XPower;				// 接触時X方向に加える力
	public float YPower;
	public float ZPower;

	/*[CustomEditor(typeof(Bullet04))]
	public class Bullet04 : Editor	// using UnityEditor; を入れておく
	{
		bool folding = false;

		public override void OnInspectorGUI()
		{
			Bullet04 B04 = target as Bullet04;
			B04.BulletSpeed = EditorGUILayout.FloatField( "弾の速さ", B04.BulletSpeed);
			B04.DestroyTime = EditorGUILayout.FloatField( "弾消滅までの時間", B04.DestroyTime);
			B04.XPower = EditorGUILayout.FloatField( "X方向圧力", B04.XPower);
			B04.YPower = EditorGUILayout.FloatField( "Y方向圧力", B04.YPower);
			B04.ZPower = EditorGUILayout.FloatField( "Z方向圧力", B04.ZPower);
		}
	}*/

	void Start () {
		Plshoot04 = GameObject.FindWithTag("Player").GetComponent<PlayerShoot04> ();
		transform.rotation = Plshoot04.transform.rotation;
		//現後一定時間で自動的に消滅させる
		Destroy (gameObject, DestroyTime);
	}	
	void Update () {		
		//弾を前進させる
		transform.position += transform.forward * Time.deltaTime * BulletSpeed;
	}

	// PlayerかEnemyのタグが付いているオブジェクトと接触したら
	void OnTriggerEnter(Collider other){
		if(other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
			//other.gameObject.transform.position = new Vector3 (XPower, YPower, ZPower);
			// 接触したRigidbodyにX、Y、Z方向に指定した力を加える
			other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(XPower, YPower, ZPower));
	}

}


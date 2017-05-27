using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeltConveyor : MonoBehaviour {
	// コンベアに適用。プレイヤにはBeltPlayerを適用する。

	[SerializeField] private float       m_uvSpeed       = 1.0f;
	[SerializeField] private float       m_movePower     = 100.0f;
	[SerializeField] private float       m_speedUpPower  = 100.0f;
	[SerializeField] private float       m_speedUpTime   = 3.0f;
	private Renderer    m_render    = null;
	private List<Rigidbody> m_hitObjects    = new List<Rigidbody>();
	//摩擦0のPhysicMaterialを作成してベルトコンベアにつけて、力が減衰しないようにする
	//Assets→Create→PhysicMaterialで値を全部最小値（0、ミニマム）にする

	void Awake()
	{
		m_render    = GetComponent< Renderer >();
	}

	void Start()
	{
		StartCoroutine( SpeedUp( m_speedUpTime ) ); 
	}

	void Update()
	{
		ScrollUV();
	}

	void OnCollisionEnter( Collision other )
	{
		var body = other.gameObject.GetComponent<Rigidbody>();
		if( body != null )
		{
			Vector3 addPower = transform.forward * m_movePower;
			body.AddForce( addPower, ForceMode.Acceleration );

			m_hitObjects.Add( body );
		}
	}

	void OnCollisionExit( Collision other )
	{
		var body    = other.gameObject.GetComponent<Rigidbody>();
		if( body != null )
		{
			Vector3 addPower = transform.forward * m_movePower;
			body.AddForce( -addPower, ForceMode.Acceleration );

			m_hitObjects.Remove( body );
		}
	}
		
	/// テクスチャのUV値をスクロールさせて、ベルトコンベアの見た目を表現する
	void ScrollUV()
	{
		var material                = m_render.material;
		Vector2 offset              = material.mainTextureOffset;
		offset                     += Vector2.up * m_uvSpeed * Time.deltaTime;
		material.mainTextureOffset  = offset;
	}

	IEnumerator SpeedUp( float i_time )
	{
		while( true )
		{
			// 一定時間ごとにスピードアップ
			yield return new WaitForSeconds( i_time );
			m_movePower += m_speedUpPower;

			// 現在乗っているオブジェクトに対してスピードアップ分力を加える
			Vector3 addPower = transform.forward * m_speedUpPower;
			foreach( var body in m_hitObjects )
			{
				if( body != null )
				{
					body.AddForce( addPower, ForceMode.Acceleration );
				}
			}
		}
	}
}
	

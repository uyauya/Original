using UnityEngine;
using System.Collections;

public class PushBlock : MonoBehaviour {
	
	void Start () {
	}

	void Update () {
	}

	void OnControllerColliderHit(ControllerColliderHit hit){
		
		//物体を押す処理
		if(hit.gameObject.tag == "PushBlock"){
			Rigidbody body = hit.collider.attachedRigidbody;
			
			if(body == null || body.isKinematic){return;}    //rigidBodyがない、もしくは物理演算の影響を受けない設定をされている 
			if(hit.moveDirection.y<-0.3){return;}            //押す力が弱い 
			
			Vector3 pushDir = new Vector3(hit.moveDirection.x,0,hit.moveDirection.z);    //y成分を０に 
			
			float pushPower = 2.0f;
			body.velocity = pushDir * pushPower;    //押す力を加える 
		}
		
	}

}

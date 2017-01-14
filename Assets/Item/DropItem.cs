using UnityEngine;
using System.Collections;

/*public class DropItem : MonoBehaviour {
		
	public enum ItemKind
	{
		Attack,
		Heal,
	};
	public ItemKind kind;

	void OnTriggerEnter(Collider other)
	{
		// Playerが判定.
		if (other.tag == "Player") {
			// アイテム取得.
			PlayerAp playerAp = other.GetComponent<PlayerAp> ();
			playerAp.GetItem (kind);
			// 取得したらアイテムを消す.
			Destroy (gameObject);
		}
	}
	
	void Start () {
		Vector3 velocity = Random.insideUnitSphere * 2.0f + Vector3.up * 8.0f;
		GetComponent<Rigidbody>().velocity = velocity;
	}

	void Update () {
	
	}
}*/

using UnityEngine;
using System.Collections;

// 破壊可能なブロック
public class BreakBlock : MonoBehaviour {

	protected EnemyBasic enemyBasic;

	void Start () {
		enemyBasic = gameObject.GetComponent<EnemyBasic> ();
		enemyBasic.Initialize ();
	}
}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyMoveLogic {
	/// <summary>
	/// 移動する速度を取得する
	/// </summary>
	Vector3 getMoveVelocity();
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyAttackLogic {

	/// <summary>
	/// 攻撃を行うか判断する
	/// </summary>
	bool attackDetermine();

	/// <summary>
	/// 攻撃判定を取得する
	/// </summary>
	GameObject getAttackHitObject();

	/// <summary>
	/// 攻撃の種類を取得する
	/// </summary>
	int getAttackType();

	/// <summary>
	/// 攻撃時に再生するモーションを取得する
	/// </summary>
	string getAttackMotion();
}

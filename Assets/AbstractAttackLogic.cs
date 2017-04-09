using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyLogic.Attack {
	public abstract class AbstractAttackLogic : AbstractLogic, IEnemyAttackLogic {
		public const int CLOSE_RANGE_HIT_TYPE = 1;
		public const int RANGE_HIT_TYPE = 2;

		public GameObject attackHitObject;
		public int attack_type;
		public string attack_motion;

		protected override void Start () {
			base.Start();

			enemy.addAttackLogic(this);
		}

		public abstract bool attackDetermine();
		public abstract GameObject getAttackHitObject();
		public abstract int getAttackType();
		public abstract string getAttackMotion();
	}
}


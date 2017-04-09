using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyLogic.Move {
	public abstract class AbstractMoveLogic : AbstractLogic, IEnemyMoveLogic {
		public float move_speed;

		protected override void Start () {
			base.Start();

			enemy.setMoveLogic(this);
		}

		public abstract Vector3 getMoveVelocity();
	}
}



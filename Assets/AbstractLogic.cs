using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyLogic {
	public abstract class AbstractLogic : MonoBehaviour {

		protected Enemy enemy;

		protected virtual void Start () {
			enemy = this.GetComponent<Enemy>();
		}
	}
}

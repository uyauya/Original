using UnityEngine;
using System.Collections;


public class Bullet01C : Bullet01 {

	protected Bullet01 bullet01;

	void Start () {
		bullet01 = gameObject.GetComponent<Bullet01> ();
		bullet01.Initialize ();
	}	

	void Update () {

	}	

}



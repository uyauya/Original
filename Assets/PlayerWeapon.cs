using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{	
	public float damage = 500;

    // Start is called before the first frame update
    void Start()
    {		
		damage += DataManager.AttackPoint;
    }

    // Update is called once per frame
    void Update()
    {
		
	}

}

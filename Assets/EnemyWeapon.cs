using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
	public static int EWeaponDamage;
	public static float EWeaponImpact;
	public int Damage = 100;
	public float Impact = 2;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		EWeaponDamage = Damage;
		EWeaponImpact = Impact;
    }
}

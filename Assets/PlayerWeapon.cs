using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{	
	public GameObject WeaponAura02;
	public GameObject WeaponAura03;
	public GameObject WeaponAura04;
	public GameObject WeaponAura05;
	public float damage = 500;				
	public bool isWeaponAura02 = false;
	public bool isWeaponAura03 = false;
	public bool isWeaponAura04 = false;
	public bool isWeaponAura05 = false;
	pshoot1 Pshoot1;

	// Start is called before the first frame update
    void Start()
    {
		WeaponAura02 = GameObject.Find ("WeaponAura02");
		WeaponAura03 = GameObject.Find ("WeaponAura03");
		WeaponAura04 = GameObject.Find ("WeaponAura04");
		WeaponAura05 = GameObject.Find ("WeaponAura05");
		Pshoot1 = battleManager.Player.GetComponent<ChangeWeapon>().pshoot1;
    }

    // Update is called once per frame
    void Update()
    {
		if (ChangeWeaponR.weponImage2.enabled == true)
		{
			isWeaponAura02 = true;
			isWeaponAura03 = false;
			isWeaponAura04 = false;
			isWeaponAura05 = false;
			if(WeaponAura02 != null)
			{
				WeaponAura02.SetActive(isWeaponAura02);
				WeaponAura03.SetActive(isWeaponAura03);
				WeaponAura04.SetActive(isWeaponAura04);
				WeaponAura05.SetActive(isWeaponAura05);
			}
		}
		else if (ChangeWeaponR.weponImage3.enabled == true)
		{
			isWeaponAura02 = false;
			isWeaponAura03 = true;
			isWeaponAura04 = false;
			isWeaponAura05 = false;
			if(WeaponAura03 != null)
			{
				WeaponAura02.SetActive(isWeaponAura02);
				WeaponAura03.SetActive(isWeaponAura03);
				WeaponAura04.SetActive(isWeaponAura04);
				WeaponAura05.SetActive(isWeaponAura05);
			}
		}
		else if (ChangeWeaponR.weponImage4.enabled == true)
		{
			isWeaponAura02 = false;
			isWeaponAura03 = false;
			isWeaponAura04 = true;
			isWeaponAura05 = false;
			if(WeaponAura04 != null)
			{
				WeaponAura02.SetActive(isWeaponAura02);
				WeaponAura03.SetActive(isWeaponAura03);
				WeaponAura04.SetActive(isWeaponAura04);
				WeaponAura05.SetActive(isWeaponAura05);
			}
		}
		else if (ChangeWeaponR.weponImage5.enabled == true)
		{
			isWeaponAura02 = false;
			isWeaponAura03 = false;
			isWeaponAura04 = false;
			isWeaponAura05 = true;
			if(WeaponAura05 != null)
			{
				WeaponAura02.SetActive(isWeaponAura02);
				WeaponAura03.SetActive(isWeaponAura03);
				WeaponAura04.SetActive(isWeaponAura04);
				WeaponAura05.SetActive(isWeaponAura05);
			}
		}
		else 
		{
			isWeaponAura02 = false;
			isWeaponAura03 = false;
			isWeaponAura04 = false;
			isWeaponAura05 = false;
		}
    }
}

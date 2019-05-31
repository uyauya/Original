using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquip : MonoBehaviour
{
    public GameObject Wep01;
    public GameObject Wep02;
    public static bool WeaponEquip = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (WeaponEquip == true)
        {
            if ((Wep01 == null) && (Wep02 == null))
            {
                return;
            }
            if (Wep01 != null)
            {
                Wep01.SetActive(true);
            }
            if (Wep02 != null)
            {
                Wep02.SetActive(false);
            }
        }
        //Debug.Log("手");
        if (WeaponEquip == false)
        {
            if ((Wep01 == null) && (Wep02 == null))
            {
                return;
            }
            if (Wep01 != null)
            {
                Wep01.SetActive(false);
            }
            if (Wep02 != null)
            {
                Wep02.SetActive(true);
            }
        //Debug.Log("背中");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTargetRange : MonoBehaviour
{
    public static bool isAttackDesision = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isAttackDesision = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isAttackDesision = false;
        }
    }
}

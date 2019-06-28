using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBeam : MonoBehaviour
{
    public float damage = 10000;				// 弾の威力
    RaycastHit hit;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        
        if (Physics.Raycast(ray, out hit, 10.0f))
            
        {
            if (hit.collider.tag == "Enemy")
            {
                damage = damage;
            }
                
        }
    }
}

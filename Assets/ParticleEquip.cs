using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEquip : MonoBehaviour
{
    public TrailRenderer trailRenderer;

    // Start is called before the first frame update
    void Start()
    {
        this.trailRenderer = GetComponent<TrailRenderer>();
        trailRenderer.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

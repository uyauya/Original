using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEquip : MonoBehaviour
{
    //public TrailRenderer trailRenderer;
    TrailRenderer[] trailRenderers;

    // Start is called before the first frame update
    void Start()
    {
        //this.trailRenderer = GetComponent<TrailRenderer>();
        //trailRenderer.enabled = false;
        trailRenderers = GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trail in trailRenderers)
        {
            trail.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

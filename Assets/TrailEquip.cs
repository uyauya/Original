using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailEquip : MonoBehaviour
{
    TrailRenderer[] trailRenderers;
	public static bool TrailOn = false;

    // Start is called before the first frame update
    void Start()
    {
        trailRenderers = GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trail in trailRenderers)
        {
            trail.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
		if(TrailOn == true)
		{
			foreach (TrailRenderer trail in trailRenderers)
			{
				trail.enabled = true;
			}
		}
    }
}

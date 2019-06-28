using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//trailRendererを付けている場合、表示・非表示選択用
public class TrailEquip : MonoBehaviour
{
	TrailRenderer[] trailRenderers;			//TrailRendererを複数選択できるよう配列にする
	public static bool TrailOn = false;		//TrailRenderer表示・非表示用

    // Start is called before the first frame update
    void Start()
    {
		//子オブジェクトに付いているTrailRendererもまとめてtrailRenderersとして選択する
		trailRenderers = GetComponentsInChildren<TrailRenderer>();
        foreach (TrailRenderer trail in trailRenderers)
        {
			//trailRenderer使用不可
			trail.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
		//TrailOnの状態時、trailRenderer使用可とする
		if(TrailOn == true)
		{
			foreach (TrailRenderer trail in trailRenderers)
			{
				trail.enabled = true;
			}
		}
    }
}

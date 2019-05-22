using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogFlow : MonoBehaviour
{
	public float count;		
	private GameObject fogFlow;
	public static bool isFogFlow = false;
	public int CountFogFlow = 5;
	public int CountNothing = 10;
	public int CountReset = 12;
	private Vector3 offset;
	public BattleManager battleManager;

	// Start is called before the first frame update
    void Start()
    {
		fogFlow = GameObject.Find ("FogFlow");
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		offset = transform.position - battleManager.Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		count += Time.deltaTime;
		// CountAfternoonに設定しているの時間を過ぎたらLightMorningとLightAfternoonを消灯
		if (count > CountFogFlow)
		{
			fogFlow.SetActive(true);
			isFogFlow = true;
		}
		if (count > CountNothing)
		{
			fogFlow.SetActive(false);
			isFogFlow = false;
		}
		if (count > CountReset)
		{
			count = 0;
		}
    }

	void LateUpdate ()
	{
		transform.position = battleManager.Player.transform.position + offset;
	}
}

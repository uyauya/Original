using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cyclone : MonoBehaviour
{
	public float count;		
	private GameObject cyclone;
	public static bool isCyclone = false;
	public int CountCyclone = 5;
	public int CountNothing = 10;
	public int CountReset = 12;
	private Vector3 offset;
	public BattleManager battleManager;

	// Start is called before the first frame update
    void Start()
    {
		cyclone = GameObject.Find ("Cyclone");
		battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
		offset = transform.position - battleManager.Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
		count += Time.deltaTime;
		// CountAfternoonに設定しているの時間を過ぎたらLightMorningとLightAfternoonを消灯
		if (count > CountCyclone)
		{
			cyclone.SetActive(true);
			isCyclone = true;
		}
		if (count > CountNothing)
		{
			cyclone.SetActive(false);
			isCyclone = false;
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

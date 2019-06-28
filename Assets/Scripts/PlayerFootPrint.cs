using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//足跡をつける
public class PlayerFootPrint : MonoBehaviour
{
	public GameObject FootPrintPrefab;
	float time = 0;
	public float PrintTime = 0.35f;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > PrintTime)
		{
			this.time = 0;
			Instantiate (FootPrintPrefab, transform.position, transform.rotation);
		}
	}
}

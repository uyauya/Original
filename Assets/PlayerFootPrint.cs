using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootPrint : MonoBehaviour
{
	public GameObject FootPrintPrefab;
	float time = 0;

	// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
	void Update()
	{
		this.time += Time.deltaTime;
		if (this.time > 0.35f)
		{
			this.time = 0;
			Instantiate (FootPrintPrefab, transform.position, transform.rotation);
		}
	}
}

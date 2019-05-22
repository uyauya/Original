using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
		StartCoroutine (AdvancedRotate ());
    }

    // Update is called once per frame
    void Update()
    {
		StartCoroutine (RecessionRotate ());
    }

	IEnumerator AdvancedRotate() 
	{
		for (int i = 0; i < 10; i++) 
		{
			transform.Rotate (new Vector3 (30, 0, 0));
			yield return null;
		}
	}

	IEnumerator RecessionRotate() 
	{
		for (int i = 0; i < 10; i++) 
		{
			transform.Rotate (new Vector3 (-5, 0, 0));
			yield return null;
		}
	}
}

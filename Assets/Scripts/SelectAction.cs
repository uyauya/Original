using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAction : MonoBehaviour
{
    
	private Animator animator;
    public ButtonController buttoncontroller;

	// Start is called before the first frame update
    void Start()
    {
		animator = GetComponent<Animator>();			// Animatorを使う場合は設定する
    }

    // Update is called once per frame
    void Update()
    {
        if (buttoncontroller != null)
        {
            if (buttoncontroller.SelectAction == true)
            {
                animator.SetBool("Select", true);
                //Debug.Log("YES");
            }
            else
            {
                animator.SetBool("Select", false);
            }
        }
    }
}

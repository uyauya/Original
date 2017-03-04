using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

	public void Onclick_Select()
	{
		SceneManager.LoadScene("Select");
		Debug.Log("START");
	}
}
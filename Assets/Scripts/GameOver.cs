using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

// ゲームオーバー、コンティニュー画面
public class GameOver : MonoBehaviour 
{
	public Text TextCountDown;
	public void Onclick_Select ()
	{
		DataManager.Continue = false;
		DataManager.FarstLevel = true;
		TextCountDown.gameObject.SetActive (false);
		// ボタンを押してセレクト画面に移行
		SoundManager00.Instance.Play(0);	//(1)はElmentの数
		Invoke("LoadScene",1.3f);
	}
	// Use this for initialization
	void Start () {
		TextCountDown.text = "";
		StartCoroutine (CountdownCoroutine ());
		Debug.Log (CountdownCoroutine());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void LoadScene() {
		SceneManager.LoadScene(UserParam.instanse.SceneName);
	}

	IEnumerator CountdownCoroutine()
	{
		TextCountDown.gameObject.SetActive (true);

		TextCountDown.text = "9";
		SoundManager00.Instance.Play(1,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "8";
		SoundManager00.Instance.Play(2,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "7";
		SoundManager00.Instance.Play(3,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "6";
		SoundManager00.Instance.Play(4,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "5";
		SoundManager00.Instance.Play(5,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "4";
		SoundManager00.Instance.Play(6,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "3";
		SoundManager00.Instance.Play(7,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "2";
		SoundManager00.Instance.Play(8,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "1";
		SoundManager00.Instance.Play(9,gameObject);
		yield return new WaitForSeconds (1.0f);

		TextCountDown.text = "げ～むお～ば～";
		SoundManager00.Instance.Play(10,gameObject);
		yield return new WaitForSeconds (1.0f);

		//TextCountDown.text = "";
		//TextCountDown.gameObject.SetActive (false);
	}

}

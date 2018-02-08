using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

public class Continue : MonoBehaviour {

	private AudioSource[] audioSources;
	private Animator animator;
	public Text blinkText;					//点滅させる
	public void Onclick_Continue ()

	{
		DataManager.Continue = true;
		DataManager.FarstLevel = false;
		// ボタンを押してセレクト画面に移行
		SoundManager00.Instance.Play(2);	//(1)はElmentの数
		// 1.3秒後にLoadScene起動
		Invoke("LoadScene",1.3f);
	}

	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
		animator = GetComponent<Animator> ();
		SoundManager00.Instance.Play(0);
	}

	void Update () {
		// 
		/*if (Input.GetMouseButtonDown(0)){
			animator.SetBool ("Continue", true);
		} else {
			animator.SetBool ("Continue", false);
		}*/
	}

	void LoadScene() {
		//SceneManager.LoadScene("STAGE02BOSS");
		UserParam userParam = DataManager.userParam;
		/*if (userParam != null) {
			if (userParam.SceneName != null && userParam.SceneName != string.Empty) {
				Debug.Log (string.Format ("LoadScene{0}", userParam.SceneName));
				SceneManager.LoadScene (userParam.SceneName);
			}
		} else
			{
				SceneManager.LoadScene ("Select");
			}

		}*/
		if (userParam != null) {
			SceneManager.LoadScene (StageManager.Instance.StageName [userParam.StageNo]);
		}
	}
}

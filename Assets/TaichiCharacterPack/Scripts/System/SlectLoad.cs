using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;		//シーン遷移する時は追加

//キャラクタ
public class SlectLoad : MonoBehaviour {
	private AudioSource[] audioSources;
	public float WaitTime = 1.8f;		//ステージ移行までの待ち時間

	// Use this for initialization
	void Start () {
		audioSources = gameObject.GetComponents<AudioSource> ();
		SoundManager00.Instance.Play(0);
		//LoadTime起動処理（下記参照）
		StartCoroutine ("LoadTime");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private IEnumerator LoadTime (){	//WaitTime後にSTAGE01シーンへ移行
		yield return new WaitForSeconds (WaitTime);
		SceneManager.LoadScene ("STAGE01");
	}
	
}

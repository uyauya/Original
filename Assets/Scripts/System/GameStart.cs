
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;			//シーン遷移する時は追加
using UnityEngine.UI;						// UIを使う時は追加する

// ゲームスタート時DataManagerにプレイヤレベル1のデータを入れる
public class GameStart : MonoBehaviour {
	private AudioSource[] audioSources;
	private Animator animator;
	public Text blinkText;					//点滅させる
    public Button start; //スタートボタン

    public void Onclick_Select ()
	{
		animator.SetBool("START", true);    
		DataManager.Continue = false;
		DataManager.FarstLevel = true;		//レベル1データ代入用
		// ボタンを押してセレクト画面に移行
		SoundManager00.Instance.Play(1);	//(1)はElmentの数
		Invoke("LoadScene",1.3f);
	}
		
		
	// ゲームスタート時DataManagerにプレイヤレベル1のデータを入れる
	void Start () {
		PlayerLevel playerLevel;
		DataManager.Score = 0;
		DataManager.Level = 1;
		DataManager.AttackPoint = PlayerLevel.userParamList[0].AttackPoint;
		DataManager.BoostPointMax = PlayerLevel.userParamList[0].boostPointMax;
		DataManager.ArmorPointMax = PlayerLevel.userParamList[0].armorPointMax;
		audioSources = gameObject.GetComponents<AudioSource> ();
		animator = GetComponent<Animator> ();
		SoundManager00.Instance.Play(0);
	}

	void Update()
	{
		if (Input.GetButtonDown ("Fire1")) 
		{
            Debug.Log("Debug");
            start.onClick.Invoke();

            /*ExecuteEvents.Execute
			(
				target      : this.gameObject,
				eventData   : new PointerEventData( EventSystem.current ),
				functor     : ExecuteEvents.pointerClickHandler
			);*/

        }
	}

	void LoadScene() {
		// Selectシーンへ移行
		SceneManager.LoadScene("Select");
	}

}
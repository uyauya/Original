using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// ポーズ画面、コンフィグ画面呼び出し
public class Pause : MonoBehaviour
{
	public bool isPause;				//ポーズ画面かどうか
	public bool isConfig;				//コンフィグ画面かどうか
	//public bool isTutorial
	//public bool isStatus;
	//public bool isSound;
	public GameObject Tutorial;
	public GameObject Setting;
	public AudioSource audioSource;
	public BGMManager bgmManager;

	void Start()
	{
		isPause = false;
		isConfig = false;
		Tutorial.SetActive(false);
		Setting.SetActive(false);
		bgmManager = GameObject.Find ("BGMManager").GetComponent<BGMManager> ();
	}

	void Update ()
	{
		// ESCキー押した時にコンフィグ画面になっていなければ
		if (Input.GetKeyDown (KeyCode.Escape)&& !isConfig) {
			//ポーズ中だったら何もしない
			if (isPause) {
				isPause = false;
				Tutorial.SetActive (false);
				Time.timeScale = 1f;
				bgmManager.Play ();
			//ポーズ画面でなかったらポーズ画面にする
			} else {
				isPause = true;
				Tutorial.SetActive (true);
				Time.timeScale = 0;
				bgmManager.Pause ();		//一時停止（完全停止する場合はaudioSource.Stop();
			}
		}
		// ポーズ中に方向キー上押した時コンフィグ画面に移行
		if(Input.GetKey(KeyCode.UpArrow)&& isPause)
		//if (Input.GetKeyDown (KeyCode.LeftShift) && isPause ) 
		{
			//コンフィグ画面だったら何もしない
			if (isConfig) {
				isConfig = false;
				Setting.SetActive (false);
			//コンフィグ画面でなかったらコンフィグ画面にする
			} else {
				isConfig = true;
				Setting.SetActive (true);
			}
		}
		// ポーズ中に方向キー下押した時チュートリアル画面に移行
		/*if(Input.GetKey(KeyCode.DownArrow)&& isPause)
		{
			//チュートリアル画面だったら何もしない
			if (isTutorial) {
				isTutorial = false;
				Setting.SetActive (false);
				//チュートリアル画面でなかったらコンフィグ画面にする
			} else {
				isTutorial = true;
				Setting.SetActive (true);
			}
		}
		// ポーズ中に方向キー左押した時ステータス画面に移行
		if(Input.GetKey(KeyCode.LeftArrow)&& isPause)
		{
			//ステータス画面だったら何もしない
			if (isStatus) {
				isStatus = false;
				Setting.SetActive (false);
				//ステータス画面でなかったらコンフィグ画面にする
			} else {
				isStatus = true;
				Setting.SetActive (true);
			}
		}
		// ポーズ中に方向キー右押した時サウンド画面に移行
		if(Input.GetKey(KeyCode.RightArrow)&& isPause)
		{
			//サウンド画面だったら何もしない
			if (isSound) {
				isSound = false;
				Setting.SetActive (false);
				//サウンド画面でなかったらコンフィグ画面にする
			} else {
				isSound = true;
				Setting.SetActive (true);
			}
		}*/
	}
	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;				// UI使用時は追加
using UnityEngine.SceneManagement;	// ステージ移行したい場合は追加する

// シーン移行用
public class SelectEvent : MonoBehaviour {
	private AudioSource[] audioSources;
	//public GameObject SelectKohaku;
	//public GameObject SelectYuko;
	//public GameObject SelectMisaki;

	//public void OnSelectKohaku(GameObject target){
	/*public void OnSelectKohaku() {
		SelectKohaku.SetActive(true);
	}

	//public void OnDeselectKohaku(GameObject target){
	public void OnDeselectKohaku() {
		SelectKohaku.SetActive(false);
	}

	//public void OnSelectYuko(GameObject target){
	public void OnSelectYuko(){
		SelectYuko.SetActive(true);
	}

	//public void OnDeselectYuko(GameObject target){
	public void OnDeselectYuko(){
		SelectYuko.SetActive(false);
	}

	//public void OnSelectMisaki(GameObject target){
	public void OnSelectMisaki(){
		SelectMisaki.SetActive(true);
	}

	//public void OnDeselectMisaki(GameObject target){
	public void OnDeselectMisaki(){
		SelectMisaki.SetActive(false);
	}*/

    public void Onclick_Kohaku() 
	{
		DataManager.PlayerNo = 0;			// DataManagerからPlayerNoを取得
        SoundManager00.Instance.Play(3);	// SoundManager00から3の音を鳴らして
		Invoke("LoadScene1",1.3f);			// 1.3秒後にLoadScene1（kohakuシーンに移行）※下記参照
	}
		

	public void Onclick_Yuko() 
	{
		DataManager.PlayerNo = 1;
        SoundManager00.Instance.Play(4);
		Invoke("LoadScene2",1.3f);
	}

	public void Onclick_Misaki() 
	{
		DataManager.PlayerNo = 2;
		SoundManager00.Instance.Play(5);
		Invoke("LoadScene3",1.3f);
	}

	public void Onclick_End ()
	{
		// ボタンを押してスタート画面に移行
		SceneManager.LoadScene("Start");
	}

	public void Onclick_Save ()
	{
		// ボタンを押してセーブ画面に移行
		SceneManager.LoadScene("Save");
	}

	public void Onclick_Load ()
	{
		// ボタンを押してロード画面に移行
		SceneManager.LoadScene("Save");
	}

	void Start () {
		//シーン起動直後にSoundManager00の指定音を鳴らす
		SoundManager00.Instance.Play(0);
		SoundManager00.Instance.Play(1);
		SoundManager00.Instance.Play(2);
		//シーン起動0.9秒後ににSoundManager00の指定音を鳴らす
		SoundManager00.Instance.PlayDelayed (6, 0.9f, gameObject);
	}

    /*void Update()
    {
        

            
        {
            //Kohaku.onClick.Invoke();
            ExecuteEvents.Execute
            (
                target: this.gameObject,
                eventData: new PointerEventData(EventSystem.current),
                functor: ExecuteEvents.pointerClickHandler
            );
        }
    }*/

    void LoadScene1() {
		//Kohakuシーンに移行
		SceneManager.LoadScene("Kohaku");
	}
	void LoadScene2() {
		SceneManager.LoadScene("Yuko");
	}
	void LoadScene3() {
		SceneManager.LoadScene("Misaki");
	}
}

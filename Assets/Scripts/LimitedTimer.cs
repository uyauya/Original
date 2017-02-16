using UnityEngine; 
using UnityEngine.UI; 
using System.Collections; 

public class LimitedTimer : MonoBehaviour { 
	private float time = 0.0f; 
	
	void Start () { 
		//初期値を表示 
		//float型からint型へCastし、String型に変換して表示 
		int minute = (int)time / 60;
		int second = (int)time % 60;
		GetComponent<Text> ().text = minute.ToString("D2") + "分" + second.ToString("D2") + "秒";
		//GetComponent<Text>().text = ((int)time).ToString(); 
	} 

	void Update (){ 
		int minute = (int)time / 60;
		int second = (int)time % 60;
		//1秒に1ずつ減らしていく 
		//time -= Time.deltaTime;
		//1秒に1ずつ減らしていく
		time += Time.deltaTime;
		//マイナスは表示しない 
		if (time < 0) time = 0; 
		GetComponent<Text> ().text = minute.ToString("D2") + "分" + second.ToString("D2") + "秒";
		//GetComponent<Text> ().text = ((int)time).ToString (); 
	} 
} 
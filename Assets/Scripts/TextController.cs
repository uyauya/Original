using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class TextController : MonoBehaviour
{
	const float TEXT_SPEED = 0.5F;
	const float TEXT_SPEED_STRING = 0.05F;
	const float COMPLETE_LINE_DELAY = 0.3F;

	[SerializeField] Text lineText;					// 文字表示Text
	[SerializeField] string[] scenarios;			// 会話内容

	float textSpeed = 0;			       			// 表示速度
	float completeLineDelay = COMPLETE_LINE_DELAY;	// 表示し終えた後の待ち時間
	int currentLine = 0;			       			// 表示している行数
	string currentText = string.Empty;				// 表示している文字
	bool isCompleteLine = false;					// １文が全部表示されたか？

	void Start()
	{
		Show ();
	}

	// 会話シーン表示
	void Show()
	{
		Initialize ();
		StartCoroutine (ScenarioCoroutine ());
	}

	// 初期化
	void Initialize()
	{
		isCompleteLine = false;
		lineText.text = "";
		currentText = scenarios [currentLine];

		textSpeed = TEXT_SPEED + (currentText.Length * TEXT_SPEED_STRING);

		LineUpdate ();
	}

	// 会話シーン更新
	IEnumerator ScenarioCoroutine()
	{
		while (true) {
			yield return null;

			// 次の内容へ
			if (isCompleteLine && Input.GetMouseButton (0)) {
				yield return new WaitForSeconds (completeLineDelay);

				if (currentLine > scenarios.Length - 1) {
					ScenarioEnd ();
					yield break;
				}
				Initialize ();
			}

			// 表示中にボタンが押されたら全部表示
			else if (!isCompleteLine && Input.GetMouseButton (0)) {
				iTween.Stop ();
				TextUpdate (currentText.Length); // 全部表示
				TextEnd ();
				yield return new WaitForSeconds (completeLineDelay);
			}
		}
	}

	// 文字を少しずつ表示
	void LineUpdate()
	{
		iTween.ValueTo (this.gameObject, iTween.Hash (
			"from", 0,
			"to", currentText.Length, 
			"time", textSpeed, 
			"onupdate", "TextUpdate",
			"oncompletetarget", this.gameObject,
			"oncomplete", "TextEnd"
		));
	}

	// 表示文字更新
	void TextUpdate(int lineCount)
	{
		lineText.text = currentText.Substring (0, lineCount);
	}

	// 表示完了
	void TextEnd()
	{
		Debug.Log ("表示完了");
		isCompleteLine = true;
		currentLine++;
	}

	// 会話終了
	void ScenarioEnd()
	{
		Debug.Log ("会話終了");
	}
}

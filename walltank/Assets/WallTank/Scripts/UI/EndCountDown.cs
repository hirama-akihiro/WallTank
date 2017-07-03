using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndCountDown : SingletonMonoBehavior<EndCountDown> {


	public bool isCount = true;
	private float countDown;
	private float undeltaTime = 0.0f;
	private float endCount = 0.0f;
	public Text text;
	private int prevCount = 5;

	// Use this for initialization
	void Start () {
		countDown = RuleManager.I.gameTime + 3;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCount)
		{
			text.text = "";
			return;
		}

		undeltaTime += Time.unscaledDeltaTime;
		float diff = countDown - undeltaTime;
		int count = (int)RuleManager.I.gameTime;

		if(prevCount != count && count <= 6)
		{
			prevCount = count;
			if (count != 0) { AudioManager.I.PlayAudio("se_maoudamashii_system27"); }
			if(count == 0) { AudioManager.I.PlayAudio("se_maoudamashii_se_whistle01"); }
		}
		
		if (count== 0 || count == -1)
		{
			endCount += Time.unscaledDeltaTime;
			Time.timeScale = 0;
			text.text = "FINISH!!";
			text.color = new Color(1, 1, 1, 1);
		}
		else if(count > 5)
		{
			text.color = new Color(0, 0, 0, 0);
		}
		else
		{
			text.text = count.ToString();
			text.color = new Color(1, 1, 1, Mathf.Abs(diff - count));
		}

		if (endCount > 2)
		{
			isCount = false;
			RuleManager.I.ActivateResultDialog();
			gameObject.SetActive(false);
		}
	}
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartCountDown : SingletonMonoBehavior<StartCountDown> {

	public bool isCount = true;
	private float countDown = 5f;
	private float undeltaTime = 0.0f;
	private int prevCount = 3;
	public Text text;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCount) { return; }

		undeltaTime += Time.unscaledDeltaTime;
		float diff = countDown - undeltaTime;
		int count = (int)diff;

		if(prevCount != count)
		{
			prevCount = count;
			if (count >= 1 && count <= 3) { AudioManager.I.PlayAudio("se_maoudamashii_system27"); }
			if (count == 0) { AudioManager.I.PlayAudio("se_maoudamashii_se_whistle01"); }
		}

		if (count == 0)
		{
			Time.timeScale = 1;
			text.text = "START!!";
			text.color = new Color(1, 1f, 00, Mathf.Abs(diff - count));
		}
		else if(count == 1 || count == 2 || count == 3)
		{
			text.text = count.ToString();
			text.color = new Color(1, 1f, 00, Mathf.Abs(diff - count));
		}

		if (countDown - undeltaTime < 0.5)
		{
			AudioManager.I.StopAudio();
			int index = Random.Range(0, 4);
			switch (index)
			{
				case 0:
					AudioManager.I.PlayAudio("loop_47", 0.5f, AudioManager.PlayMode.Repeat);
					break;
				case 1:
					AudioManager.I.PlayAudio("loop_89", 0.5f, AudioManager.PlayMode.Repeat);
					break;
				case 2:
					AudioManager.I.PlayAudio("loop_131", 0.5f, AudioManager.PlayMode.Repeat);
					break;
				case 3:
					AudioManager.I.PlayAudio("loop_144", 0.5f, AudioManager.PlayMode.Repeat);
					break;
			}
			gameObject.SetActive(false);
		}
	}
}

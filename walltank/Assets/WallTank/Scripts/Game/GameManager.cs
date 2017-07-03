using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public GameObject pausePanel;
	private int startTime;

	// Use this for initialization
	void Start () {
		AudioManager.I.StopAudio();
		startTime = (int)RuleManager.I.gameTime;
	}
	
	// Update is called once per frame
	void Update () {
		if (!RuleManager.I.isFinish && Input.GetKeyDown(KeyCode.Space) || GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any))
		{
			if(startTime == RuleManager.I.gameTime) { return; }
			if((int)RuleManager.I.gameTime == 0) { return; }

			Time.timeScale = 0;
			// カウントダウンの停止
			EndCountDown.I.isCount = false;
			StartCountDown.I.isCount = false;
			pausePanel.SetActive(true);
		}
	}
}

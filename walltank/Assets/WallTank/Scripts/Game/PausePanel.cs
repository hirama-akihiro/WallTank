using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space)||
			GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any))
		{
			EndCountDown.I.isCount = true;
			StartCountDown.I.isCount = true;

			Time.timeScale = 1;
			this.gameObject.SetActive(false);
		}

		if(GamePad.GetButtonDown(GamePad.Button.LeftShoulder, GamePad.Index.Any) &&
			GamePad.GetButtonDown(GamePad.Button.RightShoulder, GamePad.Index.Any))
		{
			Time.timeScale = 1;
			AudioManager.I.StopAudio();
			AudioManager.I.PlayAudio("loop_111", 1.0f, AudioManager.PlayMode.Repeat);
			SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Select);
			SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Select);
		}
	}
}

using UnityEngine;
using System.Collections;

public class TitleManager: MonoBehaviour {

	// Use this for initialization
	void Start () {
		AudioManager.I.PlayAudio("loop_111", 0.5f, AudioManager.PlayMode.Repeat);
	}

	// Update is called once per frame
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space) || GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any))
		{
			AudioManager.I.PlayAudio("se_maoudamashii_system37");
			SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Select);
		}
	}

	public void OnClickButotn()
	{
		// シーン遷移
		SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Select);
	}
}

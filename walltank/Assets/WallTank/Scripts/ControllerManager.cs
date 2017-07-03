using UnityEngine;
using System.Collections;

public class ControllerManager : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) ||
			GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any) ||
			GamePad.GetButtonDown(GamePad.Button.A, GamePad.Index.Any) ||
			GamePad.GetButtonDown(GamePad.Button.B, GamePad.Index.Any)
			)
        {
            AudioManager.I.PlayAudio("se_maoudamashii_system37");
            SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Game);
        }
    }

    public void OnClickButotn()
    {
        // シーン遷移
        SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Game);
    }
}

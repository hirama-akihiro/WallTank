using UnityEngine;
using System.Collections;

/// <summary>
/// Sceneの遷移を管理するクラス
/// </summary>
public class SceneStateManager : SingletonMonoBehavior<SceneStateManager> {

	#region 各SceneのPrefab変数
	public GameObject scene;
	private GameObject titleScene;
	private GameObject selectScene;
    private GameObject controllerScene;
    private GameObject gamePictureScene;
	private GameObject gameScene;
	private GameObject resultScene;
	#endregion

	public enum SceneState { Title, Select, Controller, GamePicture, Game, Result, Null }
	public SceneState startState = SceneState.Title;

	// Use this for initialization
	void Start () {
		// 各SceneのPrefab読込
		titleScene = Resources.Load("Prefabs/TitleManager") as GameObject;
		selectScene = Resources.Load("Prefabs/SelectManager") as GameObject;
        controllerScene = Resources.Load("Prefabs/ControllerManager") as GameObject;
        gamePictureScene = Resources.Load("Prefabs/GamePictureManager") as GameObject;
		gameScene = Resources.Load("Prefabs/GameManager") as GameObject;
		resultScene = Resources.Load("Prefabs/ResultManager") as GameObject;

		// ゲームスタート時のScene設定
		scene = Instantiate(titleScene);
	}
	
	// Update is called once per frame
	void Update () {
		// ゲーム終了
		if (Input.GetKeyDown(KeyCode.Escape)) { Application.Quit(); }
	}

	/// <summary>
	/// Scene切り替えメソッド
	/// </summary>
	/// <param name="sState"></param>
	public void ChangeScene(SceneState _sceneState)
	{
		GameObject.Destroy(scene);
		scene = null;
		if (_sceneState == SceneState.Title)
		{
			scene = Instantiate(titleScene);
			startState = SceneState.Title;
		}
		else if(_sceneState == SceneState.Select)
		{
			// シーン遷移SEの再生:後々再生ヶ所を変更
			//AudioManager.I.PlayAudio("se_maoudamashii_system37");
			scene = Instantiate(selectScene);
			startState = SceneState.Select;
		}
        else if (_sceneState == SceneState.Controller)
        {
            scene = Instantiate(controllerScene);
            startState = SceneState.Controller;
        }
        else if (_sceneState == SceneState.GamePicture)
        {
            scene = Instantiate(gamePictureScene);
            startState = SceneState.GamePicture;
        }
		else if (_sceneState == SceneState.Game)
		{
			// シーン遷移SEの再生:後々再生ヶ所を変更
			//AudioManager.I.PlayAudio("se_maoudamashii_system37");
			//AudioManager.I.PlayAudio("game_maoudamashii_5_town26");

			scene = Instantiate(gameScene);
			startState = SceneState.Game;
		}
		else if (_sceneState == SceneState.Result)
		{
			scene = Instantiate(resultScene);
			startState = SceneState.Result;
		}
	}
}

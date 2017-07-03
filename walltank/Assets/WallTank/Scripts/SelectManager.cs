using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SelectManager : MonoBehaviour{

	public GameObject numberOfPlayerTextObject;
	public Image stageImage;

	public Sprite plasmaFactoryImage;
	public Sprite snowLandImage;
	public Sprite fieldOfBattleImage;

	// Use this for initialization
	void Start () {
		// プレイヤー人数の更新
		numberOfPlayerTextObject.GetComponent<Text>().text = GameInfoManager.I.numberOfPlayer.ToString() + "人";

		// ステージ画像の更新
		switch (GameInfoManager.I.stageType)
		{
			case GameInfoManager.StageType.PlasmaFactory:
				stageImage.sprite = plasmaFactoryImage;
				break;
			case GameInfoManager.StageType.SnowLand:
				stageImage.sprite = snowLandImage;
				break;
			case GameInfoManager.StageType.FieldOfBattle:
				stageImage.sprite = fieldOfBattleImage;
				break;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void OnClickDecreasePlayerButton()
	{
		AudioManager.I.PlayAudio("se_maoudamashii_system37");
		switch (GameInfoManager.I.numberOfPlayer)
		{
			case 2:
			case 3:
			case 4:
				GameInfoManager.I.numberOfPlayer--;
				numberOfPlayerTextObject.GetComponent<Text>().text = GameInfoManager.I.numberOfPlayer.ToString() + "人";
				break;
		}
	}

	public void OnClickIncreasePlayerButton()
	{
		AudioManager.I.PlayAudio("se_maoudamashii_system37");
		switch (GameInfoManager.I.numberOfPlayer)
		{
			case 1:
			case 2:
			case 3:
				GameInfoManager.I.numberOfPlayer++;
				numberOfPlayerTextObject.GetComponent<Text>().text = GameInfoManager.I.numberOfPlayer.ToString() + "人";
				break;
		}
	}

	public void OnClickIncreaseStageButton()
	{
		AudioManager.I.PlayAudio("se_maoudamashii_system37");
		switch(GameInfoManager.I.stageType)
		{
			case GameInfoManager.StageType.PlasmaFactory:
				GameInfoManager.I.stageType = GameInfoManager.StageType.SnowLand;
				stageImage.sprite = snowLandImage;
				break;
			case GameInfoManager.StageType.SnowLand:
				GameInfoManager.I.stageType = GameInfoManager.StageType.FieldOfBattle;
				stageImage.sprite = fieldOfBattleImage;
				break;
			case GameInfoManager.StageType.FieldOfBattle:
				GameInfoManager.I.stageType = GameInfoManager.StageType.PlasmaFactory;
				stageImage.sprite = plasmaFactoryImage;
				break;
		}
	}

	public void OnClickDecreaseStageButton()
	{
		AudioManager.I.PlayAudio("se_maoudamashii_system37");
		switch (GameInfoManager.I.stageType)
		{
			case GameInfoManager.StageType.PlasmaFactory:
				GameInfoManager.I.stageType = GameInfoManager.StageType.FieldOfBattle;
				stageImage.sprite = fieldOfBattleImage;
				break;
			case GameInfoManager.StageType.SnowLand:
				GameInfoManager.I.stageType = GameInfoManager.StageType.PlasmaFactory;
				stageImage.sprite = plasmaFactoryImage;
				break;
			case GameInfoManager.StageType.FieldOfBattle:
				GameInfoManager.I.stageType = GameInfoManager.StageType.SnowLand;
				stageImage.sprite = snowLandImage;
				break;
		}
	}

	public void OnClickGameStartButton()
	{
		// ゲームシーンへ遷移
		AudioManager.I.PlayAudio("se_maoudamashii_system37");
		//SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Game);

		SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.GamePicture);
	}
}

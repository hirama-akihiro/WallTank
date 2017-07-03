using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Collections;

/// <summary>
/// ルールを管理するクラス
/// 現状：UI関連のクラスも混ざってしまっている・・・。
/// </summary>
public class RuleManager : SingletonMonoBehavior<RuleManager> {

	/// <summary>
	/// ハートの最大数
	/// </summary>
	public readonly static int MAX_HEART_COUNT = 6;

	/// <summary>
	/// 1プレイのゲーム時間
	/// </summary>
	public float gameTime = 40.0f;

	#region サブ攻撃関連
	private Dictionary<SubWeaponType, Sprite> subWeaponSprites;
	public List<GameObject> subWeaponImageObjects;
	private List<Image> subWeaponImages;
	#endregion

	#region HP関連
	public GameObject timeTextObject;
	private Text timeText;
	public List<GameObject> playerHpImageObjects;
	private List<Image> playerHpImages;
	#endregion
	public GameObject resultPanelObject;
	public GameObject resultTextObject;

	public bool isFinish = false;
	private int maxIndex;
	public GameObject objUI;


	// Use this for initialization
	void Start()
	{
		subWeaponSprites = new Dictionary<SubWeaponType, Sprite>();
		subWeaponSprites.Add(SubWeaponType.None, Resources.Load<Sprite>("Sprites/none"));
		subWeaponSprites.Add(SubWeaponType.CircleBomb, Resources.Load<Sprite>("Sprites/circlebomb"));
		subWeaponSprites.Add(SubWeaponType.CrossBomb, Resources.Load<Sprite>("Sprites/crossbomb") as Sprite);
		subWeaponSprites.Add(SubWeaponType.Missle, Resources.Load<Sprite>("Sprites/missile") as Sprite);
		subWeaponSprites.Add(SubWeaponType.Tornado, Resources.Load<Sprite>("Sprites/tornado") as Sprite);
		subWeaponSprites.Add(SubWeaponType.Shield, Resources.Load<Sprite>("Sprites/shield") as Sprite);
		subWeaponImages = new List<Image>();
		for (int i = 0; i < subWeaponImageObjects.Count; ++i)
		{
			subWeaponImages.Add(subWeaponImageObjects[i].GetComponent<Image>());
		}

		playerHpImages = new List<Image>();
		timeText = timeTextObject.GetComponent<Text>();
		for (int i = 0; i < playerHpImageObjects.Count; ++i)
		{
			playerHpImages.Add(playerHpImageObjects[i].GetComponent<Image>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (isFinish && (Input.GetKeyDown(KeyCode.Space) || GamePad.GetButtonDown(GamePad.Button.Start, GamePad.Index.Any)))
		{
			// リザルトダイアログ表示後にセレクトシーンへ遷移
			Time.timeScale = 1;
			AudioManager.I.StopAudio();
			AudioManager.I.PlayAudio("loop_111", 1.0f, AudioManager.PlayMode.Repeat);
			SceneStateManager.I.ChangeScene(SceneStateManager.SceneState.Select);
		}
		if (isFinish) { return; }
		gameTime -= Time.deltaTime;
		IsCheckGameFinish();
	}

	public void IsCheckGameFinish()
	{
		// プレイ時間が終了したかどうかの判定
		timeText.text = ((int)gameTime).ToString();
		// if (gameTime < 0) { ActivateResultDialog(); }

		// プレイヤーのHPが1体以外全員0になっているか
		// Todo:GetComponentは１回しか呼ばないようにする！！・・・後で
		int index = 0;
		int hp0Count = 0;
		for(int i = 0; i < TankManager.I.TankObjects.Count;++i)
		{
			Tank tank = TankManager.I.TankObjects[i].GetComponent<Tank>();
			double ratio = tank.myStatus.ratioHP * MAX_HEART_COUNT;

			// HP関連のUI更新
			for (int j = 0; j < MAX_HEART_COUNT; j++)
			{
				if (j + 1 <= ratio)
				{
					playerHpImages[index].fillAmount = 1;
				}
				else if (ratio <= j)
				{
					playerHpImages[index].fillAmount = 0;
				}
				else if (j <= ratio && ratio <= j + 1)
				{
					playerHpImages[index].fillAmount = (float)ratio % 1;
				}

				index++;
			}
	
			if (tank.myStatus.ratioHP <= 0) { hp0Count++; }

			// スキル関連のUI更新
			subWeaponImages[i].sprite = subWeaponSprites[tank.subWeaponType];
		}

		if(hp0Count >= 3) { ActivateResultDialog(); }
	}

	public void ActivateResultDialog()
	{
		EndCountDown.I.isCount = false;
		Time.timeScale = 0;

		isFinish = true;
		float maxHp = 0;
		for (int i = 0; i < TankManager.I.TankObjects.Count; ++i)
		{
			Tank tank = TankManager.I.TankObjects[i].GetComponent<Tank>();
			if (maxHp < tank.myStatus.ratioHP)
			{
				maxHp = tank.myStatus.ratioHP;
				maxIndex = i;
			}
		}

		// 1位のタンクにカメラがズームアップする処理
		StartCoroutine(ZoomUpCamera(TankManager.I.TankObjects[maxIndex].transform.position));
	}

	private IEnumerator ZoomUpCamera(Vector3 firstPos)
	{
		GameObject mainCamera = GameObject.Find("MainCamera");

		float zoomTime = 0.5f;
		Vector3 startPos = mainCamera.transform.position;
		Vector3 endPos = firstPos + new Vector3(0, 5, -4);

		while (zoomTime > 0)
		{
			mainCamera.transform.position = Vector3.Lerp(endPos, startPos, zoomTime / 0.5f);
			yield return null;
			zoomTime -= Time.unscaledDeltaTime;
		}

		resultPanelObject.SetActive(true);
		mainCamera.transform.position = endPos;
		objUI.SetActive(false);

		AudioManager.I.PlayAudio("announce");
		resultTextObject.GetComponent<Text>().text = "プレイヤー：" + (maxIndex + 1) + " WIN !!";
		switch (maxIndex)
		{
			case 0:
				resultTextObject.GetComponent<Text>().color = new Color(1.0f, 0f, 0f, 1f);
				break;
			case 1:
				resultTextObject.GetComponent<Text>().color = new Color(0f, 0f, 1.0f, 1f);
				break;
			case 2:
				resultTextObject.GetComponent<Text>().color = new Color(1.0f, 1.0f, 0f, 1f);
				break;
			case 3:
				resultTextObject.GetComponent<Text>().color = new Color(0f, 1.0f, 0f, 1f);
				break;
		}
		yield return new WaitForSeconds(0);
	}
}

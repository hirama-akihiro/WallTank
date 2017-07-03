using UnityEngine;
using System.Collections;

public enum StateMapElement { StageWall = -1, NormalPlane = 0, Wall = 1, MoveWall = 2, ItemSubweapon = 3, SubAttack = 4}

public class StageManager : SingletonMonoBehavior<StageManager> {

	private GameObject protoStage;
    private GameObject plasmaFactory;//プラズマファクトリー
    private GameObject fieldOfBattle;//戦場
    private GameObject snowLand;//スノーランド

	public readonly float MASS_WIDTH = 1.8f;
	public readonly float MASS_HEIGHT = 1.8f;
	public readonly Vector3 TOP_LEFT_POSITION = new Vector3(-10.8f, 0.0f, 7.2f);

	/// <summary>
	/// ステージ中のオブジェクト配置マップ
	/// -1:ステージ境界の壁
	/// 0:通常床
	/// 1:壁オブジェクト
	/// 2:せり上がる壁
	/// 3:アイテム or サブウェポン設置マス
	/// 4:サブ攻撃が発動されているマス(シールド or クロスボム or サークルボム)
	/// </summary>
	private readonly int[,] initStateMap_PF = new int[,]//プラズマファクトリー
	{
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
	};
    private readonly int[,] initStateMap_FoB = new int[,]//フィールドオブバトル
	{
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
		{-1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
	};
    private readonly int[,] initStateMap_SL = new int[,]//スノーランド
	{
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,-1 },
		{-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1,-1 },
	};

	private int[,] stateMap;
	public int massCount;

	// Use this for initialization
	void Start () {
		protoStage = Resources.Load("Prefabs/ProtoStage/ProtoStage") as GameObject;
        plasmaFactory = Resources.Load("Prefabs/PlasmaFactory/PlasmaFactory") as GameObject;
        fieldOfBattle = Resources.Load("Prefabs/FieldOfBattle/FieldOfBattle") as GameObject;
        snowLand = Resources.Load("Prefabs/SnowLand/SnowLand") as GameObject;
        //stateMap = new int[initStateMap_PF.GetLength(0), initStateMap_PF.GetLength(1)];

		GameObject stage;
		switch (GameInfoManager.I.stageType)
		{
			case GameInfoManager.StageType.ProtoStage:
                stateMap = new int[initStateMap_PF.GetLength(0), initStateMap_PF.GetLength(1)];
                for (int i = 0; i < initStateMap_PF.GetLength(0); ++i)
                {
                    for (int j = 0; j < initStateMap_PF.GetLength(1); ++j)
                    {
                        stateMap[i, j] = initStateMap_PF[i, j];
                    }
                }
                stage = Instantiate(protoStage, this.transform.position, this.transform.rotation) as GameObject;
				stage.transform.parent = transform;
				break;
			case GameInfoManager.StageType.PlasmaFactory:
			default:
                stateMap = new int[initStateMap_PF.GetLength(0), initStateMap_PF.GetLength(1)];
                for (int i = 0; i < initStateMap_PF.GetLength(0); ++i)
                {
                    for (int j = 0; j < initStateMap_PF.GetLength(1); ++j)
                    {
                        stateMap[i, j] = initStateMap_PF[i, j];
                    }
                }
                stage = Instantiate(plasmaFactory, this.transform.position, this.transform.rotation)as GameObject;
				stage.transform.parent = transform;
				break;
            case GameInfoManager.StageType.FieldOfBattle:
                stateMap = new int[initStateMap_FoB.GetLength(0), initStateMap_FoB.GetLength(1)];
                for (int i = 0; i < initStateMap_FoB.GetLength(0); ++i)
                {
                    for (int j = 0; j < initStateMap_FoB.GetLength(1); ++j)
                    {
                        stateMap[i, j] = initStateMap_FoB[i, j];
                    }
                }
                stage = Instantiate(fieldOfBattle, this.transform.position, this.transform.rotation)as GameObject;
				stage.transform.parent = transform;
				break;
            case GameInfoManager.StageType.SnowLand:
                stateMap = new int[initStateMap_SL.GetLength(0), initStateMap_SL.GetLength(1)];
                for (int i = 0; i < initStateMap_SL.GetLength(0); ++i)
                {
                    for (int j = 0; j < initStateMap_SL.GetLength(1); ++j)
                    {
                        stateMap[i, j] = initStateMap_SL[i, j];
                    }
                }
                stage = Instantiate(snowLand, this.transform.position, this.transform.rotation)as GameObject;
				stage.transform.parent = transform;
				break;
		}
        massCount = stateMap.GetLength(0) * stateMap.GetLength(1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// 3次元座標を配列に変換するメソッド
	/// Vector2(x,y): x = i, y = j 
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public Vector2 CalcStateMapIndex(Vector3 pos)
	{
		float i = Mathf.Abs((pos.z - (TOP_LEFT_POSITION.z + MASS_HEIGHT / 2))) / MASS_HEIGHT;
		float j = Mathf.Abs((pos.x - (TOP_LEFT_POSITION.x - MASS_HEIGHT / 2))) / MASS_WIDTH;
		return new Vector2((int)i, (int)j);
	}
    public Vector3 CalcPos(Vector2 index)
    {
        return new Vector3((TOP_LEFT_POSITION.x+0.6f-1.8f*index.x)/1.8f,0, (TOP_LEFT_POSITION.z + 0.6f + 1.8f * index.y)/1.8f);
    }
	/// <summary>
	/// StateMapの要素を取得
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	/// <returns></returns>
	public StateMapElement GetStateMapElement(int i, int j)
	{
		return (StateMapElement)stateMap[i, j];
	}

	/// <summary>
	/// StateMapの要素を取得
	/// </summary>
	/// <param name="pos"></param>
	/// <returns></returns>
	public StateMapElement GetStateMapElement(Vector3 pos)
	{
		Vector2 index = CalcStateMapIndex(pos);
		if (index.x < 0 || index.x >= stateMap.GetLength(0) || index.y < 0 || index.y >= stateMap.GetLength(1)) { return StateMapElement.Wall; }
		return (StateMapElement)stateMap[(int)index.x, (int)index.y];
	}

	/// <summary>
	/// StateMapの要素を設定
	/// </summary>
	/// <param name="pos"></param>
	/// <param name="element"></param>
	public void SetStateMapElement(Vector3 pos, StateMapElement element)
	{
		Vector2 index = CalcStateMapIndex(pos);
		stateMap[(int)index.x, (int)index.y] = (int)element;
	}

	/// <summary>
	/// StateMapの要素を設定
	/// </summary>
	/// <param name="i"></param>
	/// <param name="j"></param>
	/// <param name="element"></param>
	public void SetStateMapElement(int i, int j, StateMapElement element)
	{
		stateMap[i, j] = (int)element;
	}

	public int StateMapHeight { get { return stateMap.GetLength(0); } }
	public int StateMapWidth { get { return stateMap.GetLength(1); } }
}

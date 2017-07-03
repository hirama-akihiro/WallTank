using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameInfoManager : SingletonMonoBehavior<GameInfoManager> {

	public enum StageType { ProtoStage = 0, PlasmaFactory = 1, FieldOfBattle = 2, SnowLand = 3}
	public enum RuleType { HP = 0, Score = 1, Flag = 2 }
	public StageType stageType;
	public RuleType ruleType;
	public int numberOfPlayer = 1;
		
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

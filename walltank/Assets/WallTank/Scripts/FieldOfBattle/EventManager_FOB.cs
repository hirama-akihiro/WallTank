using UnityEngine;
using System.Collections;

public class EventManager_FOB : MonoBehaviour {

    private float gameTime_Start;//スタート時間
    private float gameTime_Now;//現在時間
    private GameObject ruleManager;

    private bool tankSpawn;//巨大戦車召喚フラグ

    public GameObject house;

	// Use this for initialization
	void Start () {
        ruleManager = GameObject.Find("RuleManager");
        gameTime_Start = ruleManager.GetComponent<RuleManager>().gameTime;

        tankSpawn = false;
	}
	
	// Update is called once per frame
	void Update () {
        gameTime_Now = ruleManager.GetComponent<RuleManager>().gameTime;

        if (gameTime_Now <= 20.0f && !tankSpawn)
        {
            house.GetComponent<House>().breakObject();
            tankSpawn = true;
        }
        
        /*
        if (Input.GetKeyDown(KeyCode.A))
        {
            house.GetComponent<House>().breakObject();
        }
         */ 
	}

    float getTimeNow()
    {
        return gameTime_Now;
    }
}

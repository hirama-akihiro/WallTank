using UnityEngine;
using System.Collections;

public class EventManager_PlasmaFactory : MonoBehaviour {

    public GameObject warpHole;
    public GameObject Robot1;
    public GameObject Robot2;
    public GameObject Robot3;
    public GameObject Robot4;
    public GameObject Vehicle;
    public GameObject Helicopter;
    public GameObject DangerMark;
    public GameObject WallObject;

    private GameObject obj;

    private float time;
    private float worpHoleTime;
    private float LazerTime;
    private float vehicleTime;
    private float heliTime;
    private float wallTime;
    private float gameTime_Start;//スタート時間
    private float gameTime_Now;//現在時間
    private GameObject ruleManager;
    private GameObject wallObjectMain;

    private bool isWorpOpen;
    private bool isWallAll1;
    private bool isWallAll2;


	// Use this for initialization
	void Start () {
        time = 0.0f;
        worpHoleTime = 0.0f;
        LazerTime = 0.0f;
        vehicleTime = -8.0f;
        heliTime = -15.0f;
        wallTime = 0.0f;
        ruleManager = GameObject.Find("RuleManager");
        gameTime_Start = ruleManager.GetComponent<RuleManager>().gameTime;
        wallObjectMain = GameObject.Find("WallObjectMain");

        isWorpOpen = false;
        isWallAll1 = false;
        isWallAll2 = false;
	}
	
	// Update is called once per frame
	void Update () {

        gameTime_Now = ruleManager.GetComponent<RuleManager>().gameTime;
        AI();
        /*
        //ワープホール開閉
        if (Input.GetKeyDown(KeyCode.Q))
        {
            warpHole.GetComponent<WarpHole>().OpenOrClose();
        }

        //ロボットレーザー照射
        if (Input.GetKeyDown(KeyCode.W))
        {
            RobotLazer();
        }

        //ビークル召喚
        if (Input.GetKeyDown(KeyCode.E))
        {
            SetVehicle();
        }

        //ヘリコプター召喚
        if (Input.GetKeyDown(KeyCode.R))
        {
            SetHelicopter();
        }
       

        //壁上下
        if(Input.GetKeyDown(KeyCode.T))
        {
            CreateObject();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            CreateObject1();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            CreateObject2();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            CreateObject3();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            CreateObjectAll();
        }
         */ 
	}

    void AI()
    {
        time += Time.deltaTime;

        worpHoleTime += Time.deltaTime;
        LazerTime += Time.deltaTime;
        vehicleTime += Time.deltaTime;
        heliTime += Time.deltaTime;
        wallTime += Time.deltaTime;

        //ワープホールのAI
        /*
        if (worpHoleTime >= 10.0f)
        {
            if (isWorpOpen)
                worpHoleTime = 0.0f - Random.Range(0, 6);
            else
                worpHoleTime = 5.0f;
            WorpHole();
        }
         */ 

        //ロボットレーザーのAI
        if (LazerTime >= 15.0f)
        {
            RobotLazer();
            LazerTime = 0.0f - Random.Range(0, 6);
        }

        //ビークルのAI
        if (vehicleTime >= 15.0f)
        {
            SetVehicle();
            vehicleTime = 0.0f - Random.Range(0, 5);
        }

        //ヘリコプターのAI
        if (heliTime >= 20.0f)
        {
            SetHelicopter();
            heliTime = 0.0f - Random.Range(0, 7);
        }

        //壁生成AI
        if (gameTime_Now <= gameTime_Start / 2 + 3 && !isWallAll2)
        {
            //WallObject.GetComponent<CreateObjectMain>().Down();
            CreateObject();
            isWallAll2 = true;
        }
        /*
        if (wallTime >= 10.0f)
        {
            int r = Random.Range(1, 5);
            switch (r)
            {
                case 1:
                    {
                        CreateObject1();
                        break;
                    }
                case 2:
                    {
                        CreateObject2();
                        break;
                    }
                case 3:
                    {
                        CreateObject3();
                        break;
                    }
                case 4:
                    {
                        CreateObject();
                        break;
                    }
            }
            wallTime = 0.0f;
        }
         */ 
    }

    //ワープホール起動
    void WorpHole()
    {
        warpHole.GetComponent<WarpHole>().OpenOrClose();
    }

    //ロボットレーザー照射
    void RobotLazer()
    {
        int number = Random.Range(1, 5);
        switch (number)
        {
            case 1:
                {
                    Robot1.GetComponent<Robot>().LazerShot();
                    break;
                }
            case 2:
                {
                    Robot2.GetComponent<Robot>().LazerShot();
                    break;
                }
            case 3:
                {
                    Robot3.GetComponent<Robot>().LazerShot();
                    break;
                }
            case 4:
                {
                    Robot4.GetComponent<Robot>().LazerShot();
                    break;
                }
        }
    }

    //ビークル召喚
    void SetVehicle()
    {
        int number = Random.Range(1, 4);
        switch (number)
        {
            case 1:
                {
                    obj = (GameObject)Instantiate(Vehicle, new Vector3(-30.0f, 0.0f, 1.8f), Vehicle.transform.rotation);
                    obj.transform.parent = this.transform;
                    obj = (GameObject)Instantiate(DangerMark, new Vector3(-10.0f, 4.0f, 1.8f), DangerMark.transform.rotation);
                    obj.transform.parent = this.transform;
                    //Instantiate(Vehicle, new Vector3(-30.0f, 0.0f, 1.8f), Vehicle.transform.rotation);
                    //Instantiate(DangerMark, new Vector3(-10.0f, 4.0f, 1.8f), DangerMark.transform.rotation);
                    break;
                }
            case 2:
                {
                    obj = (GameObject)Instantiate(Vehicle, new Vector3(-30.0f, 0.0f, -1.8f), Vehicle.transform.rotation);
                    obj.transform.parent = this.transform;
                    obj = (GameObject)Instantiate(DangerMark, new Vector3(-10.0f, 4.0f, -1.8f), DangerMark.transform.rotation);
                    obj.transform.parent = this.transform;
                    //Instantiate(Vehicle, new Vector3(-30.0f, 0.0f, -1.8f), Vehicle.transform.rotation);
                    //Instantiate(DangerMark, new Vector3(-10.0f, 4.0f, -1.8f), DangerMark.transform.rotation);
                    break;
                }
            case 3:
                {
                    obj = (GameObject)Instantiate(Vehicle, new Vector3(-30.0f, 0.0f, -5.4f), Vehicle.transform.rotation);
                    obj.transform.parent = this.transform;
                    obj = (GameObject)Instantiate(DangerMark, new Vector3(-10.0f, 4.0f, -5.4f), DangerMark.transform.rotation);
                    obj.transform.parent = this.transform;
                    //Instantiate(Vehicle, new Vector3(-30.0f, 0.0f, -5.4f), Vehicle.transform.rotation);
                    //Instantiate(DangerMark, new Vector3(-10.0f, 4.0f, -5.4f), DangerMark.transform.rotation);
                    break;
                }
        }
    }

    //ヘリコプター召喚
    void SetHelicopter()
    {
		AudioManager.I.PlayAudio("heri", 0.8f);
		int number = Random.Range(1, 4);
        //number = 3;
        switch (number)
        {
            case 1:
                {
                    obj = (GameObject)Instantiate(Helicopter, new Vector3(22.0f, 7.0f, 1.8f), Helicopter.transform.rotation);
                    obj.transform.parent = this.transform;
                    //Instantiate(Helicopter, new Vector3(22.0f, 7.0f, 1.8f), Helicopter.transform.rotation);
                    break;
                }
            case 2:
                {
                    obj = (GameObject)Instantiate(Helicopter, new Vector3(22.0f, 7.0f, -1.8f), Helicopter.transform.rotation);
                    obj.transform.parent = this.transform;
                    //Instantiate(Helicopter, new Vector3(22.0f, 7.0f, -1.8f), Helicopter.transform.rotation);
                    break;
                }
            case 3:
                {
                    obj = (GameObject)Instantiate(Helicopter, new Vector3(22.0f, 7.0f, -5.4f), Helicopter.transform.rotation);
                    obj.transform.parent = this.transform;
                    //Instantiate(Helicopter, new Vector3(22.0f, 7.0f, -5.4f), Vehicle.transform.rotation);
                    break;
                }
        }
    }

    //壁生成
    void CreateObject()
    {
        WallObject.GetComponent<CreateObjectMain>().RandomCreate();
    }

    void CreateObject1()
    {
        WallObject.GetComponent<CreateObjectMain>().LineCreate1();
    }
    void CreateObject2()
    {
        WallObject.GetComponent<CreateObjectMain>().LineCreate2();
    }
    void CreateObject3()
    {
        WallObject.GetComponent<CreateObjectMain>().LineCreate3();
    }
    void CreateObjectAll()
    {
        WallObject.GetComponent<CreateObjectMain>().AllCreate();
    }
}

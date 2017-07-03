using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CreateObjectMain : MonoBehaviour {

    private List<GameObject> list;

    public GameObject wallObject1;
    public GameObject wallObject2;
    public GameObject wallObject3;
    public GameObject wallObject4;
    public GameObject wallObject5;
    public GameObject wallObject6;
    public GameObject wallObject7;
    public GameObject wallObject8;
    public GameObject wallObject9;
    public GameObject wallObject10;
    public GameObject wallObject11;
    public GameObject wallObject12;

    private List<bool> boolList;

    private Random rand;

    private bool isUp;
    private float time;

	// Use this for initialization
	void Start () {
        list = new List<GameObject>();

        list.Add(wallObject1);
        list.Add(wallObject2);
        list.Add(wallObject3);
        list.Add(wallObject4);
        list.Add(wallObject5);
        list.Add(wallObject6);
        list.Add(wallObject7);
        list.Add(wallObject8);
        list.Add(wallObject9);
        list.Add(wallObject10);
        list.Add(wallObject11);
        list.Add(wallObject12);

        boolList = new List<bool>();
        for (int i = 0; i < 12; i++)
            boolList.Add(false);

        isUp = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isUp)
            time += Time.deltaTime;

        if (time >= 10.0f && isUp)
        {
            Down();
            isUp = false;
        }
	}

    //ランダムに壁生成
    public void RandomCreate()
    {
        if (!isUp)
        {
            //Down();
            isUp = true;
            time = 0.0f;
            for (int i = 0; i < 12; i++)
            {
                if (Random.Range(0, 5) <= 2)
                {
                    if (!boolList[i])
                    {
                        boolList[i] = true;
                        list[i].GetComponent<WallObject>().UP();
                    }
                }
            }
        }
    }

    //１列目の壁生成
    public void LineCreate1()
    {
        if (!isUp)
        {
            isUp = true;
            time = 0.0f;
            for (int i = 0; i < 4; i++)
            {
                if (!boolList[i])
                {
                    boolList[i] = true;
                    list[i].GetComponent<WallObject>().UP();
                }
            }
        }
    }

    //２列目の壁生成
    public void LineCreate2()
    {
        if (!isUp)
        {
            isUp = true;
            time = 0.0f;
            for (int i = 4; i < 8; i++)
            {
                if (!boolList[i])
                {
                    boolList[i] = true;
                    list[i].GetComponent<WallObject>().UP();
                }
            }
        }
    }

    //３列目の壁生成
    public void LineCreate3()
    {
        if (!isUp)
        {
            isUp = true;
            time = 0.0f;
            for (int i = 8; i < 12; i++)
            {
                if (!boolList[i])
                {
                    boolList[i] = true;
                    list[i].GetComponent<WallObject>().UP();
                }
            }
        }
    }

    //全列の壁生成
    public void AllCreate()
    {
        if (!isUp)
        {
            isUp = true;
            time = 0.0f;
            for (int i = 0; i < 12; i++)
            {
                if (!boolList[i])
                {
                    boolList[i] = true;
                    list[i].GetComponent<WallObject>().UP();
                }
            }
        }
    }

    //上がっている壁をすべて下げる
    public void Down()
    {
        for (int i = 0; i < 12; i++)
        {
            if (boolList[i])
            {
                boolList[i] = false;
                if (list[i].GetComponent<WallObject>().isUp)
                {
                    boolList[i] = false;
                    list[i].GetComponent<WallObject>().DOWN();
                    list[i].GetComponent<BoxCollider>().isTrigger = true;
                }
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class WallObject : MonoBehaviour {

    /// <summary>
    /// 稼働中
    /// </summary>
    private bool isMoving;

    /// <summary>
    /// 稼働時間
    /// </summary>
    private float move;

    /// <summary>
    /// 稼働限界点
    /// </summary>
    private float stopLine;

    /// <summary>
    /// 上がっているか
    /// </summary>
    public bool isUp;

    /// <summary>
    /// 初期位置
    /// </summary>
    private Vector3 startPosition;

    /// <summary>
    /// フィールドのステート
    /// </summary>
    private StateMapElement state;

    /// <summary>
    /// BoxCollider
    /// </summary>
    private BoxCollider boxCollider;

	// Use this for initialization
	void Start () {
        isMoving = false;
        move = 0.0f;
        stopLine = 1.84f;
        isUp = false;
        startPosition = this.transform.position;
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.isTrigger = true;
	}
	
	// Update is called once per frame
	void Update () {
        if (isMoving)
        {

            if (isUp)
            {
                this.gameObject.transform.position += new Vector3(0.0f, 0.1f, 0.0f);
                move += 0.1f;
                if (move >= stopLine)
                    isMoving = false;
            }
            else if (!isUp)
            {
                this.gameObject.transform.position += new Vector3(0.0f, -0.1f, 0.0f);
                move += 0.1f;
                if (move >= stopLine)
                    isMoving = false;
            }
        }
	}

    public void UP()
    {
        /*
        index = stageManager.GetComponent<StageManager>().CalcStateMapIndex(this.transform.position);
		//state = stageManager.GetComponent<StageManager>().stateMap[(int)index.x, (int)index.y];
		state = StageManager.I.GetStateMapElement(transform.position);
        if (state == StateMapElement.NormalPlane)
        {
            //stageManager.GetComponent<StageManager>().stateMap[(int)index.x, (int)index.y] = 1;
            StageManager.I.SetStateMapElement(transform.position, StateMapElement.Wall);

            isMoving = true;
            isUp = true;
            move = 0.0f;
        }
         */ 

        state = StageManager.I.GetStateMapElement(transform.position);
        if (state == StateMapElement.NormalPlane && !isUp)
        {
            StageManager.I.SetStateMapElement(transform.position, StateMapElement.Wall);
            isMoving = true;
            isUp = true;
            move = 0.0f;
            boxCollider.isTrigger = false;
        }
    }

    public void DOWN()
    {
        /*
        index = stageManager.GetComponent<StageManager>().CalcStateMapIndex(this.transform.position);
        //state = stageManager.GetComponent<StageManager>().stateMap[(int)index.x, (int)index.y];
        state = StageManager.I.GetStateMapElement(transform.position);
        //stageManager.GetComponent<StageManager>().stateMap[(int)index.x, (int)index.y] += UpDown = 0; 
        StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
        isMoving = true;
        isUp = false;
        move = 0.0f;
         */

        if (isUp)
        {
            StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
            isMoving = true;
            isUp = false;
            move = 0.0f;
            boxCollider.isTrigger = true;
        }
    }

    void OnCollisionEnter(Collision c)
    {
        if (isMoving && move <= 0.3f && isUp)
        {
            if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
            {
                this.transform.position = startPosition;
                isMoving = false;
                move = 0.0f;
                isUp = false;
               // Vector2 index = stageManager.GetComponent<StageManager>().CalcStateMapIndex(this.transform.position);
				//state = stageManager.GetComponent<StageManager>().stateMap[(int)index.x, (int)index.y];
				//stageManager.GetComponent<StageManager>().stateMap[(int)index.x, (int)index.y] = 0;
				//state = StageManager.I.GetStateMapElement(transform.position);
				StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
                boxCollider.isTrigger = true;
            }
        }
    }
}

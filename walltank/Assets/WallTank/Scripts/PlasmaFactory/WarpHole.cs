using UnityEngine;
using System.Collections;

public class WarpHole : MonoBehaviour {

    private int doorVec;
    private bool isDoorMove;
    private float doorSpeed;
    private float moveLength;
    private bool isOpen;
    private bool isWorping;

    private bool isWorp_1P;
    private bool isWorp_2P;
    private bool isWorp_3P;
    private bool isWorp_4P;

    public GameObject Door_L_1;
    public GameObject Door_L_2;
    public GameObject Door_R_1;
    public GameObject Door_R_2;
    public GameObject HoleEnter;
    public GameObject HoleExit;
    public GameObject WorpEffect;

    private GameObject obj;

	// Use this for initialization
	void Start () {
        doorVec = 1;
        isDoorMove = false;
        doorSpeed = 0.01f;
        moveLength = 0.0f;
        isOpen = false;
        isWorping = false;

        isWorp_1P = false;
        isWorp_2P = false;
        isWorp_3P = false;
        isWorp_4P = false; 
	}
	
	// Update is called once per frame
	void Update () {
        if (isDoorMove){
            Door_L_1.transform.position += new Vector3(doorSpeed * doorVec, 0.0f, 0.0f);
            Door_L_2.transform.position += new Vector3(doorSpeed * doorVec, 0.0f, 0.0f);
            Door_R_1.transform.position += new Vector3(doorSpeed * doorVec * -1, 0.0f, 0.0f);
            Door_R_2.transform.position += new Vector3(doorSpeed * doorVec * -1, 0.0f, 0.0f);

            moveLength += doorSpeed;
            if (moveLength >= 0.4f)
                isDoorMove = false;
        }
	}

    public void OpenOrClose(){
        if (!isDoorMove){
            doorVec *= -1;
            isDoorMove = true;
            moveLength = 0.0f;

            if (!isOpen)
            {
                isOpen = true;
                HoleEnter.SetActive(true);
                HoleExit.SetActive(true);
            }
            else if (isOpen)
            {
                isOpen = false;
                HoleEnter.SetActive(false);
                HoleExit.SetActive(false);
            }
        }
    }

    public void WorpStart(GameObject player)
    {
        obj = (GameObject)Instantiate(WorpEffect, new Vector3(-7.4f, -1.3f, 1.9f), WorpEffect.transform.rotation);
        obj.transform.parent = this.transform;
        //Instantiate(WorpEffect, new Vector3(-7.4f, -1.3f, 1.9f), WorpEffect.transform.rotation);

    }
}

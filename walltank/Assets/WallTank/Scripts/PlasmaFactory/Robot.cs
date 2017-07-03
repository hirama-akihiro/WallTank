using UnityEngine;
using System.Collections;

public class Robot : MonoBehaviour {

    public Transform LazerPoint;
    public GameObject Lazer;

    private GameObject obj;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

    //レーザー発射
    public void LazerShot()
    {
        obj = (GameObject)Instantiate(Lazer, LazerPoint.position, LazerPoint.rotation);
        obj.transform.parent = this.transform;
        //Instantiate(Lazer, LazerPoint.position, LazerPoint.rotation);
    }
}

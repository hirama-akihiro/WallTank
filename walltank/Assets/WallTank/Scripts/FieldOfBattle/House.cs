using UnityEngine;
using System.Collections;

public class House : MonoBehaviour {

    private GameObject obj;
    private GameObject eventManager;
    public GameObject tiger;
    public GameObject breakEffect;

	// Use this for initialization
	void Start () {
        eventManager = GameObject.Find("EventManager");
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void breakObject()
    {
        obj = (GameObject)Instantiate(breakEffect, this.transform.position, this.transform.rotation);
        obj.transform.parent = eventManager.transform;
        obj = (GameObject)Instantiate(tiger, this.transform.position, this.transform.rotation);
        obj.transform.parent = eventManager.transform;
        Destroy(this.gameObject);
    }
}

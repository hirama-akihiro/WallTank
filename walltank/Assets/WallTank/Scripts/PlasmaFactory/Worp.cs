using UnityEngine;
using System.Collections;

public class Worp : MonoBehaviour {

    private GameObject worpHole;
    public GameObject worpEffect;

	// Use this for initialization
	void Start () {
        //worpHole = GameObject.Find("WorpHole");

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            Debug.Log("aaa");
            Instantiate(worpEffect, this.transform.position, this.transform.rotation);
        }
    }
}

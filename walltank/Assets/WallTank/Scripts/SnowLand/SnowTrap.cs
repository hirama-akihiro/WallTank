using UnityEngine;
using System.Collections;

public class SnowTrap : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //ダメージ判定
    void OnTriggerEnter(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            //Debug.Log("snow");
            c.GetComponent<Tank>().changeSpeed(2.0f);
        }
    }

    void OnTriggerExit(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            c.GetComponent<Tank>().changeSpeed(4.0f);
        }
    }
}

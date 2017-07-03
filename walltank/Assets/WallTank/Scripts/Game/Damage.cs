using UnityEngine;
using System.Collections;

public class Damage : MonoBehaviour {

    public float atkPower;

	// Use this for initialization
	void Start () {
	  
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    /*
    void OnCollisionEnter(Collision c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            c.gameObject.GetComponent<Status>().Damage(ap);
        }
    }
     */ 

    //ダメージ判定
    void OnTriggerEnter(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            c.gameObject.GetComponent<Tank>().Damage(atkPower);
        }
    }
}

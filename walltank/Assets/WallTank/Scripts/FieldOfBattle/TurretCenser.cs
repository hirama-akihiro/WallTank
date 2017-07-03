using UnityEngine;
using System.Collections;

public class TurretCenser : MonoBehaviour {

    private bool censerSwitch;
    //private SphereCollider collider;
    private GameObject player;
    public GameObject turret;

	// Use this for initialization
	void Start () {
        censerSwitch = false;
        //collider = GetComponent<SphereCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        if (censerSwitch)
        {
            //collider.radius += Time.deltaTime;
        }
	}

    void OnTriggerEnter(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            Debug.Log("aaaa");
            player = c.gameObject;
            censerOff();
            //turret.GetComponent<Turret>().getPlayerObject(player);
        }
    }

    public void censerOn()
    {
        censerSwitch = true;
        //collider.radius = 0.1f;
    }

    public void censerOff()
    {
        censerSwitch = false;
        //collider.radius = 0.1f;
    }

    public GameObject getPlayerObject()
    {
        return player;
    }
}

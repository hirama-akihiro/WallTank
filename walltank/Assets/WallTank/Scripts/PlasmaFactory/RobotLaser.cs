using UnityEngine;
using System.Collections;

public class RobotLaser : MonoBehaviour {

    private BoxCollider collider;
    private float time;
    private float range;

    public float atkPower;

	// Use this for initialization
	void Start () {
        collider = GetComponent<BoxCollider>();
        time = 0;
        range = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 3.0f && range <= 50.0f){
            collider.size += new Vector3(range, 0.0f, 0.0f);
        }
        if (time >= 5.0f)
            Destroy(this.gameObject);
	}

    //ダメージ判定
    void OnTriggerStay(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            c.gameObject.GetComponent<Tank>().Damage(atkPower);
        }
    }
}

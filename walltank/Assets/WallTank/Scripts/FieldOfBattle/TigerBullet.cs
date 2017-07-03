using UnityEngine;
using System.Collections;

public class TigerBullet : MonoBehaviour {

    public GameObject explosion;
    public float speed;
    private GameObject obj;
    private GameObject eventManager;

	// Use this for initialization
	void Start () {
        eventManager = GameObject.Find("EventManager");
        transform.Rotate(new Vector3(0.0f, this.transform.rotation.y, 90.0f));
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
        transform.position -= new Vector3(0.0f, 3.0f * Time.deltaTime, 0.0f);
	}

    //爆発
    void OnCollisionEnter(Collision c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player") || LayerMask.LayerToName(c.gameObject.layer).Equals("Floor") || LayerMask.LayerToName(c.gameObject.layer).Equals("Wall"))
        {
            obj = (GameObject)Instantiate(explosion, this.transform.position, this.transform.rotation);
            obj.transform.parent = eventManager.transform;
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player") || LayerMask.LayerToName(c.gameObject.layer).Equals("Floor") || LayerMask.LayerToName(c.gameObject.layer).Equals("Wall"))
        {
            obj = (GameObject)Instantiate(explosion, this.transform.position, this.transform.rotation);
            obj.transform.parent = eventManager.transform;
            Destroy(this.gameObject);
        }
    }
}

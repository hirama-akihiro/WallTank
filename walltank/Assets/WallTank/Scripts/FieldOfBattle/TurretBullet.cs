using UnityEngine;
using System.Collections;

public class TurretBullet : MonoBehaviour {

    public float lifeTime;
    public float speed;
    public GameObject breakEffect;
    private GameObject obj;
    private GameObject eventManager;

	// Use this for initialization
	void Start () {
        eventManager = GameObject.Find("EventManager");
        transform.Rotate(new Vector3(90.0f, this.transform.rotation.y, 90.0f));
	}
	
	// Update is called once per frame
	void Update () {
        lifeTime -= Time.deltaTime;
        transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
	}

    void OnTriggerEnter(Collider c)
    {
        //プレイヤー接触で消える
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            obj = (GameObject)Instantiate(breakEffect, this.transform.position, this.transform.rotation);
            obj.transform.parent = eventManager.transform;
            Destroy(gameObject);
            //Instantiate(breakEffect, this.transform.position, this.transform.rotation);
        }
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Wall") || LayerMask.LayerToName(c.gameObject.layer).Equals("Block"))
        {
            Destroy(gameObject);
        }
    }
}

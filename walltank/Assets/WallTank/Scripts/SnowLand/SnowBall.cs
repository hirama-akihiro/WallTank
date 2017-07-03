using UnityEngine;
using System.Collections;

public class SnowBall : MonoBehaviour {

    private float time;
    private GameObject obj;
    private GameObject eventManager;
    private Rigidbody rigidBody;
    private bool isShot;

    public GameObject snowTrap;

	// Use this for initialization
	void Start () {
        time = 0.0f;
        eventManager = GameObject.Find("EventManager");
        rigidBody = GetComponent<Rigidbody>();
        isShot = false;
        this.gameObject.transform.localScale = new Vector3(0.0f, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {
        if (time <= 1.5f && !isShot)
        {
            time += Time.deltaTime;
            gameObject.transform.localScale = new Vector3(time, time, time);
        }
        else if (time >= 1.5f && !isShot)
        {
            isShot = true;
            Shot();
        }
	}

    //床トラップ生成
    void OnCollisionEnter(Collision c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Floor"))
        {
            obj = (GameObject)Instantiate(snowTrap, this.gameObject.transform.position, snowTrap.transform.rotation);
            obj.transform.parent = eventManager.transform;
            Destroy(this.gameObject);
        }
    }

    //飛ばす
    public void Shot()
    {
        rigidBody.isKinematic = false;
        rigidBody.AddForce(Vector3.up * 5.0f, ForceMode.VelocityChange);
        rigidBody.AddForce(this.gameObject.transform.forward * Random.Range(1.0f, 5.0f), ForceMode.VelocityChange);
    }

    void OnTriggerEnter(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Floor"))
        {
            obj = (GameObject)Instantiate(snowTrap, this.gameObject.transform.position, snowTrap.transform.rotation);
            obj.transform.parent = eventManager.transform;
            Destroy(this.gameObject);
        }
    }
}

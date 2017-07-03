using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {

    public float speed;

    public GameObject Explosion;

    private GameObject obj;
    private GameObject eventManager;

    private float time;

    public float atkPower;

	// Use this for initialization
	void Start () {
		GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward) * speed;
        eventManager = GameObject.Find("EventManager");
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 14)
        {
            Destroy(this.gameObject);
        }
        //this.transform.position += this.transform.TransformDirection(Vector3.forward) * speed;
	}

    /// <summary>
    /// 接触判定
    /// </summary>
    /// <param name="c"></param>
    void OnCollisionEnter(Collision c){
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player")){
            Destroy(this.gameObject);
            obj = (GameObject)Instantiate(Explosion, this.transform.position, this.transform.rotation);
            obj.transform.parent = eventManager.transform;
            c.gameObject.GetComponent<Tank>().Damage(atkPower);
            //Instantiate(Explosion, this.transform.position, this.transform.rotation);
        }
    }
}

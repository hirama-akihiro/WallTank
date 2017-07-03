using UnityEngine;
using System.Collections;

public class Tiger : MonoBehaviour {

    public GameObject bullet;
    public GameObject CannonBarrel;
    public Transform shotPosition;
    public float speed;
    private GameObject eventManager;
    public GameObject breakEffect;
    private GameObject obj;

    public float rotateTankSpeed;
    public float rotateCannonSpeed;

    private float coolTime;
    private int count;

	// Use this for initialization
	void Start () {
        coolTime = 5.1f;
        eventManager = GameObject.Find("EventManager");
        count = 0;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += transform.TransformDirection(Vector3.forward) * speed * Time.deltaTime;
        transform.Rotate(new Vector3(0f, rotateTankSpeed, 0f));
        CannonBarrel.transform.Rotate(new Vector3(0f, rotateCannonSpeed, 0f));

        //射撃
        coolTime -= Time.deltaTime;
        if (coolTime <= 0.0f)
        {
            obj = (GameObject)Instantiate(bullet, shotPosition.transform.position, shotPosition.transform.rotation);
            obj.transform.parent = eventManager.transform;
            coolTime = 5.0f + Random.Range(0, 3);
        }

        if (count >= 3)
        {
            count = 0 - Random.Range(0, 3);
            rotateCannonSpeed *= -1;
        }
	}

    void OnCollisionEnter(Collision c)
    {
        //障害物破壊エフェクト
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Block"))
        {
            obj = (GameObject)Instantiate(breakEffect, c.gameObject.transform.position, this.transform.rotation);
            obj.transform.parent = eventManager.transform;
            Destroy(c.gameObject);
        }
        //バウンド
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Wall"))
        {
            count += 1;
            speed *= -1;
        }
    }
}

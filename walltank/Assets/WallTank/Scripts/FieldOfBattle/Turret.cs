using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

    public float coolTime;
    public GameObject bullet;
    private GameObject obj;
    private enum State { passive, active, search };
    private State state = State.passive;
    private float spinTime;

    public GameObject shotPosition;
    private float rotateSpeed;
    private GameObject eventManger;
    private GameObject fob;

    //センサー用
    /*
    private SphereCollider collider;
    private GameObject player;
    public GameObject censer;
    */

	// Use this for initialization
	void Start () {
        //collider = GetComponent<SphereCollider>();
        eventManger = GameObject.Find("EventManager");
        fob = GameObject.Find("FieldOfBattle(Clone)");
        shotPosition = (GameObject)Instantiate(shotPosition, this.transform.position, this.transform.rotation);
        shotPosition.transform.parent = fob.transform;
	}
	
	// Update is called once per frame
	void Update () {
        switch (state)
        {
            case State.passive:
                {
                    coolTime -= Time.deltaTime;
                    if (coolTime <= 0.0f)
                    {
                        state = State.search;
                        spinTime = 3.0f;
                        rotateSpeed = Random.Range(1.0f, 7.0f);
                        //censer.GetComponent<TurretCenser>().censerOn();
                    }
                    break;
                }
            case State.search:
                {
                    //int rand = Random.Range(1, 8);
                    //transform.Rotate(new Vector3(0.0f, 0.0f, rand*45.0f));
                    spinTime -= Time.deltaTime;
                    gameObject.transform.Rotate(0, rotateSpeed, 0.0f, Space.World);
                    shotPosition.transform.Rotate(0, rotateSpeed, 0.0f, Space.World);
                    if (spinTime <= 0.0f)
                    {
                        state = State.active;
                    }
                    //collider.radius += Time.deltaTime;
                    /*
                    if (player != null)
                    {
                        //collider.radius = 0.1f;
                        state = State.active;
                        transform.LookAt(player.transform);
                        transform.Rotate(new Vector3(-90.0f, this.transform.rotation.y, 0.0f));
                    }
                     */
                    break;
                }
            case State.active:
                {
                    //obj = (GameObject)Instantiate(bullet, this.transform.position, this.transform.rotation);
                    //obj.transform.parent = this.transform;
                    obj = (GameObject)Instantiate(bullet, this.transform.position, this.transform.rotation);
                    obj.transform.parent = eventManger.transform;
                    //player = null;
                    coolTime = Random.Range(5.0f, 7.7f);
                    state = State.passive;
                    break;
                }
        }
	}
    /*
    public void getPlayerObject(GameObject playerObject)
    {
        player = playerObject;
    }

    void OnTriggerEnter(Collider c)
    {
        if (LayerMask.LayerToName(c.gameObject.layer).Equals("Player"))
        {
            //Debug.Log("aaaa");
            player = c.gameObject;
        }
    }
     */ 
}

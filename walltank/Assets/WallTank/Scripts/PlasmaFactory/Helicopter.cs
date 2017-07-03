using UnityEngine;
using System.Collections;

public class Helicopter : MonoBehaviour {

    public Transform dropPosition;
    public GameObject itemBox;

    private GameObject obj;
    private GameObject eventManager;

    private float speed;

    private float time;

    private float dropCount;

    private float dropOffset;

	// Use this for initialization
	void Start () {
        dropOffset = Random.Range(0.0f, 1.5f);
        speed = 2.5f;
        time = -4.7f + dropOffset;
        dropCount = 2;
		GetComponent<Rigidbody>().velocity = new Vector3(-speed, 0.0f, 0.0f);
        eventManager = GameObject.Find("EventManager");
	}
	
	// Update is called once per frame
	void Update () {
        //this.transform.position += new Vector3(-speed, 0.0f, 0.0f);
        //time += Time.deltaTime;
        if (this.transform.position.x <= 5.4f && dropCount == 2){
            Drop();
            dropCount -= 1;
        }
        if (this.transform.position.x <= -5.4f && dropCount == 1)
        {
            Drop();
            dropCount -= 1;
        }
		if (this.transform.position.x <= -15.0f)
		{
			Destroy(this.gameObject);
		}
	}

    /// <summary>
    /// 投下(アイテムor攻撃)
    /// </summary>
    void Drop(){
        obj = (GameObject)Instantiate(itemBox, dropPosition.transform.position, dropPosition.transform.rotation);
        obj.transform.parent = eventManager.transform;
        //Instantiate(itemBox, dropPosition.transform.position, dropPosition.transform.rotation);

        /*
        rand = Random.Range(1, 6);
        switch (rand)
        {
            case 1:
                {
                    Instantiate(Item1, dropPosition.transform.position, dropPosition.transform.rotation);
                    break;
                }
            case 2:
                {
                    Instantiate(Item2, dropPosition.transform.position, dropPosition.transform.rotation);
                    break;
                }
            case 3:
                {
                    Instantiate(Item3, dropPosition.transform.position, dropPosition.transform.rotation);
                    break;
                }
            case 4:
                {
                    Instantiate(Item4, dropPosition.transform.position, dropPosition.transform.rotation);
                    break;
                }
            case 5:
                {
                    Instantiate(Item5, dropPosition.transform.position, dropPosition.transform.rotation);
                    break;
                }
        }
        */
    }
}

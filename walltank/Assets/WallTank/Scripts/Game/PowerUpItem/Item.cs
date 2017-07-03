using UnityEngine;
using System.Collections;

public class Item : GetableObject {
    protected Status myStatus;
    public bool isDropped;
    protected int count;
    public GameObject particle;
    // Use this for initialization
    void Start () {
        isDropped = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (isDropped == true)
        {
            StartCoroutine(StartCoolTime());
        }
        if (isDropped == false)
        {
            gameObject.transform.position = new Vector3(
                gameObject.transform.position.x,
                PingPong(0.5f, 1, 1) + 1f,
                gameObject.transform.position.z);
            gameObject.transform.Rotate(0, 1, 0);
        }
    }
    IEnumerator StartCoolTime()
    {
        Transform tank = gameObject.transform.parent;
        yield return new WaitForSeconds(5.0f);
        if (gameObject.GetComponentInParent<Tank>().myStatus.itemHolder.Count != 0)
        {
            gameObject.GetComponentInParent<Tank>().myStatus.itemHolder.RemoveAt(0);
            end();
            foreach (Transform obj in tank)
            {
                if (obj.gameObject.layer == LayerMask.NameToLayer("item"))
                {
                    obj.position += new Vector3(0, -1, 0);
                }
            }
        }
        else
        {//バグの回避
            Destroy(gameObject);
        }
    }
    void end()
    {
        gameObject.GetComponentInParent<Tank>().myStatus = gameObject.GetComponentInParent<Tank>().myStatus- myStatus;
        Destroy(gameObject);
    }
    protected override void SettingObject(GameObject tankObject)
    {
        if (tankObject.layer == LayerMask.NameToLayer("Player"))
        {
            tankObject.GetComponent<Tank>().myStatus.itemHolder.Add(gameObject);

            tankObject.GetComponent<Tank>().myStatus = tankObject.GetComponent<Tank>().myStatus + myStatus;
            gameObject.transform.parent = tankObject.transform;
            gameObject.transform.position = tankObject.transform.position + new Vector3(0, 5 + tankObject.GetComponent<Tank>().myStatus.itemHolder.Count, 0);
            gameObject.GetComponent<Item>().isDropped = true;
            GameObject obj = Instantiate(particle);
            obj.transform.parent = gameObject.transform;
            obj.transform.position = gameObject.transform.parent.position;
        }
    }
    protected float PingPong(float moveSpeed, float moveTime, float range)
    {
        return Mathf.PingPong(Time.time * Mathf.Abs(moveSpeed), Mathf.Abs(range));
    }
}

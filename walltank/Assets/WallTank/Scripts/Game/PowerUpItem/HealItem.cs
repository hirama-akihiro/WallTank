using UnityEngine;
using System.Collections;

public class HealItem : Item
{

    // Use this for initialization
    void Start()
    {
        isDropped = false;
    }
    void Update()
    {
        if (isDropped == true)
        {
            //StartCoroutine(StartCoolTime());
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
        yield return new WaitForSeconds(2f);

        Transform tank = gameObject.transform.parent;
        if (gameObject.GetComponentInParent<Tank>().myStatus.itemHolder.Count != 0)
        {//2回呼び出されることがあり，その対策
            gameObject.GetComponentInParent<Tank>().myStatus.itemHolder.RemoveAt(0);
            end();
            foreach (Transform obj in tank)
            {
                if (obj.gameObject.layer == LayerMask.NameToLayer("Item"))
                {
                    obj.position += new Vector3(0, -1, 0);
                }
            }

        }
        else
        {
            Destroy(gameObject);
        }

    }
    void end()
    {
        Destroy(gameObject);
    }
    override protected void SettingObject(GameObject tankObject)
    {
        if (tankObject.layer == LayerMask.NameToLayer("Player"))
        {
            tankObject.GetComponent<Tank>().myStatus.itemHolder.Add(gameObject);

            tankObject.GetComponent<Tank>().Heal(10);
            //gameObject.transform.parent = tankObject.transform;
           // gameObject.transform.position = tankObject.transform.position + new Vector3(0, 5 + tankObject.GetComponent<Tank>().myStatus.itemHolder.Count, 0);
            gameObject.GetComponent<Item>().isDropped = true;
            GameObject obj = Instantiate(particle);
            obj.transform.parent = gameObject.transform;
            obj.transform.position = gameObject.transform.parent.position;
        }
    }
}

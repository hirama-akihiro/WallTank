using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Present : Item {
    private List<GameObject> subWeaponObjects;
    private Dictionary<SubWeaponType, GameObject> subWeapons;
    // Use this for initialization
    void Start () {
        subWeaponObjects = new List<GameObject>();
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/ShieldObject") as GameObject);
        //subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/TornadoObject") as GameObject);
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/CircleBombObject") as GameObject);
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/CrossBombObject") as GameObject);
        //subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/TackleObject") as GameObject);
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/MissileObject") as GameObject);

        //アイテムも出現
        //subWeaponObjects.Add(Resources.Load("Prefabs/Items/RapidFire") as GameObject);
        //subWeaponObjects.Add(Resources.Load("Prefabs/Items/SpeedUp") as GameObject);

        subWeapons = new Dictionary<SubWeaponType, GameObject>();
        subWeapons.Add(SubWeaponType.Shield, Resources.Load("Prefabs/SubWeapons/ShieldWeapon") as GameObject);
        //subWeapons.Add(SubWeaponType.Tornado, Resources.Load("Prefabs/SubWeapons/TornadoWeapon") as GameObject);
        subWeapons.Add(SubWeaponType.CircleBomb, Resources.Load("Prefabs/SubWeapons/CircleBombWeapon") as GameObject);
        subWeapons.Add(SubWeaponType.CrossBomb, Resources.Load("Prefabs/SubWeapons/CrossBombWeapon") as GameObject);
        subWeapons.Add(SubWeaponType.Missle, Resources.Load("Prefabs/SubWeapons/MissileWeapon") as GameObject);
        gameObject.transform.parent =SubWeaponManager.I.transform;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
    protected override void SettingObject(GameObject tankObject)
    {
        if (tankObject.layer== LayerMask.NameToLayer("Player"))
        {
            AudioManager.I.PlayAudio("get");
            RandomGetSubWeaopon(tankObject);
        }
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("MainWeapon"))
        {
            RandomActivateSubWeapon();
            AudioManager.I.PlayAudio("get");
            StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
            Destroy(gameObject);
            Destroy(c.gameObject);
        }
    }
    public void ActivateSubWeapon(SubWeaponType subWeaponType, Vector3 position, Quaternion rotation)
    {
        if (subWeaponType == SubWeaponType.None || subWeaponType == SubWeaponType.Tackle) { return; }
        Instantiate(subWeapons[subWeaponType], position, rotation);
    }
    public void RandomActivateSubWeapon()
    {
        GameObject subWeapon = subWeaponObjects[Random.Range(0, subWeaponObjects.Count)];
        GameObject addWeapon = Instantiate(subWeapon, gameObject.transform.position, Quaternion.identity) as GameObject;
        //マネージャーの子供にする．
        addWeapon.transform.parent = SubWeaponManager.I.transform;
    }
    public void GetSubWeapon(SubWeaponType subWeaponType,GameObject tankObject)
    {
        tankObject.GetComponent<Tank>().subWeaponType = subWeaponType;
    }
    public void RandomGetSubWeaopon(GameObject tankObject)
    {
        GameObject subWeapon = subWeaponObjects[Random.Range(0, subWeaponObjects.Count)];
        GetSubWeapon(subWeapon.GetComponent<SubWeaponObject>().getMyType(), tankObject);
    }
}

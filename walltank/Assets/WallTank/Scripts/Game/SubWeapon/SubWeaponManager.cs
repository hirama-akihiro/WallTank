using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum SubWeaponType { None, Shield, Tornado, Tackle, CircleBomb, CrossBomb, Missle }
public enum ItemType { SpeedUp, RapidFire }

public class SubWeaponManager : SingletonMonoBehavior<SubWeaponManager>
{
    private List<GameObject> subWeaponObjects;
    private Dictionary<SubWeaponType, GameObject> subWeapons;

	public float intervalTime = 1.5f;
	private float _intervalTime = 0.0f;

    // Use this for initialization
    void Start()
    {
        subWeaponObjects = new List<GameObject>();
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/ShieldObject") as GameObject);
        //subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/TornadoObject") as GameObject);
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/CircleBombObject") as GameObject);
        subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/CrossBombObject") as GameObject);
        //subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/TackleObject") as GameObject);
		subWeaponObjects.Add(Resources.Load("Prefabs/SubWeapons/MissileObject") as GameObject);
        subWeaponObjects.Add(Resources.Load("Prefabs/Items/Heal") as GameObject);
        //アイテムも出現
        //subWeaponObjects.Add(Resources.Load("Prefabs/Items/RapidFire") as GameObject);
        //subWeaponObjects.Add(Resources.Load("Prefabs/Items/SpeedUp") as GameObject);

        subWeapons = new Dictionary<SubWeaponType, GameObject>();
        subWeapons.Add(SubWeaponType.Shield, Resources.Load("Prefabs/SubWeapons/ShieldWeapon") as GameObject);
        subWeapons.Add(SubWeaponType.Tornado, Resources.Load("Prefabs/SubWeapons/TornadoWeapon") as GameObject);
        subWeapons.Add(SubWeaponType.CircleBomb, Resources.Load("Prefabs/SubWeapons/CircleBombWeapon") as GameObject);
        subWeapons.Add(SubWeaponType.CrossBomb, Resources.Load("Prefabs/SubWeapons/CrossBombWeapon") as GameObject);
		subWeapons.Add(SubWeaponType.Missle, Resources.Load("Prefabs/SubWeapons/MissileWeapon") as GameObject);
		//for (int i = 0; i < StageManager.I.stateMap.GetLength(0); ++i)
		//{
		//	for (int j = 0; j < StageManager.I.stateMap.GetLength(1); ++j)
		//	{
		//		if (StageManager.I.stateMap[i, j] != 0) { continue; }
		//		Vector3 setPos = StageManager.I.TOP_LEFT_POSITION + new Vector3((j) * StageManager.I.MASS_WIDTH, 0, -(i) * StageManager.I.MASS_HEIGHT);
		//		GameObject subWeapon = subWeaponObjects[Random.Range(0, subWeaponObjects.Count)];
		//		GameObject addWeapon = Instantiate(subWeapon, setPos, Quaternion.identity) as GameObject;
		//		addWeapon.transform.parent = transform;
		//		Debug.LogFormat(StageManager.I.CalcStateMaoIndex(addWeapon.transform.position).ToString());
		//	}
		//}

		SetSubWeapon(3, 3);
		SetSubWeapon(3, 9);
		SetSubWeapon(5, 3);
		SetSubWeapon(5, 9);

		_intervalTime = intervalTime;
    }

	void Update()
	{
		_intervalTime -= Time.deltaTime;
		if(_intervalTime < 0)
		{
			RandomSetSubWeapon();
			_intervalTime = intervalTime;
		}
	}
    

    public void ActivateSubWeapon(SubWeaponType subWeaponType, Transform transform, GamePad.Index index)
	{
		if (subWeaponType == SubWeaponType.None || subWeaponType == SubWeaponType.Tackle) { return; }
		GameObject subWeapon = Instantiate(subWeapons[subWeaponType], transform.position, transform.rotation) as GameObject;
		subWeapon.GetComponent<SubWeapon>().Init(index);

		if (subWeaponType == SubWeaponType.Shield) { subWeapon.transform.parent = transform; }
		else { subWeapon.transform.parent = this.transform; }
	}

	public void RandomSetSubWeapon()
	{
		int i = 0, j = 0;
		while(true)
		{
			i = UnityEngine.Random.Range(0, StageManager.I.StateMapHeight);
			j = UnityEngine.Random.Range(0, StageManager.I.StateMapWidth);
			if (StageManager.I.GetStateMapElement(i, j) ==  StateMapElement.NormalPlane) { break; }
		}
		SetSubWeapon(i, j);
	}

	private void SetSubWeapon(int i, int j)
	{
		Vector3 setPos = StageManager.I.TOP_LEFT_POSITION + new Vector3(j * StageManager.I.MASS_WIDTH, 0, -i * StageManager.I.MASS_HEIGHT);
		GameObject subWeapon = subWeaponObjects[UnityEngine.Random.Range(0, subWeaponObjects.Count)];
		GameObject addWeapon = Instantiate(subWeapon, setPos, Quaternion.identity) as GameObject;
		StageManager.I.SetStateMapElement(addWeapon.transform.position, StateMapElement.ItemSubweapon);
		addWeapon.transform.parent = transform;
	}
}

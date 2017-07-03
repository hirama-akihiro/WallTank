using UnityEngine;
using System.Collections;

public class CrossBombWeapon : SubWeapon {

	public float bombRange = 2;
	public GameObject explosion;

	// Use this for initialization
	void Start()
	{
		lifeTime = 3.0f;
		atkPower = 10.0f;
		subWeaponType = SubWeaponType.CrossBomb;

		// 座標の調整:地面に埋まるようにする
		transform.position = new Vector3(transform.position.x, -0.05f, transform.position.z);
		transform.Rotate(new Vector3(0, 180, 0));

		// StateMapの更新
		StageManager.I.SetStateMapElement(transform.position, StateMapElement.SubAttack);
		Destroy(gameObject, lifeTime);
	}

	// Update is called once per frame
	void Update () {
	
	}

	void OnDestroy()
	{
		ExplosionBomb();
	}

	private void RangeDamage(Vector3 pos)
	{
		// 一定範囲内の敵にダメージ
		foreach (GameObject tank in TankManager.I.TankObjects)
		{
			if (bombRange > Vector3.Distance(tank.transform.position, pos))
			{
				tank.GetComponent<Tank>().Damage(atkPower);
			}
		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag.Equals("Tank"))
		{
			Destroy(gameObject);
		}
	}

	private void ExplosionBomb()
	{
		// 爆破エフェクトを生成
		// 地雷の位置から十字にオブジェクトを生成していく
		AudioManager.I.PlayAudio("attack", 0.6f);
		GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
		RangeDamage(transform.position);
		if (SubWeaponManager.I)
		{
			explosionObject.transform.parent = SubWeaponManager.I.transform;
			Destroy(explosionObject.gameObject, 1.5f);
		}
		else
		{
			Destroy(explosionObject.gameObject);
		}

		if (!StageManager.I) { return; }
		if (!SubWeaponManager.I) { return; }
		float slideOffset = StageManager.I.MASS_WIDTH;
		for (int i = 1; i < 10; ++i)
		{
			Vector3 genePos = transform.position + new Vector3(-slideOffset * i, 0, 0);
			StateMapElement stateMapElement = StageManager.I.GetStateMapElement(genePos);
			if ((stateMapElement == StateMapElement.NormalPlane ||
				stateMapElement == StateMapElement.ItemSubweapon ||
				stateMapElement == StateMapElement.SubAttack)
				&& i != 0)
			{
				explosionObject = Instantiate(explosion, genePos, Quaternion.identity) as GameObject;
				RangeDamage(genePos);
				if(SubWeaponManager.I)
				{
					explosionObject.transform.parent = SubWeaponManager.I.transform;
					Destroy(explosionObject.gameObject, 1.5f);
				}
				else
				{
					Destroy(explosionObject.gameObject);
				}
			}
			else { break; }
		}

		for (int i = 1; i < 10; ++i)
		{
			Vector3 genePos = transform.position + new Vector3(slideOffset * i, 0, 0);
			StateMapElement stateMapElement = StageManager.I.GetStateMapElement(genePos);
			if ((stateMapElement == StateMapElement.NormalPlane ||
				stateMapElement == StateMapElement.ItemSubweapon ||
				stateMapElement == StateMapElement.SubAttack)
				&& i != 0)
			{
				explosionObject = Instantiate(explosion, genePos, Quaternion.identity) as GameObject;
				RangeDamage(genePos);
				if (SubWeaponManager.I)
				{
					explosionObject.transform.parent = SubWeaponManager.I.transform;
					Destroy(explosionObject.gameObject, 1.5f);
				}
				else
				{
					Destroy(explosionObject.gameObject);
				}
			}
			else { break; }
		}

		for (int i = 1; i < 10; ++i)
		{
			Vector3 genePos = transform.position + new Vector3(0, 0, -slideOffset * i);
			StateMapElement stateMapElement = StageManager.I.GetStateMapElement(genePos);
			if ((stateMapElement == StateMapElement.NormalPlane ||
				stateMapElement == StateMapElement.ItemSubweapon ||
				stateMapElement == StateMapElement.SubAttack)
				&& i != 0)
			{
				explosionObject = Instantiate(explosion, genePos, Quaternion.identity) as GameObject;
				RangeDamage(genePos);
				if (SubWeaponManager.I)
				{
					explosionObject.transform.parent = SubWeaponManager.I.transform;
					Destroy(explosionObject.gameObject, 1.5f);
				}
				else
				{
					Destroy(explosionObject.gameObject);
				}
			}
			else { break; }
		}
		for (int i = 1; i < 10; ++i)
		{
			Vector3 genePos = transform.position + new Vector3(0, 0, slideOffset * i);
			StateMapElement stateMapElement = StageManager.I.GetStateMapElement(genePos);
			if ((stateMapElement == StateMapElement.NormalPlane ||
				stateMapElement == StateMapElement.ItemSubweapon ||
				stateMapElement == StateMapElement.SubAttack)
				&& i != 0)
			{
				explosionObject = Instantiate(explosion, genePos, Quaternion.identity) as GameObject;
				RangeDamage(genePos);
				if (SubWeaponManager.I)
				{
					explosionObject.transform.parent = SubWeaponManager.I.transform;
					Destroy(explosionObject.gameObject, 1.5f);
				}
				else
				{
					Destroy(explosionObject.gameObject);
				}
			}
			else { break; }
		}

		// StateMapの更新
		if (!StageManager.I) { return; }
		StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
	}
}

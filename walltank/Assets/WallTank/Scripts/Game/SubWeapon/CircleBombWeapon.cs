using UnityEngine;
using System.Collections;

public class CircleBombWeapon : SubWeapon {

	public float bombRange = 2;
	public GameObject explosion;

	// Use this for initialization
	void Start () {
		lifeTime = 3.0f;
		atkPower = 10.0f;
		subWeaponType = SubWeaponType.CircleBomb;

		transform.position = new Vector3(transform.position.x, 0.8f, transform.position.z);

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

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag.Equals("MainWeapon"))
		{
			Destroy(collision.gameObject);
			Destroy(gameObject);
		}
	}

	private void RangeDamage()
	{
		// 一定範囲内の敵にダメージ
		if (!TankManager.I) { return; }
		foreach (GameObject tank in TankManager.I.TankObjects)
		{
			if (bombRange > Vector3.Distance(tank.transform.position, transform.position))
			{
				tank.GetComponent<Tank>().Damage(atkPower);
			}
		}
	}

	private void ExplosionBomb()
	{
		// 爆破エフェクトを生成
		AudioManager.I.PlayAudio("attack", 0.6f);
		GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;

		if(SubWeaponManager.I)
		{
			explosionObject.transform.parent = SubWeaponManager.I.transform;
			Destroy(explosionObject.gameObject, 1.5f);
		}
		else
		{
			Destroy(explosionObject.gameObject);
		}
		RangeDamage();

		// StateMapの更新
		if (!StageManager.I) { return; }
		StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
	}
}

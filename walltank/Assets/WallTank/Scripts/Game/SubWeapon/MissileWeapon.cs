using UnityEngine;
using System.Collections;

public class MissileWeapon : SubWeapon {

	private float ceilingHeight = 20.0f;
	private float upTime = 1f;
	private float downTime = 2f;
	private float moveSpeed = 20.0f;
	private bool isUp = true;

	private Transform targetPosition;
	public GameObject explosion;
	public float bombRange = 2;

	// Use this for initialization
	void Start () {
		lifeTime = upTime + downTime;
		atkPower = 10.0f;
		subWeaponType = SubWeaponType.Missle;

		// 対象となるターゲットを選択
		// 現時点では自分から最も遠いプレイヤー
		double maxDist = double.MinValue;
		foreach(GameObject tankPos in TankManager.I.TankObjects)
		{
			double dist = Vector3.Distance(tankPos.transform.position, transform.position);
			if(dist > maxDist)
			{
				maxDist = dist;
				targetPosition = tankPos.transform;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// ミサイルが上昇
		if(isUp)
		{
			transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0);

			// 下降モードに変更
			if (transform.position.y > ceilingHeight)
			{
				isUp = false;
				transform.position = new Vector3(targetPosition.position.x, ceilingHeight, targetPosition.position.z);
				transform.Rotate(180, 0, 0);
			}
		}

		// ミサイルがターゲット上空から下降
		if(!isUp)
		{
			transform.position -= new Vector3(0, moveSpeed * Time.deltaTime * 0.5f, 0);

			// 地面衝突時に爆破エフェクトと共に消滅
			if(transform.position.y < 1)
			{
				// 爆破エフェクトを生成
				GameObject explosionObject = Instantiate(explosion, transform.position, Quaternion.identity) as GameObject;
				Destroy(explosionObject.gameObject, 1.5f);

				// 一定範囲内の敵にダメージ
				foreach (GameObject tank in TankManager.I.TankObjects)
				{
					if (bombRange > Vector3.Distance(tank.transform.position, transform.position))
					{
						tank.GetComponent<Tank>().Damage(atkPower);
					}
				}
				Destroy(gameObject);
			}
		}
	}
}

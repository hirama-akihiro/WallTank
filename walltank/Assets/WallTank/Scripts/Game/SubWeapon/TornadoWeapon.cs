using UnityEngine;
using System.Collections;

public class TornadoWeapon : SubWeapon {

	public float moveSpeed = 10.0f;
	public float holdtime = 3f;

	private bool isHitWall = false;

	// Use this for initialization
	void Start () {
		lifeTime = 5.0f;
		atkPower = 0.0f;
		subWeaponType = SubWeaponType.Tornado;

		AudioManager.I.PlayAudio("shippu");

		// 初期速度
		//GetComponent<Rigidbody>().AddForce(transform.forward * moveSpeed, ForceMode.VelocityChange);

		//Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += transform.forward * moveSpeed * Time.deltaTime;

		float _x = Mathf.Clamp(transform.position.x, -11, 11);
		float _z = Mathf.Clamp(transform.position.z, -6.5f, 6.5f);
		transform.position = new Vector3(_x, 0, _z);

		// 壁にぶつかったら暫く固定
		if (holdtime < 1.0 || isHitWall)
		{
			moveSpeed = 0.0f;
		}

		if (holdtime < 0.0)
		{
			foreach (Transform child in gameObject.transform)
			{
				if (child.tag.Equals("Tank"))
				{
					child.gameObject.GetComponent<Tank>().isFreeze = false;
					child.parent = TankManager.I.transform;
				}
			}
			Destroy(gameObject);
		}
		holdtime -= Time.deltaTime;
	}

	void OnDestroy()
	{
	}

	void OnTriggerEnter(Collider collider)
	{
		// タンクに衝突したらタンクを巻き込む
		if(collider.gameObject.tag.Equals("Tank") || collider.gameObject.tag.Equals("MainWeapon"))
		{
			if (collider.tag.Equals("Tank"))
			{
				collider.gameObject.GetComponent<Tank>().isFreeze = true;
			}
			collider.transform.parent = transform;
		}

		// 壁に衝突したら,巻き込んだGameObjectを開放して自身を消滅
		if (collider.gameObject.layer.Equals(LayerMask.NameToLayer("Wall")))
		{
			isHitWall = true;
			holdtime = 1.0f;
		}
	}
}

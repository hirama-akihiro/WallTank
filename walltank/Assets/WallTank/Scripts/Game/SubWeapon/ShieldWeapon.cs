using UnityEngine;
using System.Collections;

public class ShieldWeapon : SubWeapon {

	[SerializeField]
	private float hp = 5;

	// Use this for initialization
	void Start () {
		lifeTime = 4.0f;
		atkPower = 0.0f;
		subWeaponType = SubWeaponType.Shield;

		transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

		Destroy(gameObject, lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Init(GamePad.Index index)
	{
		if (!TankManager.I) { return; }
		TankManager.I.TankObjects[(int)index - 1].GetComponent<Tank>().ActivateInvincible(2.0f);
	}

	void OnDestroy()
	{
		// StateMapの更新
		if (!StageManager.I) { return; }
		StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);
	}
}

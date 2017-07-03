using UnityEngine;
using System.Collections;

public class SubWeapon : MonoBehaviour {

	[SerializeField]
	protected float lifeTime;
	[SerializeField]
	protected float atkPower;
	[SerializeField]
	protected SubWeaponType subWeaponType;

	public virtual void Init(GamePad.Index index) { }
}

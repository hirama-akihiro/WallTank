using UnityEngine;
using System.Collections;

public class SubWeaponObject : GetableObject {
	[SerializeField]
	protected SubWeaponType subWeaponType;
	/// <summary>
	/// サブウェポン設置時の見た目オブジェクト
	/// </summary>
	public GameObject subWeaponAppearObject;

	public GameObject subWeaponMagicCircle;

	void Start()
	{
		if(subWeaponMagicCircle!= null)
		{
			GameObject subWeaponMagicCircleObj = Instantiate(subWeaponMagicCircle, new Vector3(transform.position.x, 0.6f, transform.position.z), Quaternion.identity) as GameObject;
			subWeaponMagicCircleObj.transform.parent = transform;
		}
	}

	void Update()
	{
		if(subWeaponAppearObject != null)
		{
			subWeaponAppearObject.transform.position = new Vector3(
				subWeaponAppearObject.transform.position.x,
				PingPong(1, 1, 1) + 1f,
				subWeaponAppearObject.transform.position.z);
		}
	}

	/// <summary>
	/// 取得したオブジェクト(サブウェポン，パワーアップアイテム)をプレイヤーに設定
	/// </summary>
	/// <param name="tankObject"></param>
	/// <param name="setObject"></param>
	protected override void SettingObject(GameObject tankObject)
	{
		tankObject.GetComponent<Tank>().subWeaponType = subWeaponType;
		RuleManager.I.IsCheckGameFinish();
	}

	protected float PingPong(float moveSpeed, float moveTime, float range)
	{
		return Mathf.PingPong(Time.time * Mathf.Abs(moveSpeed), Mathf.Abs(range));
	}
    public SubWeaponType getMyType()
    {
        return subWeaponType;
    }
}

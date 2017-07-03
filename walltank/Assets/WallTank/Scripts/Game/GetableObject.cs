using UnityEngine;
using System.Collections;

public class GetableObject : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other)
	{
		if (!other.gameObject.tag.Equals("Tank")) { return; }

		// 自身の情報をプレイヤーに設定
		AudioManager.I.PlayAudio("get");
		SettingObject(other.gameObject);

		// StateMapの更新
		StageManager.I.SetStateMapElement(transform.position, StateMapElement.NormalPlane);

		Destroy(gameObject);
	}

	/// <summary>
	/// 取得したオブジェクト(サブウェポン，パワーアップアイテム)をプレイヤーに設定
	/// </summary>
	/// <param name="tankObject"></param>
	/// <param name="setObject"></param>
	protected virtual void SettingObject(GameObject tankObject) { }
}

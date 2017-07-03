using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthUIDirection : MonoBehaviour{

	public Image healthUIImage;
	public Tank tank;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		healthUIImage.fillAmount = tank.myStatus.ratioHP;
	}
}

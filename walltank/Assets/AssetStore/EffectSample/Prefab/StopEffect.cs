using UnityEngine;
using System.Collections;

public class StopEffect : MonoBehaviour {

	float stopTime = 1.0f;
	bool isStop = false;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		stopTime -= Time.deltaTime;

		if(stopTime < 0 && !isStop)
		{
			foreach(Transform child in transform)
			{
				ParticleSystem pSystem = child.GetComponent<ParticleSystem>(); 
				child.GetComponent<ParticleSystem>().Pause();
			}
			isStop = true;
		}
	}
}

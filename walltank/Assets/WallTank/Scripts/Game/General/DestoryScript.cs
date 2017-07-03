using UnityEngine;
using System.Collections;

public class DestoryScript : MonoBehaviour {

    private float time;
    public float deadLine;

	// Use this for initialization
	void Start () {
        time = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= deadLine)
            Destroy(gameObject);
	}
}

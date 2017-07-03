using UnityEngine;
using System.Collections;

public class DangerMark : MonoBehaviour {

    private float time;
    private bool flash;

	// Use this for initialization
	void Start () {
        flash = true;
        time = 0;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        if (time >= 5.0f)
        {
            Destroy(this.gameObject);
        }
	}
}

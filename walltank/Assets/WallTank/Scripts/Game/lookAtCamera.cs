using UnityEngine;
using System.Collections;

public class lookAtCamera : MonoBehaviour {

    private GameObject camera;
    private Vector3 target;

	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Camera");
        target = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + 1.0f, this.gameObject.transform.position.z + 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
        target = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - 1.0f, this.gameObject.transform.position.z + 1.0f);
        this.transform.LookAt(target);//カメラのほうを見る
	}
}

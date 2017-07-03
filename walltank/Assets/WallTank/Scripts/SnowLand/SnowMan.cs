using UnityEngine;
using System.Collections;

public class SnowMan : MonoBehaviour {

    public Transform ballSpawnPosition;
    public GameObject snowBall;

    private float time;
    private enum State { idle, attack };
    //private float shotTime;
    State state;
    private GameObject obj;
    private float rotateSpeed;

	// Use this for initialization
    void Start(){
        time = 0.0f;
    }
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        switch (state)
        {
            case State.idle:
                {
                    if (time >= 10.0f)
                    {
                        time = 0.0f;
                        //shotTime = Random.Range(1, 5);
                        state = State.attack;
                        rotateSpeed = Random.Range(1.0f, 5.0f);
                    }
                    break;
                }
            case State.attack://雪玉投げ
                {
                    if (time <= 3.0f)
                    {
                        gameObject.transform.Rotate(0, rotateSpeed, 0.0f, Space.World);
                        ballSpawnPosition.transform.Rotate(0, rotateSpeed, 0.0f, Space.World);
                    }
                    else
                    {
                        obj = (GameObject)Instantiate(snowBall, ballSpawnPosition.transform.position, ballSpawnPosition.transform.rotation);
                        obj.transform.parent = this.transform;
                        state = State.idle;
                        time = 0.0f;
                    }
                    break;
                }
        }
	}
}

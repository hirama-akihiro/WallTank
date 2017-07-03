using UnityEngine;
using System.Collections;

public class WorpObject : MonoBehaviour {

    private bool isSetPlayer;
    private int state;

	// Use this for initialization
	void Start () {
        isSetPlayer = true;
        state = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (isSetPlayer)
        {
            switch (state)
            {
                case 1:
                    {
                        this.transform.Translate(0, 0.08f, 0);
                        if (this.transform.position.z >= 9.7f)
                            state = 2;
                        break;
                    }
                case 2:
                    {
                        this.transform.Translate(0.12f, 0, 0);
                        if (this.transform.position.x >= 9.0f)
                            state = 3;
                        break;
                    }
                case 3:
                    {
                        this.transform.Translate(0, -0.08f, 0);
                        if (this.transform.position.z <= 5.4f)
                            state = 4;
                        break;
                    }
                case 4:
                    {
                        Destroy(this.gameObject);
                        break;
                    }
            }
        }
	}

    void setPlayer(GameObject player)
    {
        isSetPlayer = true;
    }
}

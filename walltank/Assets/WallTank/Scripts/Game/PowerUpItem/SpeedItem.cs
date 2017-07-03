using UnityEngine;
using System.Collections;

public class SpeedItem : Item {
    // Use this for initialization
	void Start () {
        isDropped = false;
        count = 200;
		myStatus = new Status(0f, 10f, 1f, true, false, 0);
    }

}
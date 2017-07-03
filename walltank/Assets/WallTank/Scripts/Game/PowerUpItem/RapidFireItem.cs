using UnityEngine;
using System.Collections;

public class RapidFireItem : Item {

	// Use this for initialization
	void Start () {
        isDropped = false;
        count = 200;
		myStatus = new Status(0f, 0f, 0.1f, true, false, 0);
    }
}
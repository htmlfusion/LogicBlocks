using UnityEngine;
using System.Collections;
using System;

public class Autodestruct : MonoBehaviour {

    int createdAt;

    public int destroyAfter = 5;

	// Use this for initialization
	void Start () {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        createdAt = (int)t.TotalSeconds;
    }

    // Update is called once per frame
    void Update () {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int now = (int)t.TotalSeconds;
        if ((now - createdAt) > destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}

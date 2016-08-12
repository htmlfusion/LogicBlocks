using UnityEngine;
using System.Collections;
using System;

public class Autodestruct : MonoBehaviour {

    int createdAt;

    public int destroyAfter = 10;

    // Use this for initialization
    void Start () {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        createdAt = (int)t.TotalSeconds;
    }

    int lifeSpan()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int now = (int)t.TotalSeconds;
        return now - createdAt;
    }

    // Update is called once per frame
    void Update ()
    {
        int alive = lifeSpan();
        if (alive > destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;
using System.Collections;
using System;

public class Autodestruct : MonoBehaviour {

    int createdAt;

    public int destroyAfter = 5000;

    public int destroyBefore = 500;

    // Use this for initialization
    void Start () {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        createdAt = (int)t.TotalMilliseconds;
    }

    int lifeSpan()
    {
        TimeSpan t = DateTime.UtcNow - new DateTime(1970, 1, 1);
        int now = (int)t.TotalMilliseconds;
        return now - createdAt;
    }

    void OnCollisionEnter(Collision collision)
    {
        int alive = lifeSpan();
        if (alive > 0 && alive < destroyBefore) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if ((lifeSpan()) > destroyAfter)
        {
            Destroy(gameObject);
        }
    }
}

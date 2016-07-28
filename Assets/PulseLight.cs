using UnityEngine;
using System.Collections;

public class PulseLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Light>().range = Mathf.Max(Mathf.PingPong(Time.time * 2, 2.25f), 0.8f);
    }
}

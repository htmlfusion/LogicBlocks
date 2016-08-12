using UnityEngine;
using System.Collections;

public class TutorialAssetController : MonoBehaviour {
    public float speed = 10f;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        this.gameObject.transform.Rotate(Vector3.up, speed * Time.deltaTime);
    }
}

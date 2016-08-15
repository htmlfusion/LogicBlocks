using UnityEngine;
using System.Collections;

public class MenuButtonController : MonoBehaviour {
    private Quaternion rotate;
    private Vector3 updatePosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rotate = Quaternion.LookRotation(Camera.main.transform.forward, Camera.main.transform.up);
        this.transform.rotation = rotate;
        updatePosition = GameObject.Find("tutorial_animation").transform.position;
        updatePosition.y = updatePosition.y + 0.5f;
        this.transform.position = updatePosition;
        
    }

    void OnSelect() {
        Debug.Log(this.gameObject.tag);
    }
}

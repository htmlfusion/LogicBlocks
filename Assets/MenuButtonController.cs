using UnityEngine;
using System.Collections;

public class MenuButtonController : MonoBehaviour {
    private Vector3 rotate;
    private Vector3 updatePosition;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        rotate = Camera.main.transform.position;
        this.transform.LookAt(rotate);
        this.transform.RotateAround(this.transform.position, this.transform.up, 180f);
        updatePosition = GameObject.Find("tutori al_animation").transform.position;
        updatePosition.y = updatePosition.y + 1.1f;
        this.transform.position = updatePosition;
        
    }

    void OnSelect() {
        Debug.Log(this.gameObject.tag);
    }
}

using UnityEngine;
using System.Collections;

public class BlockButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        string sideName = gameObject.name.Replace("_Cylinder", "");
        transform.parent.GetComponent<LogicBlockController>().SideClicked(sideName);
    }

}

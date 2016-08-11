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
        LogicBlockController controller = transform.parent.GetComponent<LogicBlockController>();
        if (controller.BlockType != LogicBlockController.BlockTypes.End &&
            controller.BlockType != LogicBlockController.BlockTypes.Start)
        {
            string sideName = gameObject.name.Replace("_Cylinder", "");
            bool fired = controller.SideClicked(sideName);
            if (fired)
            {
                Destroy(collision.gameObject);
            }
        }
    }

}

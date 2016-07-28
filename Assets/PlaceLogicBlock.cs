using UnityEngine;
using System.Collections;

namespace HoloToolkit.Unity
{

    public class PlaceLogicBlock : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnSelect()
        {
            if (GazeManager.Instance.Hit)
            {
                Vector3 hit = CursorManager.Instance.gameObject.transform.position;
                GameObject LogicBlock = Resources.Load<GameObject>("LogicBlock");
                GameObject block = GameObject.Instantiate(LogicBlock) as GameObject;
                Bounds blockBounds = block.GetComponent<BoxCollider>().bounds;
                block.transform.position = new Vector3(hit.x, hit.y, hit.z);
                block.transform.position = block.transform.position + GazeManager.Instance.HitInfo.normal * blockBounds.size.x/2;
            }
        }
    }
}
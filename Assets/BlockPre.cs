using UnityEngine;
using System.Collections;

namespace HoloToolkit.Unity
{

    public class BlockPre : MonoBehaviour
    {

        public float DistanceFromCollision = 0.01f;
        GameObject BlockPreview;
        GameObject block;
        Bounds blockBounds;

        // Use this for initialization
        void Start()
        {
            //GameObject LogicBlock = Resources.Load<GameObject>("LogicBlock");
            //block = GameObject.Instantiate(LogicBlock) as GameObject;
            //block
            BlockPreview = Resources.Load<GameObject>("BlockPreview");
            block = GameObject.Instantiate(BlockPreview) as GameObject;
        }

        // Update is called once per frame
        void Update()
        {
            if (HandsManager.Instance.HandDetected)
            {
                block.SetActive(true);
            }
            else
            {
                block.SetActive(false);
            }

            if (GazeManager.Instance.Hit && GazeManager.Instance.HitInfo.collider.name.StartsWith("LogicBlock"))
            {
                block.SetActive(true);
                Vector3 hit = CursorManager.Instance.gameObject.transform.position;
                blockBounds = GazeManager.Instance.HitInfo.collider.GetComponent<BoxCollider>().bounds;
                block.transform.position = GazeManager.Instance.HitInfo.collider.bounds.center;
                block.transform.position = block.transform.position + GazeManager.Instance.HitInfo.normal * blockBounds.size.x;
                //block.transform.up = GazeManager.Instance.HitInfo.normal;
            }
            else
            {
                block.SetActive(false);
            }
        }


    }

}

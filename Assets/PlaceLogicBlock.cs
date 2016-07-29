using UnityEngine;
using System.Collections;

namespace HoloToolkit.Unity
{

    public class PlaceLogicBlock : MonoBehaviour
    {

        private GameObject startBlock;
        private GameObject endBlock;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (startBlock && endBlock)
            {

            }
        }

        GameObject CreateBlock()
        {
            GameObject LogicBlock = Resources.Load<GameObject>("LogicBlock");
            GameObject block = GameObject.Instantiate(LogicBlock) as GameObject;
            return block;
        }

        GameObject PlaceBlock(GameObject block)
        {
            Vector3 hit = CursorManager.Instance.gameObject.transform.position;
            Bounds blockBounds = block.GetComponent<BoxCollider>().bounds;
            block.transform.position = new Vector3(hit.x, hit.y, hit.z);
            block.transform.position = block.transform.position + GazeManager.Instance.HitInfo.normal * blockBounds.size.x / 2;
            return block;
        }

        GameObject CreateAndPlace()
        {
            GameObject block = CreateBlock();
            PlaceBlock(block);
            return block;
        }

        void OnSelect()
        {
            if (GazeManager.Instance.Hit)
            {
                CreateAndPlace();
            }
        }

        public void SetStartBlock()
        {
            if (startBlock)
            {
                PlaceBlock(startBlock);
            } else
            {
                startBlock = CreateAndPlace();
                startBlock.GetComponent<LogicBlockController>().BlockType = LogicBlockController.BlockTypes.Start;
            }
        }

        public void SetEndBlock()
        {
            if (endBlock)
            {
                PlaceBlock(endBlock);
            }
            else
            {
                endBlock = CreateAndPlace();
                endBlock.GetComponent<LogicBlockController>().BlockType = LogicBlockController.BlockTypes.End;
            }
        }

    }
}
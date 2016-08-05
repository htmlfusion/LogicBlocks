using UnityEngine;

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
                KickOff();
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
            block.transform.up = GazeManager.Instance.HitInfo.normal;
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

        void KickOff()
        {
            LogicBlockController controller = startBlock.GetComponent<LogicBlockController>();
            if (startBlock && endBlock && controller.BallCount() == 0)
            {
                controller.ShootBall("Top");
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
                startBlock.tag = "StartBlock";
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
                endBlock.tag = "EndBlock";
                endBlock.GetComponent<LogicBlockController>().BlockType = LogicBlockController.BlockTypes.End;
            }
        }

        public void FlipSide()
        {
            GameObject focused = GazeManager.Instance.FocusedObject;
            if (focused.name.StartsWith("LogicBlock"))
            {
                Vector3 hitNormal = GazeManager.Instance.HitInfo.normal;
                string sideName = focused.GetComponent<LogicBlockController>().NormalToSide(hitNormal);
                if (sideName != null)
                {
                    focused.GetComponent<LogicBlockController>().FlipSide(sideName);
                }
            }
        }

    }
}
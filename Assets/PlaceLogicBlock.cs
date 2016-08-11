using UnityEngine;

namespace HoloToolkit.Unity
{

    public class PlaceLogicBlock : Singleton<PlaceLogicBlock>
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

        public GameObject PlaceBlock(GameObject block)
        {
                 
                 Vector3 hit = CursorManager.Instance.gameObject.transform.position;
                 Bounds blockBounds = block.GetComponent<BoxCollider>().bounds;
                 block.transform.position = new Vector3(hit.x, hit.y, hit.z);
                 block.transform.position = block.transform.position + GazeManager.Instance.HitInfo.normal * blockBounds.size.x / 2;
                 block.transform.up = GazeManager.Instance.HitInfo.normal;

                 if(!GazeManager.Instance.HitInfo.collider.name.StartsWith("LogicBlock"))
                 {
                     Quaternion rotate;
                     rotate = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
                     rotate.x = block.transform.rotation.x;
                     rotate.z = block.transform.rotation.z;
                     block.transform.rotation = rotate;
                 }
                 else
                 {
                     Vector3 rotate;
                     rotate = GazeManager.Instance.HitInfo.transform.eulerAngles;
                     block.transform.rotation = Quaternion.Euler(rotate);
                 }
                 
                 
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
                controller.ShootBall("Front");
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
                LogicBlockController controller = startBlock.GetComponent<LogicBlockController>();
                controller.BlockType = LogicBlockController.BlockTypes.Start;
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
                LogicBlockController controller = endBlock.GetComponent<LogicBlockController>();
                controller.BlockType = LogicBlockController.BlockTypes.End;
                controller.SetSideBehavior("Front", LogicBlockController.Behaviors.Trigger);
                controller.SetSideBehavior("Left", LogicBlockController.Behaviors.Trigger);
                controller.SetSideBehavior("Right", LogicBlockController.Behaviors.Trigger);
                controller.SetSideBehavior("Back", LogicBlockController.Behaviors.Trigger);
            }
        }

        public void FlipSide()
        {
            GameObject focused = GazeManager.Instance.FocusedObject;
            if (focused.name.StartsWith("LogicBlock"))
            {
                Vector3 hitNormal = GazeManager.Instance.HitInfo.normal.normalized;
                string sideName = focused.GetComponent<LogicBlockController>().NormalToSide(hitNormal);
                if (sideName != null)
                {
                    focused.GetComponent<LogicBlockController>().FlipSide(sideName);
                }
            }
        }

        public void DeleteBlock()
        {
            if(GazeManager.Instance.HitInfo.collider.name.StartsWith("LogicBlock"))
            {
                Destroy(GazeManager.Instance.HitInfo.collider.gameObject);
            }
        }

    }
}
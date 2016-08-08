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
            Quaternion rotate;
            Vector3 hit = CursorManager.Instance.gameObject.transform.position;
            Bounds blockBounds = block.GetComponent<BoxCollider>().bounds;
            block.transform.position = new Vector3(hit.x, hit.y, hit.z);
            block.transform.position = block.transform.position + GazeManager.Instance.HitInfo.normal * blockBounds.size.x / 2;
            block.transform.up = GazeManager.Instance.HitInfo.normal;
            
            if(!GazeManager.Instance.HitInfo.collider.name.StartsWith("LogicBlock"))
            {
                rotate = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
                rotate.x = block.transform.rotation.x;
                rotate.z = block.transform.rotation.z;
            }
            else
            {
                rotate = Quaternion.Euler(0, GazeManager.Instance.HitInfo.collider.transform.eulerAngles.y, 0);
            }
            block.transform.rotation = rotate;

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

    }
}
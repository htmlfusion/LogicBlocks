using UnityEngine;
using System.Collections;

namespace HoloToolkit.Unity
{

    public class BlockPre : MonoBehaviour
    {

        public float DistanceFromCollision = 0.01f;
        GameObject BlockPreview;
        GameObject block;
        Vector3 blockBounds;

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
            if (HandsManager.Instance.HandDetected && !TutorialManager.Instance.menuOn)
            {
                Quaternion rotate;
                block.SetActive(true);
                Vector3 hit = CursorManager.Instance.gameObject.transform.position;
                blockBounds = block.transform.localScale;
                block.transform.position = new Vector3(hit.x, hit.y, hit.z);
                block.transform.position = block.transform.position + GazeManager.Instance.HitInfo.normal * blockBounds.x / 2;
                block.transform.up = GazeManager.Instance.HitInfo.normal;

                if (GazeManager.Instance.Hit && GazeManager.Instance.HitInfo.collider != null)
                {
                    if (GazeManager.Instance.HitInfo.collider.name.StartsWith("LogicBlock"))
                    {
                        rotate = Quaternion.LookRotation(GazeManager.Instance.HitInfo.transform.forward, GazeManager.Instance.HitInfo.transform.up);
                    }
                    else
                    {
                        rotate = Quaternion.LookRotation(-Camera.main.transform.forward, Camera.main.transform.up);
                        rotate.x = CursorManager.Instance.gameObject.transform.rotation.x;
                        rotate.z = CursorManager.Instance.gameObject.transform.rotation.z;
                    }

                    block.transform.rotation = rotate;
                }
                
            }
            else
             {
                 block.SetActive(false);
             }
        }


    }

}

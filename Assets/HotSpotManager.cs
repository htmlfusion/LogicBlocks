using UnityEngine;
using System.Collections;

namespace HoloToolkit.Unity
{

    public class HotSpotManager : MonoBehaviour
    {

        private bool ready = false;
        private bool dropped = false;
        private float maxDistance = 10;

        // Use this for initialization
        void Start()
        {
            print("Starting");
            // Wait 10 seconds, cause why not. Also, I'm not sure how to wait for the mesh manager
            StartCoroutine(Wait(5));
        }

        // Update is called once per frame
        void Update()
        {
            if (ready) {
                print("Droping hotspot");
                DropHotSpot();
                ready = false;
            }

        }

        IEnumerator Wait(int delay)
        {
            print(Time.time);
            yield return new WaitForSeconds(delay);
            ready = true;
            print(Time.time);
        }

        void DropHotSpot()
        {
            // Make sure we have the spatial mapping manager
            if (SpatialMappingManager.Instance != null)
            {

                // Do a raycast into the world that will only hit the Spatial Mapping mesh.
                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;
                var down = new Vector3(0, -1, 0);

                gazeDirection.y = 0;
                print(headPosition);
                print(gazeDirection);
                print(down);
                RaycastHit hitInfo;
                // Raycast to the user's feet
                if (Physics.Raycast(headPosition, down, out hitInfo,
                    30.0f))
                {
                    print ("hit");
                    // Move this object to the ground below the player
                    // then translate it forwward in the ray direction
                    this.transform.position = hitInfo.point;
                    this.transform.Translate(gazeDirection * 2);
                }
                dropped = true;
            }
        }
    }
}
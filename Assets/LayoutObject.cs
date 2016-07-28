using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace HoloToolkit.Unity
{

    public class LayoutObject : MonoBehaviour
    {


        /// <summary>
        /// Determines which plane types should be rendered.
        /// </summary>
        [Tooltip("Select surface type to attatch object to.")]
        public PlaneTypes targetType =
            (PlaneTypes.Wall | PlaneTypes.Floor | PlaneTypes.Ceiling | PlaneTypes.Table);

        [Tooltip("Object Y offset from plane's center [-.5,.5]")]
        public float yPosition = 0;

        [Tooltip("Object X offset from plane's center [-.5,.5]")]
        public float xPosition = 0;

        [Tooltip("Maximum distance the plane can be from the user in meters, -1 for no limit")]
        public float maxDistance = 2;

        [Tooltip("Align with normal")]
        public bool alignWithNormal = true;

        [Tooltip("How much time (in seconds) that the SurfaceObserver will run after being started; used when 'Limit Scanning By Time' is checked.")]
        public float scanTime = 30.0f;


        private bool objectPlaced = false;

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            if ((Time.time - SpatialMappingManager.Instance.StartTime) < scanTime)
            {
                return;
            }

            // Object will only be placed once
            if (!objectPlaced)
            {
                PlaceObject();
                List<GameObject> vertexRemoval = new List<GameObject>();
                SpatialMappingManager.Instance.StopObserver();

                GameObject cutHole = GameObject.Find("CutHole");
                cutHole.transform.position = transform.position;
                vertexRemoval.Add(cutHole);
                RemoveSurfaceVertices.Instance.RemoveSurfaceVerticesWithinBounds(vertexRemoval);
            }
        }


        void PlaceObject()
        {
            // Make sure we have the spatial mapping manager
            if (SpatialMappingManager.Instance != null)
            {

                // Do a raycast into the world that will only hit the Spatial Mapping mesh.
                var headPosition = Camera.main.transform.position;
                var gazeDirection = Camera.main.transform.forward;

                Vector3 direction;

                switch (targetType)
                {
                    case PlaneTypes.Wall:
                        direction = new Vector3(0, 0, 1);
                        break;
                    case PlaneTypes.Floor:
                        direction = new Vector3(0, -1, 0);
                        break;
                    default:
                        direction = new Vector3(0, 0, 1);
                        break;
                }

                Vector3 worldDirection = Camera.main.transform.TransformDirection(direction);
                
                RaycastHit hitInfo;
                // Raycast to the user's feet
                if (Physics.Raycast(headPosition, worldDirection, out hitInfo,
                    30.0f))
                {
                    // Place the cursor at the calculated position.
                    transform.position = hitInfo.point;

                    // Orient the cursor to match the surface being gazed at.
                    transform.forward = hitInfo.normal * -1;
                }
                objectPlaced = true;
            }
        }
    }
}
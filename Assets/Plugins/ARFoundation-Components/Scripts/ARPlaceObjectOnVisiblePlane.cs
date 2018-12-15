using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded
{

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlaceObjectOnVisiblePlane : MonoBehaviour
    {

        [SerializeField]
        [EnumMask]
        private PlaneAlignment planeAlignment = PlaneAlignment.Horizontal;

        [SerializeField]
        private GameObject objectToPlace;

        [SerializeField]
        private bool placeMultiple;

        public delegate void PlaneUpdatedEvent(bool planeVisible, Pose pose);
        public delegate void GameObjectPlacedEvent(GameObject gameObject);

        public event PlaneUpdatedEvent PlaneUpdated;
        public event GameObjectPlacedEvent ObjectPlaced;

        [HideInInspector]
        public ARSessionOrigin sessionOrigin;

        [HideInInspector]
        public ARPlaneManager planeManager;

        [HideInInspector]
        public Camera childCamera;

        private void Awake()
        {

            sessionOrigin = gameObject.GetComponent<ARSessionOrigin>();
            planeManager = gameObject.GetComponent<ARPlaneManager>();
            childCamera = gameObject.GetComponentInChildren<Camera>();

        }

        private void Start()
        {

            if (ARSubsystemManager.systemState == ARSystemState.None ||
                ARSubsystemManager.systemState == ARSystemState.Unsupported)
            {

                enabled = false;

            }

        }

        private void Update()
        {

            if (planeManager.enabled)
            {

                Pose pose;

                bool planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out pose);

                if (objectToPlace)
                {

                    PlaceObjectOnPlane();

                }

                if (PlaneUpdated != null)
                {

                    PlaneUpdated(planeVisible, pose);

                }

            }

        }

        private void PlaceObjectOnPlane()
        {

            Pose pose;

            ARPlane plane;

            if (ARFoundationExtensions.HasTouchedPlane(sessionOrigin, planeManager, planeAlignment, out pose, out plane))
            {

                var spawnedGameObject = Instantiate(objectToPlace, pose.position, pose.rotation);

                if (plane.boundedPlane.Alignment == PlaneAlignment.Horizontal)
                {

                    RotateObjectTowardsCamera(spawnedGameObject.transform, childCamera);

                }

                if (ObjectPlaced != null)
                {

                    ObjectPlaced(spawnedGameObject);

                }

                if (!placeMultiple)
                {

                    planeManager.enabled = false;

                    ARFoundationExtensions.RemoveAllSpawnedPlanesFromScene();

                }

            }

        }

        private static void RotateObjectTowardsCamera(Transform transform, Camera camera)
        {

            Vector3 previousRotation = transform.rotation.eulerAngles;

            Vector3 lookAtPosition = transform.position + camera.transform.rotation * Vector3.forward;
            lookAtPosition.y = 0;

            transform.LookAt(lookAtPosition);

            transform.rotation = Quaternion.Euler(previousRotation.x, transform.rotation.eulerAngles.y, previousRotation.z);

        }

    }

}

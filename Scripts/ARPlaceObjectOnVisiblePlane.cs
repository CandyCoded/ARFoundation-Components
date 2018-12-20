using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
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
        private bool showBeforePlacing;

        [SerializeField]
        private bool spawnMultiple;

        private GameObject showBeforePlacingGameObject;

        public delegate void PlaneUpdatedEvent(bool planeVisible, Pose pose, ARPlane plane);
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

                bool planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out Pose lookingAtPose, out ARPlane lookingAtPlane);

                if (showBeforePlacing && showBeforePlacingGameObject)
                {

                    ShowObjectOnPlane(lookingAtPose, lookingAtPlane);

                    if (ARFoundationExtensions.HasTouchedPlane(sessionOrigin, planeManager, planeAlignment))
                    {

                        PlaceObjectOnPlane(lookingAtPose, lookingAtPlane);

                    }

                }
                else if (ARFoundationExtensions.HasTouchedPlane(sessionOrigin, planeManager, planeAlignment, out Pose touchPose, out ARPlane touchPlane))
                {

                    PlaceObjectOnPlane(touchPose, touchPlane);

                }


                PlaneUpdated?.Invoke(planeVisible, lookingAtPose, lookingAtPlane);

            }

        }

        private void ShowObjectOnPlane(Pose lookingAtPose, ARPlane lookingAtPlane)
        {

            if (lookingAtPlane != null)
            {

                showBeforePlacingGameObject.SetActive(true);

                SetTransformWithPoseAndPlaneData(showBeforePlacingGameObject.transform, childCamera, lookingAtPose, lookingAtPlane);

            }
            else
            {

                showBeforePlacingGameObject.SetActive(false);

            }

        }

        private void PlaceObjectOnPlane(Pose touchPose, ARPlane touchPlane)
        {

            GameObject objectOnPlane = null;

            if (objectToPlace.scene.IsValid() && spawnMultiple == false)
            {

                objectOnPlane = objectToPlace;

            }
            else
            {

                objectOnPlane = Instantiate(objectToPlace);

            }

            objectOnPlane.SetActive(true);

            SetTransformWithPoseAndPlaneData(objectOnPlane.transform, childCamera, touchPose, touchPlane);

            ObjectPlaced?.Invoke(objectOnPlane);

            if (spawnMultiple == false)
            {

                planeManager.enabled = false;

                Destroy(showBeforePlacingGameObject);

                ARFoundationExtensions.RemoveAllSpawnedPlanesFromScene();

            }

        }

        private static void SetTransformWithPoseAndPlaneData(Transform transform, Camera camera, Pose pose, ARPlane plane)
        {

            transform.position = pose.position;
            transform.rotation = pose.rotation;

            if (plane.boundedPlane.Alignment == PlaneAlignment.Horizontal)
            {

                RotateObjectTowardsCamera(transform, camera);

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

        private void OnEnable()
        {

            if (objectToPlace.scene.IsValid())
            {

                objectToPlace.SetActive(false);

            }

            showBeforePlacingGameObject = Instantiate(objectToPlace);
            showBeforePlacingGameObject.SetActive(false);

        }

        private void OnDisable()
        {

            Destroy(showBeforePlacingGameObject);

        }

    }

}

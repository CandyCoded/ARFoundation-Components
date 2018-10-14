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
        private GameObject gameObjectToPlace;

        public delegate void PlaneEvent(bool planeVisible, Pose pose);
        public event PlaneEvent PlaneUpdate;

        [HideInInspector]
        public ARSessionOrigin sessionOrigin;

        [HideInInspector]
        public ARPlaneManager planeManager;

        private Camera childCamera;

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

                if (gameObjectToPlace)
                {

                    PlaceObject(planeVisible, pose);

                }

                if (PlaneUpdate != null)
                {

                    PlaneUpdate(planeVisible, pose);

                }

            }

        }

        private void PlaceObject(bool planeVisible, Pose pose)
        {

            if (planeVisible)
            {

                gameObjectToPlace.SetActive(true);

                sessionOrigin.MakeContentAppearAt(gameObjectToPlace.transform, pose.position + new Vector3(0, 0.1f, 0));

                Vector3 lookAtPosition = gameObjectToPlace.transform.position + childCamera.transform.rotation * Vector3.forward;
                lookAtPosition.y = 0;

                gameObjectToPlace.transform.LookAt(lookAtPosition);

                if (gameObjectToPlace.GetInputDown(childCamera))
                {

                    sessionOrigin.MakeContentAppearAt(gameObjectToPlace.transform, pose.position);

                    planeManager.enabled = false;

                    ARFoundationExtensions.RemoveAllSpawnedPlanesFromScene();

                }

            }
            else
            {

                gameObjectToPlace.SetActive(false);

                gameObjectToPlace.transform.position = Vector3.zero;
                gameObjectToPlace.transform.rotation = Quaternion.identity;

            }

        }

    }

}

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARDistanceFromPlane : MonoBehaviour
    {

        [SerializeField]
        [EnumMask]
        private PlaneAlignment planeAlignment = PlaneAlignment.Horizontal;

        public delegate void DistanceEvent(bool planeVisible, float distance);
        public event DistanceEvent DistanceUpdate;

        [HideInInspector]
        public ARSessionOrigin sessionOrigin;

        [HideInInspector]
        public ARPlaneManager planeManager;

        private void Awake()
        {

            sessionOrigin = gameObject.GetComponent<ARSessionOrigin>();
            planeManager = gameObject.GetComponent<ARPlaneManager>();

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

            if (planeManager.enabled && DistanceUpdate != null)
            {

                bool planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out Pose pose);

                Vector3 distanceFromPlane = sessionOrigin.camera.transform.position - pose.position;

                DistanceUpdate(planeVisible, Mathf.Abs(distanceFromPlane.magnitude));

            }

        }

    }

}

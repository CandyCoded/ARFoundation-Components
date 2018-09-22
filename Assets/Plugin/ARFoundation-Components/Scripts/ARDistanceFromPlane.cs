using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

namespace CandyCoded
{

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARDistanceFromPlane : MonoBehaviour
    {

        [SerializeField]
        [EnumMask]
        private PlaneAlignment planeAlignment = PlaneAlignment.Horizontal;

        public delegate void DistanceEvent(float distance);
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

        private void Update()
        {

            if (planeManager.enabled && DistanceUpdate != null)
            {

                Pose pose;

                ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out pose);

                Vector3 distanceFromPlane = sessionOrigin.camera.transform.position - pose.position;

                DistanceUpdate(Mathf.Abs(distanceFromPlane.magnitude));

            }

        }

    }

}

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Events;

namespace CandyCoded.ARFoundationComponents
{

    [System.Serializable]
    public class DistanceUpdateEvent : UnityEvent<bool, float>
    {

    }

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARDistanceFromPlane : MonoBehaviour
    {

        public DistanceUpdateEvent DistanceUpdate;

        public ARSessionOrigin sessionOrigin { get; private set; }

        public ARRaycastManager raycastManager { get; private set; }

        public ARPlaneManager planeManager { get; private set; }

        private void Awake()
        {

            sessionOrigin = gameObject.GetComponent<ARSessionOrigin>();
            raycastManager = gameObject.GetComponent<ARRaycastManager>();
            planeManager = gameObject.GetComponent<ARPlaneManager>();

        }

        private void Start()
        {

            if (ARSession.state == ARSessionState.None ||
                ARSession.state == ARSessionState.Unsupported)
            {

                enabled = false;

            }

        }

        private void Update()
        {

            if (!planeManager.enabled || DistanceUpdate == null)
            {
                return;
            }

            var planeVisible = ARFoundationExtensions.IsLookingAtPlane(raycastManager, planeManager, out var pose);

            var distanceFromPlane = sessionOrigin.camera.transform.position - pose.position;

            DistanceUpdate?.Invoke(planeVisible, Mathf.Abs(distanceFromPlane.magnitude));

        }

    }

}

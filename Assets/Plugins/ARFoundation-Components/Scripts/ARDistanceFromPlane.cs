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
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARDistanceFromPlane : MonoBehaviour
    {

        public DistanceUpdateEvent DistanceUpdate;

        public ARSessionOrigin sessionOrigin { get; private set; }
        public ARPlaneManager planeManager { get; private set; }

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

                var planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, out var pose);

                var distanceFromPlane = sessionOrigin.camera.transform.position - pose.position;

                DistanceUpdate?.Invoke(planeVisible, Mathf.Abs(distanceFromPlane.magnitude));

            }

        }

    }

}

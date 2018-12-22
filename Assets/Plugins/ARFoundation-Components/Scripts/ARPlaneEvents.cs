using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [System.Serializable]
    public class PlaneUpdatedEvent : UnityEvent<bool, Pose, ARPlane>
    {
    }

    [System.Serializable]
    public class PlaneTouchedWithTouchPositionEvent : UnityEvent<Pose, ARPlane>
    {
    }

    [System.Serializable]
    public class PlaneTouchedWithLookingAtPositionEvent : UnityEvent<Pose, ARPlane>
    {
    }

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlaneEvents : MonoBehaviour
    {

        public PlaneUpdatedEvent PlaneUpdated;
        public PlaneTouchedWithTouchPositionEvent PlaneTouchedWithTouchPosition;
        public PlaneTouchedWithLookingAtPositionEvent PlaneTouchedWithLookingAtPosition;

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

            if (planeManager.enabled)
            {

                bool planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, out Pose lookingAtPose, out ARPlane lookingAtPlane);

                PlaneUpdated?.Invoke(planeVisible, lookingAtPose, lookingAtPlane);

                if (ARFoundationExtensions.HasTouchedPlane(sessionOrigin, planeManager, out Pose touchPose, out ARPlane touchPlane))
                {

                    PlaneTouchedWithTouchPosition?.Invoke(touchPose, touchPlane);
                    PlaneTouchedWithLookingAtPosition?.Invoke(lookingAtPose, lookingAtPlane);

                }

            }

        }

    }

}

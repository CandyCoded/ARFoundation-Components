using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [System.Serializable]
    public class PlaneUpdatedEvent : UnityEvent<bool, Pose, ARPlane>
    {
    }

    [System.Serializable]
    public class PlaneTouchedEvent : UnityEvent<Pose, ARPlane>
    {
    }

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlaneEvents : MonoBehaviour
    {

        public PlaneUpdatedEvent PlaneUpdated;
        public PlaneTouchedEvent PlaneTouchedWithTouchPosition;
        public PlaneTouchedEvent PlaneTouchedWithLookingAtPosition;

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

                var planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, out var lookingAtPose, out var lookingAtPlane);

                PlaneUpdated?.Invoke(planeVisible, lookingAtPose, lookingAtPlane);

                if (InputManager.GetInputDown(out var currentFingerId) && EventSystem.current?.IsPointerOverGameObject(currentFingerId) != true)
                {

                    if (ARFoundationExtensions.HasTouchedPlane(sessionOrigin, planeManager, out var touchPose, out var touchPlane))
                    {

                        PlaneTouchedWithTouchPosition?.Invoke(touchPose, touchPlane);

                    }

                    PlaneTouchedWithLookingAtPosition?.Invoke(lookingAtPose, lookingAtPlane);

                }

            }

        }

    }

}

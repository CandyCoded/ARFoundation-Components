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

                bool planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, out Pose lookingAtPose, out ARPlane lookingAtPlane);

                PlaneUpdated?.Invoke(planeVisible, lookingAtPose, lookingAtPlane);

                if (InputManager.GetInputDown(out int currentFingerId) && !EventSystem.current.IsPointerOverGameObject(currentFingerId))
                {

                    if (ARFoundationExtensions.HasTouchedPlane(sessionOrigin, planeManager, out Pose touchPose, out ARPlane touchPlane))
                    {

                        PlaneTouchedWithTouchPosition?.Invoke(touchPose, touchPlane);

                    }

                    PlaneTouchedWithLookingAtPosition?.Invoke(lookingAtPose, lookingAtPlane);

                }

            }

        }

    }

}

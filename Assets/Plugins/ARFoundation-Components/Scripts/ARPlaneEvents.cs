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

    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlaneEvents : MonoBehaviour
    {

        public PlaneUpdatedEvent PlaneUpdated;

        public PlaneTouchedEvent PlaneTouchedWithTouchPosition;

        public PlaneTouchedEvent PlaneTouchedWithLookingAtPosition;


        public ARRaycastManager raycastManager { get; private set; }

        public ARPlaneManager planeManager { get; private set; }

        private void Awake()
        {

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

            if (!planeManager.enabled)
            {
                return;
            }

            var planeVisible = ARFoundationExtensions.IsLookingAtPlane(raycastManager, planeManager, out var lookingAtPose, out var lookingAtPlane);

            PlaneUpdated?.Invoke(planeVisible, lookingAtPose, lookingAtPlane);

            if (!InputManager.GetInputDown(out var currentFingerId) || EventSystem.current && EventSystem.current.IsPointerOverGameObject(currentFingerId))
            {
                return;
            }

            if (ARFoundationExtensions.HasTouchedPlane(raycastManager, planeManager, out var touchPose, out var touchPlane))
            {

                PlaneTouchedWithTouchPosition?.Invoke(touchPose, touchPlane);

            }

            PlaneTouchedWithLookingAtPosition?.Invoke(lookingAtPose, lookingAtPlane);

        }

    }

}

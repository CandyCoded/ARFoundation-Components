// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [Serializable]
    public class PlaneEvent : UnityEvent<Pose, ARPlane>
    {

    }

    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(ARPlaneManager))]
    [HelpURL("https://github.com/CandyCoded/ARFoundation-Components/blob/master/Documentation/ARPlaneEvents.md")]
    public class ARPlaneEvents : MonoBehaviour
    {

        public PlaneEvent PlaneUpdated;

        public PlaneEvent PlaneTouchedWithTouchPosition;

        public PlaneEvent PlaneTouchedWithLookingAtPosition;

        private ARRaycastManager _raycastManager;

        private ARPlaneManager _planeManager;

        private void Awake()
        {

            _raycastManager = gameObject.GetComponent<ARRaycastManager>();
            _planeManager = gameObject.GetComponent<ARPlaneManager>();

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

            if (!_planeManager.enabled)
            {
                return;
            }

            var planeVisible = ARFoundationExtensions.IsLookingAtPlane(_raycastManager, _planeManager,
                out var lookingAtPose, out var lookingAtPlane);

            if (!planeVisible)
            {
                return;
            }

            PlaneUpdated?.Invoke(lookingAtPose, lookingAtPlane);

            if (!InputManager.GetInputDown(out var currentFingerId) || EventSystem.current &&
                EventSystem.current.IsPointerOverGameObject(currentFingerId))
            {
                return;
            }

            if (ARFoundationExtensions.HasTouchedPlane(_raycastManager, _planeManager, out var touchPose,
                out var touchPlane))
            {
                PlaneTouchedWithTouchPosition?.Invoke(touchPose, touchPlane);
            }

            PlaneTouchedWithLookingAtPosition?.Invoke(lookingAtPose, lookingAtPlane);

        }

    }

}

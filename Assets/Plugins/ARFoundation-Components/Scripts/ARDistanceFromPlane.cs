// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

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

        private ARSessionOrigin sessionOrigin;

        private ARRaycastManager raycastManager;

        private ARPlaneManager planeManager;

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

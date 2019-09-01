// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARTrackedObjectManager))]
    [HelpURL(
        "https://github.com/CandyCoded/ARFoundation-Components/blob/master/Documentation/ARTrackedObjectEvents.md")]
    public class ARTrackedObjectEvents : MonoBehaviour
    {

        [Serializable]
        public class TrackedObjectEvent : UnityEvent<ARTrackedObject>
        {

        }

        public TrackedObjectEvent TrackedObjectAdded;

        public TrackedObjectEvent TrackedObjectUpdated;

        public TrackedObjectEvent TrackedObjectRemoved;

        private ARTrackedObjectManager trackedObjectManager;

        private void Awake()
        {

            trackedObjectManager = gameObject.GetComponent<ARTrackedObjectManager>();

        }

        private void OnTrackedObjectChanged(ARTrackedObjectsChangedEventArgs eventArgs)
        {

            foreach (var trackedObject in eventArgs.added)
            {

                TrackedObjectAdded?.Invoke(trackedObject);

            }

            foreach (var trackedObject in eventArgs.updated)
            {

                TrackedObjectUpdated?.Invoke(trackedObject);

            }

            foreach (var trackedObject in eventArgs.removed)
            {

                TrackedObjectRemoved?.Invoke(trackedObject);

            }

        }

        private void OnEnable()
        {

            trackedObjectManager.trackedObjectsChanged += OnTrackedObjectChanged;

        }

        private void OnDisable()
        {

            trackedObjectManager.trackedObjectsChanged -= OnTrackedObjectChanged;

        }

    }

}

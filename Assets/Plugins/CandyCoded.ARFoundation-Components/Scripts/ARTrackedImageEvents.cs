// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARTrackedImageManager))]
    [HelpURL("https://github.com/CandyCoded/ARFoundation-Components/blob/master/Documentation/ARTrackedImageEvents.md")]
    public class ARTrackedImageEvents : MonoBehaviour
    {

        [Serializable]
        public class TrackedImageEvent : UnityEvent<ARTrackedImage>
        {

        }

        public TrackedImageEvent TrackedImageAdded;

        public TrackedImageEvent TrackedImageUpdated;

        public TrackedImageEvent TrackedImageRemoved;

        private ARTrackedImageManager _trackedImageManager;

        private void Awake()
        {

            _trackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();

        }

        private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
        {

            foreach (var trackedImage in eventArgs.added)
            {

                TrackedImageAdded?.Invoke(trackedImage);

            }

            foreach (var trackedImage in eventArgs.updated)
            {

                TrackedImageUpdated?.Invoke(trackedImage);

            }

            foreach (var trackedImage in eventArgs.removed)
            {

                TrackedImageRemoved?.Invoke(trackedImage);

            }

        }

        private void OnEnable()
        {

            _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;

        }

        private void OnDisable()
        {

            _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;

        }

    }

}

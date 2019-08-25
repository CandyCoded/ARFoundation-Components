// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARSessionOrigin))]
    public class ARShowPlacementMarkerOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private GameObject _placementMarker;

        public GameObject placementMarker
        {
            get => _placementMarker;
            set
            {

                CleanupPlacementMarker();

                _placementMarker = value;

                SetupPlacementMarker();

            }
        }

        public float verticalOffset = 0.01f;
#pragma warning restore CS0649

        private bool placementMarkerActiveState;

        private GameObject placementMarkerGameObject;

        public Camera mainCamera { get; private set; }

        private void Awake()
        {

            mainCamera = gameObject.GetComponent<ARSessionOrigin>().camera;

        }

        public void ShowPlacementMarkerOnPlane(bool planeVisible, Pose pose, ARPlane plane)
        {

            if (planeVisible && placementMarkerGameObject)
            {

                placementMarkerGameObject.SetActive(true);

                placementMarkerGameObject.transform.position = pose.position + new Vector3(0, verticalOffset, 0);
                placementMarkerGameObject.transform.rotation = pose.rotation;

                if (plane.alignment.Equals(PlaneAlignment.None) || plane.alignment.Equals(PlaneAlignment.NotAxisAligned))
                {
                    return;
                }

                var cameraPosition = mainCamera.transform.position;

                placementMarkerGameObject.transform.LookAt(new Vector3(
                    cameraPosition.x,
                    placementMarkerGameObject.transform.position.y,
                    cameraPosition.z
                ));

            }
            else
            {

                placementMarkerGameObject.SetActive(false);

            }

        }

        private void SetupPlacementMarker()
        {

            if (!_placementMarker)
            {
                return;
            }

            if (_placementMarker.scene.IsValid())
            {

                placementMarkerActiveState = _placementMarker.activeSelf;

                _placementMarker.SetActive(false);

            }

            placementMarkerGameObject = Instantiate(_placementMarker);
            placementMarkerGameObject.SetActive(false);

        }

        private void CleanupPlacementMarker()
        {

            if (_placementMarker && _placementMarker.scene.IsValid())
            {

                _placementMarker.SetActive(placementMarkerActiveState);

            }

            if (placementMarkerGameObject)
            {

                Destroy(placementMarkerGameObject);

            }

        }

        private void OnEnable()
        {

            SetupPlacementMarker();

        }

        private void OnDisable()
        {

            CleanupPlacementMarker();

        }

    }

}

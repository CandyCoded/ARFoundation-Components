// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARSessionOrigin))]
    [HelpURL(
        "https://github.com/CandyCoded/ARFoundation-Components/blob/master/Documentation/ARShowPlacementMarkerOnPlane.md")]
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

        private bool _placementMarkerActiveState;

        private GameObject _placementMarkerGameObject;

        public Camera mainCamera { get; private set; }

        private void Awake()
        {

            mainCamera = gameObject.GetComponent<ARSessionOrigin>().camera;

        }

        public void ShowPlacementMarkerOnPlane(bool planeVisible, Pose pose, ARPlane plane)
        {

            if (planeVisible && _placementMarkerGameObject)
            {

                _placementMarkerGameObject.SetActive(true);

                _placementMarkerGameObject.transform.position = pose.position + new Vector3(0, verticalOffset, 0);
                _placementMarkerGameObject.transform.rotation = pose.rotation;

                if (plane.alignment.Equals(PlaneAlignment.None) ||
                    plane.alignment.Equals(PlaneAlignment.NotAxisAligned))
                {
                    return;
                }

                var cameraPosition = mainCamera.transform.position;

                _placementMarkerGameObject.transform.LookAt(new Vector3(
                    cameraPosition.x,
                    _placementMarkerGameObject.transform.position.y,
                    cameraPosition.z
                ));

            }
            else
            {

                _placementMarkerGameObject.SetActive(false);

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

                _placementMarkerActiveState = _placementMarker.activeSelf;

                _placementMarker.SetActive(false);

            }

            _placementMarkerGameObject = Instantiate(_placementMarker);
            _placementMarkerGameObject.SetActive(false);

        }

        private void CleanupPlacementMarker()
        {

            if (_placementMarker && _placementMarker.scene.IsValid())
            {

                _placementMarker.SetActive(_placementMarkerActiveState);

            }

            if (_placementMarkerGameObject)
            {

                Destroy(_placementMarkerGameObject);

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

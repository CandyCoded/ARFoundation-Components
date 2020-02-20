// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARRaycastManager))]
    [RequireComponent(typeof(ARPlaneManager))]
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

        private ARRaycastManager _raycastManager;

        private ARPlaneManager _planeManager;

        private Camera _mainCamera;

        private void Awake()
        {

            _raycastManager = gameObject.GetComponent<ARRaycastManager>();
            _planeManager = gameObject.GetComponent<ARPlaneManager>();

            _mainCamera = gameObject.GetComponent<ARSessionOrigin>().camera;

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

            ShowPlacementMarkerOnPlane(planeVisible, lookingAtPose, lookingAtPlane);

        }

        public void ShowPlacementMarkerOnPlane(bool planeVisible, Pose pose, ARPlane plane)
        {

            if (planeVisible && _placementMarkerGameObject)
            {

                _placementMarkerGameObject.SetActive(true);

                _placementMarkerGameObject.transform.position =
                    pose.position + pose.up.normalized * verticalOffset;

                _placementMarkerGameObject.transform.rotation = pose.rotation;

                if (plane.alignment.Equals(PlaneAlignment.None) ||
                    plane.alignment.Equals(PlaneAlignment.NotAxisAligned))
                {
                    return;
                }

                var cameraPosition = _mainCamera.transform.position;

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

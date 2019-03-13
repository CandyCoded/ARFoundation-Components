using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

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
            get { return _placementMarker; }
            set
            {

                _placementMarker = value;

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

                if (plane.boundedPlane.Alignment == PlaneAlignment.Horizontal)
                {

                    placementMarkerGameObject.transform.LookAt(new Vector3(
                        mainCamera.transform.position.x,
                        placementMarkerGameObject.transform.position.y,
                        mainCamera.transform.position.z
                    ));

                }

            }
            else
            {

                placementMarkerGameObject.SetActive(false);

            }

        }

        private void SetupPlacementMarker()
        {

            if (_placementMarker)
            {

                if (_placementMarker.scene.IsValid())
                {

                    placementMarkerActiveState = _placementMarker.activeSelf;

                    _placementMarker.SetActive(false);

                }

                placementMarkerGameObject = Instantiate(_placementMarker);
                placementMarkerGameObject.SetActive(false);

            }

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

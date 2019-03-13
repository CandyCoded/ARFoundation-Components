using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(ARSessionOrigin))]
    public class ARShowPlacementMarkerOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        public GameObject placementMarker;
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

            if (planeVisible)
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

        private void OnEnable()
        {

            if (placementMarker.scene.IsValid())
            {

                placementMarkerActiveState = placementMarker.activeSelf;

                placementMarker.SetActive(false);

            }

            placementMarkerGameObject = Instantiate(placementMarker);
            placementMarkerGameObject.SetActive(false);

        }

        private void OnDisable()
        {

            if (placementMarker.scene.IsValid())
            {

                placementMarker.SetActive(placementMarkerActiveState);

            }

            Destroy(placementMarkerGameObject);

        }

    }

}

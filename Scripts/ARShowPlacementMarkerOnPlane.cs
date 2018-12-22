using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    public class ARShowPlacementMarkerOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private GameObject placementMarker;
#pragma warning restore CS0649

        private bool placementMarkerActiveState;

        private GameObject placementMarkerGameObject;

        public void ShowPlacementMarkerOnPlane(bool planeVisible, Pose pose, ARPlane plane)
        {

            if (planeVisible)
            {

                placementMarkerGameObject.SetActive(true);

                placementMarkerGameObject.transform.position = pose.position;
                placementMarkerGameObject.transform.rotation = pose.rotation;

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

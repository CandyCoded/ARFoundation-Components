// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    [System.Serializable]
    public class GameObjectPlacedEvent : UnityEvent<GameObject>
    {

    }

    [RequireComponent(typeof(ARSessionOrigin))]
    public class ARPlaceObjectOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        public GameObject objectToPlace;

        public bool placeMultiple;

        public float verticalOffset = 0.01f;
#pragma warning restore CS0649

        public GameObjectPlacedEvent GameObjectPlaced;

        private bool objectToPlaceActiveState;

        public Camera mainCamera { get; private set; }

        private void Awake()
        {

            mainCamera = gameObject.GetComponent<ARSessionOrigin>().camera;

        }

        public void PlaceObjectOnPlane(Pose pose, ARPlane plane)
        {

            var objectToPlaceGameObject = objectToPlace;

            if (placeMultiple)
            {

                objectToPlaceGameObject = Instantiate(objectToPlace);

            }

            objectToPlaceGameObject.SetActive(true);

            objectToPlaceGameObject.transform.position = pose.position + new Vector3(0, verticalOffset, 0);
            objectToPlaceGameObject.transform.rotation = pose.rotation;

            if (plane.alignment.Equals(PlaneAlignment.None) || plane.alignment.Equals(PlaneAlignment.NotAxisAligned))
            {
                return;
            }

            var cameraPosition = mainCamera.transform.position;

            objectToPlaceGameObject.transform.LookAt(new Vector3(
                cameraPosition.x,
                objectToPlaceGameObject.transform.position.y,
                cameraPosition.z
            ));

            GameObjectPlaced?.Invoke(objectToPlaceGameObject);

        }

        private void OnEnable()
        {

            if (!objectToPlace || !objectToPlace.scene.IsValid())
            {
                return;
            }

            objectToPlaceActiveState = objectToPlace.activeSelf;

            objectToPlace.SetActive(false);

        }

        private void OnDisable()
        {

            if (objectToPlace && objectToPlace.scene.IsValid())
            {

                objectToPlace.SetActive(objectToPlaceActiveState);

            }

        }

    }

}

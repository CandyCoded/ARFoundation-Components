// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    [Serializable]
    public class GameObjectPlacedEvent : UnityEvent<GameObject>
    {

    }

    [RequireComponent(typeof(XROrigin))]
    [HelpURL("https://github.com/CandyCoded/ARFoundation-Components/blob/master/Documentation/ARPlaceObjectOnPlane.md")]
    public class ARPlaceObjectOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        public GameObject objectToPlace;

        public bool placeMultiple;

        public float verticalOffset = 0.01f;
#pragma warning restore CS0649

        public GameObjectPlacedEvent GameObjectPlaced;

        public List<GameObject> objectsPlaced = new List<GameObject>();

        public Camera mainCamera { get; private set; }

        private bool _objectToPlaceActiveState;

        private void Awake()
        {

            mainCamera = gameObject.GetComponent<XROrigin>().GetComponentInChildren<Camera>();

        }

        public void PlaceObjectOnPlane(Pose pose, ARPlane plane)
        {

            var objectToPlaceGameObject = objectToPlace;

            if (!placeMultiple && objectsPlaced.Count != 0)
            {
                return;
            }

            if (!objectToPlaceGameObject.scene.IsValid())
            {

                objectToPlaceGameObject = Instantiate(objectToPlace);

            }

            objectToPlaceGameObject.SetActive(true);

            objectToPlaceGameObject.transform.position =
                pose.position + pose.up.normalized * verticalOffset;

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

            objectsPlaced.Add(objectToPlaceGameObject);

            GameObjectPlaced?.Invoke(objectToPlaceGameObject);

        }

        private void OnEnable()
        {

            if (!objectToPlace || !objectToPlace.scene.IsValid())
            {
                return;
            }

            _objectToPlaceActiveState = objectToPlace.activeSelf;

            objectToPlace.SetActive(false);

        }

        private void OnDisable()
        {

            if (objectToPlace && objectToPlace.scene.IsValid())
            {

                objectToPlace.SetActive(_objectToPlaceActiveState);

            }

        }

    }

}

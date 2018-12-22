using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [System.Serializable]
    public class GameObjectPlacedEvent : UnityEvent<GameObject>
    {
    }

    public class ARPlaceObjectOnPlane : MonoBehaviour
    {

#pragma warning disable CS0649
        [SerializeField]
        private GameObject objectToPlace;

        [SerializeField]
        private bool placeMultiple;
#pragma warning restore CS0649

        public GameObjectPlacedEvent GameObjectPlaced;

        private bool objectToPlaceActiveState;

        public void PlaceObjectOnPlane(Pose pose, ARPlane plane)
        {

            GameObject objectToPlaceGameObject = objectToPlace;

            if (placeMultiple)
            {

                objectToPlaceGameObject = Instantiate(objectToPlace);

            }

            objectToPlaceGameObject.SetActive(true);

            objectToPlaceGameObject.transform.position = pose.position;
            objectToPlaceGameObject.transform.rotation = pose.rotation;

            GameObjectPlaced?.Invoke(objectToPlaceGameObject);

        }

        private void OnEnable()
        {

            if (objectToPlace.scene.IsValid())
            {

                objectToPlaceActiveState = objectToPlace.activeSelf;

                objectToPlace.SetActive(false);

            }

        }

        private void OnDisable()
        {

            if (objectToPlace.scene.IsValid())
            {

                objectToPlace.SetActive(objectToPlaceActiveState);

            }

        }

    }

}

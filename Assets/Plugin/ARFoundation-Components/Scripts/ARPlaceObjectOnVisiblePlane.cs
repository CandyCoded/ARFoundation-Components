using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

namespace CandyCoded
{

    [RequireComponent(typeof(ARSessionOrigin))]
    [RequireComponent(typeof(ARPlaneManager))]
    public class ARPlaceObjectOnVisiblePlane : MonoBehaviour
    {

        [SerializeField]
        private PlaneAlignment planeAlignment = PlaneAlignment.Horizontal;

        public delegate void PlaneEvent(bool planeVisible, Pose pose);
        public event PlaneEvent PlaneUpdate;

        [HideInInspector]
        public ARSessionOrigin sessionOrigin;

        [HideInInspector]
        public ARPlaneManager planeManager;

        private void Awake()
        {

            sessionOrigin = gameObject.GetComponent<ARSessionOrigin>();
            planeManager = gameObject.GetComponent<ARPlaneManager>();

        }

        private void Update()
        {

            if (planeManager.enabled)
            {

                Pose pose;

                bool planeVisible = ARFoundationExtensions.IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out pose);

                if (PlaneUpdate != null)
                {

                    PlaneUpdate(planeVisible, pose);

                }

            }

        }

    }

}

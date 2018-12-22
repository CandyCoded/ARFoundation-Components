using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.XR;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    public static class ARFoundationExtensions
    {

        public static Vector2 CenterOfScreen
        {
            get
            {
                return new Vector2(Screen.width, Screen.height) / 2;
            }
        }

        public static bool RaycastToPlane(Vector3 position, ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
        {

            var hits = new List<ARRaycastHit>();

            pose = Pose.identity;

            plane = null;

            if (sessionOrigin.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
            {

                var hit = hits[0];

                pose = hit.pose;

                plane = planeManager.TryGetPlane(hit.trackableId);

                return true;

            }

            return false;

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
        {

            return RaycastToPlane(CenterOfScreen, sessionOrigin, planeManager, out pose, out plane);

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, out Pose pose)
        {

            return RaycastToPlane(CenterOfScreen, sessionOrigin, planeManager, out pose, out ARPlane plane);

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager)
        {

            return RaycastToPlane(CenterOfScreen, sessionOrigin, planeManager, out Pose pose, out ARPlane plane);

        }

        public static bool HasTouchedPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
        {

            pose = Pose.identity;

            plane = null;

            if (Input.touchSupported && Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {

                    return RaycastToPlane(touch.position, sessionOrigin, planeManager, out pose, out plane);

                }

            }

            return false;

        }

        public static bool HasTouchedPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, out Pose pose)
        {

            return HasTouchedPlane(sessionOrigin, planeManager, out pose, out ARPlane plane);

        }

        public static bool HasTouchedPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager)
        {

            return HasTouchedPlane(sessionOrigin, planeManager, out Pose pose, out ARPlane plane);

        }

        public static void SetActiveStateOfPlaneVisuals(bool activeState)
        {

            var planes = Object.FindObjectsOfType<ARPlane>();

            foreach (ARPlane plane in planes)
            {

                plane.gameObject.SetActive(activeState);

            }

        }

    }

}

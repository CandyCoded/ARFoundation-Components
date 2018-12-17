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

        public static bool RaycastToPlane(Vector3 position, ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose, out ARPlane plane)
        {

            var hits = new List<ARRaycastHit>();

            pose = Pose.identity;

            plane = null;

            if (sessionOrigin.Raycast(position, hits, TrackableType.PlaneWithinPolygon))
            {

                var hit = hits[0];

                pose = hit.pose;

                var trackableId = hit.trackableId;

                plane = planeManager.TryGetPlane(trackableId);

                return plane.boundedPlane.Alignment == planeAlignment;

            }

            return false;

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose, out ARPlane plane)
        {

            return RaycastToPlane(CenterOfScreen, sessionOrigin, planeManager, planeAlignment, out pose, out plane);

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose)
        {

            ARPlane plane;

            return RaycastToPlane(CenterOfScreen, sessionOrigin, planeManager, planeAlignment, out pose, out plane);

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment)
        {

            Pose pose;
            ARPlane plane;

            return RaycastToPlane(CenterOfScreen, sessionOrigin, planeManager, planeAlignment, out pose, out plane);

        }

        public static bool HasTouchedPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose, out ARPlane plane)
        {

            pose = Pose.identity;

            plane = null;

            if (Input.touchSupported && Input.touchCount > 0)
            {

                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {

                    return RaycastToPlane(touch.position, sessionOrigin, planeManager, planeAlignment, out pose, out plane);

                }

            }

            return false;

        }

        public static bool HasTouchedPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose)
        {

            ARPlane plane;

            return HasTouchedPlane(sessionOrigin, planeManager, planeAlignment, out pose, out plane);

        }

        public static bool HasTouchedPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment)
        {

            ARPlane plane;

            Pose pose;

            return HasTouchedPlane(sessionOrigin, planeManager, planeAlignment, out pose, out plane);

        }

        public static void RemoveAllSpawnedPlanesFromScene()
        {

            var planes = Object.FindObjectsOfType<ARPlane>();

            foreach (ARPlane plane in planes)
            {

                Object.Destroy(plane.gameObject);

            }

        }

    }

}

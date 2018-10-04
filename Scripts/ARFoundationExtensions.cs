using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

namespace CandyCoded
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

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose, out ARPlane plane)
        {

            var hits = new List<ARRaycastHit>();

            pose = Pose.identity;

            plane = null;

            if (sessionOrigin.Raycast(CenterOfScreen, hits, TrackableType.PlaneWithinPolygon))
            {

                var hit = hits[0];

                pose = hit.pose;

                var trackableId = hit.trackableId;

                plane = planeManager.TryGetPlane(trackableId);

                return plane.boundedPlane.Alignment == planeAlignment;

            }

            return false;

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment, out Pose pose)
        {

            ARPlane plane;

            return IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out pose, out plane);

        }

        public static bool IsLookingAtPlane(ARSessionOrigin sessionOrigin, ARPlaneManager planeManager, PlaneAlignment planeAlignment)
        {

            Pose pose;
            ARPlane plane;

            return IsLookingAtPlane(sessionOrigin, planeManager, planeAlignment, out pose, out plane);

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

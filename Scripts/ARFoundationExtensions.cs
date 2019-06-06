using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    public static class ARFoundationExtensions
    {

        public static Vector2 CenterOfScreen => new Vector2(Screen.width, Screen.height) / 2;

        public static bool RaycastToPlane(Vector2 position, ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
        {

            var hits = new List<ARRaycastHit>();

            pose = Pose.identity;

            plane = null;

            if (!raycastManager.Raycast(position, hits, TrackableType.Planes))
            {
                return false;
            }

            var hit = hits[0];

            pose = hit.pose;

            foreach (var trackable in planeManager.trackables)
            {

                if (trackable.trackableId.Equals(hit.trackableId))
                {

                    plane = trackable;

                }

            }

            return true;

        }

        public static bool IsLookingAtPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
        {

            return RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out pose, out plane);

        }

        public static bool IsLookingAtPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose)
        {

            return RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out pose, out _);

        }

        public static bool IsLookingAtPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager)
        {

            return RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out _, out _);

        }

        public static bool HasTouchedPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
        {

            pose = Pose.identity;

            plane = null;

            if (!Input.touchSupported || Input.touchCount <= 0)
            {
                return false;
            }

            var touch = Input.GetTouch(0);

            return touch.phase == TouchPhase.Began && RaycastToPlane(touch.position, raycastManager, planeManager, out pose, out plane);

        }

        public static bool HasTouchedPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose)
        {

            return HasTouchedPlane(raycastManager, planeManager, out pose, out _);

        }

        public static bool HasTouchedPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager)
        {

            return HasTouchedPlane(raycastManager, planeManager, out _, out _);

        }

        public static void SetActiveStateOfPlaneVisuals(bool activeState)
        {

            var planes = Object.FindObjectsOfType<ARPlane>();

            foreach (var plane in planes)
            {

                plane.gameObject.SetActive(activeState);

            }

        }

    }

}

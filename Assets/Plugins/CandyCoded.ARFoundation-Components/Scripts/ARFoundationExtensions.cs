// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    public static class ARFoundationExtensions
    {

        public static Vector2 CenterOfScreen => new Vector2(Screen.width, Screen.height) / 2;

        /// <summary>
        /// Raycast from `Vector2` screen point. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="position">Position on screen to fire raycast from.</param>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        /// <param name="pose">Unity pose object.</param>
        /// <param name="plane">AR Foundation plane object.</param>
        /// <returns>void</returns>
        public static bool RaycastToPlane(Vector2 position, ARRaycastManager raycastManager,
            ARPlaneManager planeManager, out Pose pose, out ARPlane plane)
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

        /// <summary>
        /// Raycast from center of screen. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        /// <param name="pose">Unity pose object.</param>
        /// <param name="plane">AR Foundation plane object.</param>
        public static bool IsLookingAtPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose,
            out ARPlane plane)
        {

            return RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out pose, out plane);

        }

        /// <summary>
        /// Raycast from center of screen. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        /// <param name="pose">Unity pose object.</param>
        public static bool IsLookingAtPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose)
        {

            return RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out pose, out var _);

        }

        /// <summary>
        /// Raycast from center of screen. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        public static bool IsLookingAtPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager)
        {

            return RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out var _, out var _);

        }

        /// <summary>
        /// Raycast from input position of screen. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        /// <param name="pose">Unity pose object.</param>
        /// <param name="plane">AR Foundation plane object.</param>
        public static bool HasTouchedPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose,
            out ARPlane plane)
        {

            pose = Pose.identity;

            plane = null;

            if (!Input.touchSupported || Input.touchCount <= 0)
            {
                return false;
            }

            var touch = Input.GetTouch(0);

            return touch.phase == TouchPhase.Began &&
                   RaycastToPlane(touch.position, raycastManager, planeManager, out pose, out plane);

        }

        /// <summary>
        /// Raycast from input position of screen. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        /// <param name="pose">Unity pose object.</param>
        public static bool HasTouchedPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager, out Pose pose)
        {

            return HasTouchedPlane(raycastManager, planeManager, out pose, out var _);

        }

        /// <summary>
        /// Raycast from input position of screen. Returns true if raycast collides with a plane.
        /// </summary>
        /// <param name="raycastManager">AR Foundation raycast manager component.</param>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        public static bool HasTouchedPlane(ARRaycastManager raycastManager, ARPlaneManager planeManager)
        {

            return HasTouchedPlane(raycastManager, planeManager, out var _, out var _);

        }

        /// <summary>
        /// Disables/enables all `ARPlane` gameObjects.
        /// </summary>
        /// <param name="planeManager">AR Foundation plane manager component.</param>
        /// <param name="activeState">False deactivates any tracked planes. True reactivates any deactivated planes.</param>
        public static void SetActiveStateOfPlaneVisuals(this ARPlaneManager planeManager, bool activeState)
        {

            foreach (var trackable in planeManager.trackables)
            {

                trackable.gameObject.SetActive(activeState);

            }

        }

    }

}

// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class QuickSetupPlaneDetection
    {

        private const string defaultPlaneName = "AR Default Plane";

        private const string defaultPlaneMenuPath = "GameObject/XR/AR Default Plane";

        private const string sessionOriginName = "AR Session Origin";

        private const string defaultPlanePrefabPath = "Assets/AR Default Plane.prefab";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Plane Detection")]
        private static void Setup()
        {

            EditorApplication.isPlaying = false;

            AutoPopulateScene.SetupARFoundation();
            SetupARFoundationDemoPlane();

            AutoPopulateScene.SetupARFoundationComponents();
            AutoPopulateScene.SetupARFoundationComponentsDemoCube();

            SetupARFoundationComponentsPlaneDetection();

        }

        private static void SetupARFoundationDemoPlane()
        {

            var defaultPlanePrefab = CustomEditorExtensions.FindOrCreatePrefabFromAssetMenu(defaultPlaneName,
                defaultPlaneMenuPath, defaultPlanePrefabPath);

            var sessionOrigin = GameObject.Find(sessionOriginName);

            var planeManager = sessionOrigin.AddOrGetComponent<ARPlaneManager>();

            planeManager.planePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(defaultPlanePrefabPath);

        }

        private static void SetupARFoundationComponentsPlaneDetection()
        {

            var sessionOrigin = GameObject.Find(sessionOriginName);

            sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            Selection.activeGameObject = sessionOrigin;

            placeObjectOnPlane.objectToPlace =
                AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);

            placeObjectOnPlane.placeMultiple = true;

            EditorApplication.update += AddPlaneEvent;

            var showPlacementMarkerOnPlane = sessionOrigin.AddComponent<ARShowPlacementMarkerOnPlane>();

            showPlacementMarkerOnPlane.placementMarker =
                AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);

        }

        private static void AddPlaneEvent()
        {

            var sessionOrigin = GameObject.Find(sessionOriginName);
            var planeEvents = sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectsOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            if (planeEvents.PlaneTouchedWithLookingAtPosition == null)
            {
                return;
            }

            UnityEventTools.AddPersistentListener(planeEvents.PlaneTouchedWithLookingAtPosition,
                placeObjectsOnPlane.PlaceObjectOnPlane);

            EditorApplication.update -= AddPlaneEvent;

        }

    }

}
#endif

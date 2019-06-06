#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class QuickSetupPlaneDetection
    {

        private const string defaultPlanePrefabPath = "Assets/AR Default Plane.prefab";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Plane Detection")]
        private static void Setup()
        {

            EditorApplication.isPlaying = false;

            AutoPopulateScene.SetupARFoundation();
            AutoPopulateScene.SetupARFoundationComponents();
            AutoPopulateScene.SetupARFoundationComponentsDemoCube();

            SetupARFoundationComponentsDemoPlane();
            SetupARFoundationComponentsPlaneDetection();

        }

        private static void SetupARFoundationComponentsDemoPlane()
        {

            var defaultPlanePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(defaultPlanePrefabPath);

            if (defaultPlanePrefab)
            {
                return;
            }

            EditorApplication.ExecuteMenuItem("GameObject/XR/AR Default Plane");

            PrefabUtility.SaveAsPrefabAsset(GameObject.Find("AR Default Plane"), defaultPlanePrefabPath);

            Object.DestroyImmediate(GameObject.Find("AR Default Plane"));

        }


        private static void SetupARFoundationComponentsPlaneDetection()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");

            var planeManager = sessionOrigin.AddOrGetComponent<ARPlaneManager>();

            planeManager.planePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(defaultPlanePrefabPath);

            sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            Selection.activeGameObject = sessionOrigin;

            placeObjectOnPlane.objectToPlace = AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);
            placeObjectOnPlane.placeMultiple = true;

            EditorApplication.update += AddPlaneEvent;

        }

        private static void AddPlaneEvent()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");
            var planeEvents = sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectsOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            if (planeEvents.PlaneTouchedWithTouchPosition == null)
            {
                return;
            }

            UnityEventTools.AddPersistentListener(planeEvents.PlaneTouchedWithTouchPosition, placeObjectsOnPlane.PlaceObjectOnPlane);

            EditorApplication.update -= AddPlaneEvent;

        }

    }

}
#endif

#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    public static class AutoPopulateScene
    {

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup")]
        private static void PopulateSceneWithARGameObjects()
        {

            EditorApplication.isPlaying = false;

            SetupARFoundationGameObjects();
            SetupARFoundationComponentsGameObjects();

        }

        private static void SetupARFoundationGameObjects()
        {

            EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session Origin");

            Object.DestroyImmediate(GameObject.Find("Main Camera"));

            EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session");

            EditorApplication.ExecuteMenuItem("GameObject/XR/AR Default Plane");

            var defaultPlanePrefab = PrefabUtility.SaveAsPrefabAsset(GameObject.Find("AR Default Plane"), "Assets/AR Default Plane.prefab");

            Object.DestroyImmediate(GameObject.Find("AR Default Plane"));

            var sessionOrigin = GameObject.Find("AR Session Origin");

            var planeManager = sessionOrigin.AddComponent<ARPlaneManager>();

            planeManager.planePrefab = defaultPlanePrefab;

        }

        private static void SetupARFoundationComponentsGameObjects()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");

            var planeEvents = sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectsOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            //UnityEventTools.AddPersistentListener(planeEvents.PlaneTouchedWithTouchPosition, placeObjectsOnPlane.PlaceObjectOnPlane);

            var cubePrefab = CreateDemoCubePrefab();

            placeObjectsOnPlane.objectToPlace = cubePrefab;
            placeObjectsOnPlane.placeMultiple = true;

        }

        private static GameObject CreateDemoCubePrefab()
        {

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one * 0.1f;

            var cubePrefabs = PrefabUtility.SaveAsPrefabAsset(cube, "Assets/Cube.prefab");

            Object.DestroyImmediate(cube);

            return cubePrefabs;

        }

    }

}

#endif

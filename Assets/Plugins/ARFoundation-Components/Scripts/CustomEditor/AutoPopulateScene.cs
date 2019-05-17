#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;
using UnityEditor.Events;

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

            if (!GameObject.Find("AR Session Origin"))
            {

                EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session Origin");

            }

            var mainCamera = GameObject.Find("Main Camera");

            if (mainCamera)
            {

                Object.DestroyImmediate(mainCamera);

            }

            if (!GameObject.Find("AR Session"))
            {

                EditorApplication.ExecuteMenuItem("GameObject/XR/AR Session");

            }

            const string defaultPlanePrefabPath = "Assets/AR Default Plane.prefab";

            var defaultPlanePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(defaultPlanePrefabPath);

            if (!defaultPlanePrefab)
            {

                EditorApplication.ExecuteMenuItem("GameObject/XR/AR Default Plane");

                defaultPlanePrefab = PrefabUtility.SaveAsPrefabAsset(GameObject.Find("AR Default Plane"), defaultPlanePrefabPath);

                Object.DestroyImmediate(GameObject.Find("AR Default Plane"));

            }

            var sessionOrigin = GameObject.Find("AR Session Origin");

            var planeManager = sessionOrigin.AddOrGetComponent<ARPlaneManager>();

            planeManager.planePrefab = defaultPlanePrefab;

        }

        private static void SetupARFoundationComponentsGameObjects()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");

            var planeEvents = sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectsOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            Selection.activeGameObject = sessionOrigin;

            UnityEventTools.AddPersistentListener(planeEvents.PlaneTouchedWithTouchPosition, placeObjectsOnPlane.PlaceObjectOnPlane);

            const string cubePrefabPath = "Assets/Cube.prefab";

            var cubePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cubePrefabPath);

            if (!cubePrefab)
            {

                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.localScale = Vector3.one * 0.1f;

                cubePrefab = PrefabUtility.SaveAsPrefabAsset(cube, "Assets/Cube.prefab");

                Object.DestroyImmediate(cube);

            }

            placeObjectsOnPlane.objectToPlace = cubePrefab;
            placeObjectsOnPlane.placeMultiple = true;

        }

    }

}

#endif

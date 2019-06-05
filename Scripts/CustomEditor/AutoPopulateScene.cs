#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;
using UnityEditor.Events;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents
{

    public static class AutoPopulateScene
    {

        private const string defaultPlanePrefabPath = "Assets/AR Default Plane.prefab";

        private const string cubePrefabPath = "Assets/Cube.prefab";

        private const string imageReferenceLibraryPath = "Assets/ReferenceImageLibrary.asset";

        private const string objectReferenceLibraryPath = "Assets/ReferenceObjectLibrary.asset";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Plane Detection")]
        private static void QuickSetupPlaneDetection()
        {

            EditorApplication.isPlaying = false;

            SetupARFoundation();
            SetupARFoundationComponents();
            SetupARFoundationComponentsDemoPlane();
            SetupARFoundationComponentsDemoCube();
            SetupARFoundationComponentsPlaneDetection();

        }

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Image Detection")]
        private static void QuickSetupImageDetection()
        {

            EditorApplication.isPlaying = false;

            SetupARFoundation();
            SetupARFoundationComponents();
            SetupARFoundationComponentsDemoCube();
            SetupARFoundationComponentsImageDetection();

        }

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Object Detection")]
        private static void QuickSetupObjectDetection()
        {

            EditorApplication.isPlaying = false;

            SetupARFoundation();
            SetupARFoundationComponents();
            SetupARFoundationComponentsDemoCube();
            SetupARFoundationComponentsObjectDetection();

        }

        private static void SetupARFoundation()
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

            var sessionOrigin = GameObject.Find("AR Session Origin");

            sessionOrigin.AddOrGetComponent<ARRaycastManager>();

        }

        private static void SetupARFoundationComponents()
        {

            var camera = GameObject.Find("AR Camera");
            var cameraManager = camera.AddOrGetComponent<ARCameraManager>();

            var light = GameObject.Find("Directional Light");
            var lightEstimation = light.AddOrGetComponent<ARLightEstimation>();
            lightEstimation.cameraManager = cameraManager;

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

        private static void SetupARFoundationComponentsDemoCube()
        {

            var cubePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cubePrefabPath);

            if (cubePrefab)
            {
                return;
            }

            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.localScale = Vector3.one * 0.1f;

            PrefabUtility.SaveAsPrefabAsset(cube, cubePrefabPath);

            Object.DestroyImmediate(cube);

        }

        private static void SetupARFoundationComponentsPlaneDetection()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");

            var planeManager = sessionOrigin.AddOrGetComponent<ARPlaneManager>();

            planeManager.planePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(defaultPlanePrefabPath);

            var planeEvents = sessionOrigin.AddOrGetComponent<ARPlaneEvents>();
            var placeObjectsOnPlane = sessionOrigin.AddOrGetComponent<ARPlaceObjectOnPlane>();

            Selection.activeGameObject = sessionOrigin;

            UnityEventTools.AddPersistentListener(planeEvents.PlaneTouchedWithTouchPosition, placeObjectsOnPlane.PlaceObjectOnPlane);

            placeObjectsOnPlane.objectToPlace = AssetDatabase.LoadAssetAtPath<GameObject>(cubePrefabPath);
            placeObjectsOnPlane.placeMultiple = true;

        }

        private static void SetupARFoundationComponentsImageDetection()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");
            var trackedImageManager = sessionOrigin.AddOrGetComponent<ARTrackedImageManager>();

            trackedImageManager.trackedImagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cubePrefabPath);

            var referenceLibrary = AssetDatabase.LoadAssetAtPath<XRReferenceImageLibrary>(imageReferenceLibraryPath);

            if (!referenceLibrary)
            {

                referenceLibrary = ScriptableObject.CreateInstance<XRReferenceImageLibrary>();

                AssetDatabase.CreateAsset(referenceLibrary, imageReferenceLibraryPath);

            }

            trackedImageManager.referenceLibrary = AssetDatabase.LoadAssetAtPath<XRReferenceImageLibrary>(imageReferenceLibraryPath);

        }

        private static void SetupARFoundationComponentsObjectDetection()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");
            var trackedObjectManager = sessionOrigin.AddOrGetComponent<ARTrackedObjectManager>();

            trackedObjectManager.trackedObjectPrefab = AssetDatabase.LoadAssetAtPath<GameObject>(cubePrefabPath);

            var referenceLibrary = AssetDatabase.LoadAssetAtPath<XRReferenceObjectLibrary>(objectReferenceLibraryPath);

            if (!referenceLibrary)
            {

                referenceLibrary = ScriptableObject.CreateInstance<XRReferenceObjectLibrary>();

                AssetDatabase.CreateAsset(referenceLibrary, objectReferenceLibraryPath);

            }

            trackedObjectManager.referenceLibrary = referenceLibrary;

        }

    }

}

#endif

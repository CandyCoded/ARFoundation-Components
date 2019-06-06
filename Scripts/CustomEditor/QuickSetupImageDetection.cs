#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class QuickSetupImageDetection
    {

        private const string imageReferenceLibraryPath = "Assets/ReferenceImageLibrary.asset";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Image Detection")]
        private static void Setup()
        {

            EditorApplication.isPlaying = false;

            AutoPopulateScene.SetupARFoundation();
            AutoPopulateScene.SetupARFoundationComponents();
            AutoPopulateScene.SetupARFoundationComponentsDemoCube();

            SetupARFoundationComponentsImageDetection();

        }

        private static void SetupARFoundationComponentsImageDetection()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");
            var trackedImageManager = sessionOrigin.AddOrGetComponent<ARTrackedImageManager>();

            trackedImageManager.trackedImagePrefab = AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);

            var referenceLibrary = AssetDatabase.LoadAssetAtPath<XRReferenceImageLibrary>(imageReferenceLibraryPath);

            if (!referenceLibrary)
            {

                referenceLibrary = ScriptableObject.CreateInstance<XRReferenceImageLibrary>();

                AssetDatabase.CreateAsset(referenceLibrary, imageReferenceLibraryPath);

            }

            trackedImageManager.referenceLibrary = referenceLibrary;

        }

    }

}
#endif

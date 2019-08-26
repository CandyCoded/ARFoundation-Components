// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class QuickSetupObjectDetection
    {

        private const string objectReferenceLibraryPath = "Assets/ReferenceObjectLibrary.asset";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Object Detection")]
        private static void Setup()
        {

            EditorApplication.isPlaying = false;

            AutoPopulateScene.SetupARFoundation();
            AutoPopulateScene.SetupARFoundationComponents();
            AutoPopulateScene.SetupARFoundationComponentsDemoCube();

            SetupARFoundationComponentsObjectDetection();

        }

        private static void SetupARFoundationComponentsObjectDetection()
        {

            var sessionOrigin = GameObject.Find("AR Session Origin");
            var trackedObjectManager = sessionOrigin.AddOrGetComponent<ARTrackedObjectManager>();

            trackedObjectManager.trackedObjectPrefab =
                AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);

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

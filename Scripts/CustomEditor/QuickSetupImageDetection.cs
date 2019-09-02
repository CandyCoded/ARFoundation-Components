// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class QuickSetupImageDetection
    {

        private const string sessionOriginName = "AR Session Origin";

        private const string sessionOriginMenuPath = "GameObject/XR/AR Session Origin";

        private const string imageReferenceLibraryAssetPath = "Assets/ReferenceImageLibrary.asset";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Image Detection")]
        private static void Setup()
        {

            EditorApplication.isPlaying = false;

            AutoPopulateScene.SetupARFoundation();
            AutoPopulateScene.SetupARFoundationComponents();
            AutoPopulateScene.SetupARFoundationComponentsDemoCube();

            SetupARFoundationImageDetection();
            SetupARFoundationComponentsImageDetection();

        }

        private static void SetupARFoundationImageDetection()
        {

            var sessionOrigin =
                CustomEditorExtensions.FindOrCreateGameObjectFromAssetMenu(sessionOriginName, sessionOriginMenuPath);

            var trackedImageManager = sessionOrigin.AddOrGetComponent<ARTrackedImageManager>();

            trackedImageManager.trackedImagePrefab =
                AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);

            var referenceLibrary =
                CustomEditorExtensions.FindOrCreateScriptableObjectAtPath<XRReferenceImageLibrary>(
                    imageReferenceLibraryAssetPath);

            try
            {

                trackedImageManager.referenceLibrary = referenceLibrary;

            }
            catch (Exception err)
            {

                Debug.LogWarning(err);

            }

        }

        private static void SetupARFoundationComponentsImageDetection()
        {

            var sessionOrigin = GameObject.Find(sessionOriginName);

            sessionOrigin.AddOrGetComponent<ARTrackedImageEvents>();

        }

    }

}
#endif

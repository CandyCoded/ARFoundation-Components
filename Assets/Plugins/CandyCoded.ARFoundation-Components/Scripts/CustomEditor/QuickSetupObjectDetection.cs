// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

#if UNITY_EDITOR
using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class QuickSetupObjectDetection
    {

        private const string sessionOriginName = "AR Session Origin";

        private const string sessionOriginMenuPath = "GameObject/XR/AR Session Origin";

        private const string objectReferenceLibraryAssetPath = "Assets/ReferenceObjectLibrary.asset";

        [MenuItem("Window/CandyCoded/ARFoundation Components/Quick Setup/Object Detection")]
        private static void Setup()
        {

            EditorApplication.isPlaying = false;

            AutoPopulateScene.SetupARFoundation();
            AutoPopulateScene.SetupARFoundationComponents();
            AutoPopulateScene.SetupARFoundationComponentsDemoCube();

            SetupARFoundationObjectDetection();
            SetupARFoundationComponentsObjectDetection();

        }

        private static void SetupARFoundationObjectDetection()
        {

            var sessionOrigin =
                CustomEditorExtensions.FindOrCreateGameObjectFromAssetMenu(sessionOriginName, sessionOriginMenuPath);

            var trackedObjectManager = sessionOrigin.AddOrGetComponent<ARTrackedObjectManager>();

            trackedObjectManager.trackedObjectPrefab =
                AssetDatabase.LoadAssetAtPath<GameObject>(AutoPopulateScene.cubePrefabPath);

            var referenceLibrary =
                CustomEditorExtensions.FindOrCreateScriptableObjectAtPath<XRReferenceObjectLibrary>(
                    objectReferenceLibraryAssetPath);

            try
            {

                trackedObjectManager.referenceLibrary = referenceLibrary;

            }
            catch (Exception err)
            {

                Debug.LogWarning(err);

            }

        }

        private static void SetupARFoundationComponentsObjectDetection()
        {

            var sessionOrigin = GameObject.Find(sessionOriginName);

            sessionOrigin.AddOrGetComponent<ARTrackedObjectEvents>();

        }

    }

}
#endif

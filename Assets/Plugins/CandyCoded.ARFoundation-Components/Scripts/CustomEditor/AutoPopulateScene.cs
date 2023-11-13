// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

namespace CandyCoded.ARFoundationComponents.Editor
{

    public static class AutoPopulateScene
    {

        private const string sessionOriginName = "XR Origin";

        private const string sessionOriginMenuPath = "GameObject/XR/XR Origin (Mobile AR)";

        private const string sessionName = "AR Session";

        private const string sessionMenuPath = "GameObject/XR/AR Session";

        private const string directionalLightName = "Directional Light";

        private const string directionalLightMenuPath = "GameObject/Light/Directional Light";

        internal const string cubePrefabPath = "Assets/Cube.prefab";

        internal static void SetupARFoundation()
        {

            foreach (var camera in (Camera[])Resources.FindObjectsOfTypeAll(typeof(Camera)))
            {
                if (camera.transform.parent == null && camera.gameObject.CompareTag("MainCamera"))
                {
                    Object.DestroyImmediate(camera.gameObject);
                }
            }

            var sessionOrigin =
                CustomEditorExtensions.FindOrCreateGameObjectFromAssetMenu(sessionOriginName, sessionOriginMenuPath);

            sessionOrigin.AddOrGetComponent<ARRaycastManager>();

            var session = CustomEditorExtensions.FindOrCreateGameObjectFromAssetMenu(sessionName, sessionMenuPath);

        }

        internal static void SetupARFoundationComponents()
        {

            var camera = GameObject.Find("Main Camera");
            camera.tag = "MainCamera";

            var cameraManager = camera.AddOrGetComponent<ARCameraManager>();
            cameraManager.requestedLightEstimation = LightEstimation.AmbientIntensity;

            var light = CustomEditorExtensions.FindOrCreateGameObjectFromAssetMenu(directionalLightName,
                directionalLightMenuPath);

            var lightComponent = light.GetComponent<Light>();

            lightComponent.color = new Color(1, 0.957f, 0.839f, 1);
            lightComponent.shadows = LightShadows.Soft;

            var lightEstimation = light.AddOrGetComponent<ARLightEstimation>();
            lightEstimation.cameraManager = cameraManager;

        }

        internal static void SetupARFoundationComponentsDemoCube()
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

    }

}

#endif

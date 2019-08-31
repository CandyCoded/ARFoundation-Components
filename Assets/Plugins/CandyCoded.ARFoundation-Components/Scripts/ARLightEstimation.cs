// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(Light))]
    [HelpURL("https://github.com/CandyCoded/ARFoundation-Components/blob/master/Documentation/ARLightEstimation.md")]
    public class ARLightEstimation : MonoBehaviour
    {

        [SerializeField]
        private ARCameraManager _cameraManager;

#pragma warning disable CS0109
        public new Light light { get; private set; }

        public new ARCameraManager cameraManager
        {
            get => _cameraManager;
            set => _cameraManager = value;
        }
#pragma warning restore CS0109

        private void Awake()
        {

            light = gameObject.GetComponent<Light>();

        }

        private void Start()
        {

            if (ARSession.state == ARSessionState.None ||
                ARSession.state == ARSessionState.Unsupported)
            {

                enabled = false;

            }

        }

        private void FrameChanged(ARCameraFrameEventArgs args)
        {

            var averageBrightness = args.lightEstimation.averageBrightness;
            var averageColorTemperature = args.lightEstimation.averageColorTemperature;
            var colorCorrection = args.lightEstimation.colorCorrection;

            if (averageBrightness.HasValue)
            {

                light.intensity = averageBrightness.Value;

            }

            if (averageColorTemperature.HasValue)
            {

                light.colorTemperature = averageColorTemperature.Value;

            }

            if (colorCorrection.HasValue)
            {

                light.color = colorCorrection.Value;

            }

        }

        private void OnEnable()
        {

            if (cameraManager == null)
            {

                return;

            }

            cameraManager.frameReceived += FrameChanged;

        }

        private void OnDisable()
        {

            if (cameraManager == null)
            {

                return;

            }

            cameraManager.frameReceived -= FrameChanged;

        }

    }

}

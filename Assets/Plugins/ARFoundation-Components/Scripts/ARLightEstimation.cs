using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded.ARFoundationComponents
{

    [RequireComponent(typeof(Light))]
    public class ARLightEstimation : MonoBehaviour
    {

#pragma warning disable CS0109
        public new Light light { get; private set; }
#pragma warning restore CS0109

        private void Awake()
        {

            light = gameObject.GetComponent<Light>();

        }

        private void Start()
        {

            if (ARSubsystemManager.systemState == ARSystemState.None ||
                ARSubsystemManager.systemState == ARSystemState.Unsupported)
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

            ARSubsystemManager.cameraFrameReceived += FrameChanged;

        }

        private void OnDisable()
        {

            ARSubsystemManager.cameraFrameReceived -= FrameChanged;

        }

    }

}

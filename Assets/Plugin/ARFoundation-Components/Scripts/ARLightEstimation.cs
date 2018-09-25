using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace CandyCoded
{

    [RequireComponent(typeof(Light))]
    public class ARLightEstimation : MonoBehaviour
    {

        private new Light light;

        private void Awake()
        {

            light = gameObject.GetComponent<Light>();

            if (ARSubsystemManager.systemState == ARSystemState.Unsupported)
            {

                enabled = false;

            }

        }

        private void FrameChanged(ARCameraFrameEventArgs args)
        {

            float? averageBrightness = args.lightEstimation.averageBrightness;
            float? averageColorTemperature = args.lightEstimation.averageColorTemperature;
            Color? colorCorrection = args.lightEstimation.colorCorrection;

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

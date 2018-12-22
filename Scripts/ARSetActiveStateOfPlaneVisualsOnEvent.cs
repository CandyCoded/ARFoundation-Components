using UnityEngine;

namespace CandyCoded.ARFoundationComponents
{

    public class ARSetActiveStateOfPlaneVisualsOnEvent : MonoBehaviour
    {

        public void SetActiveStateOfPlaneVisuals(bool activeState)
        {

            ARFoundationExtensions.SetActiveStateOfPlaneVisuals(activeState);

        }

    }

}

using UnityEngine;

namespace CandyCoded.ARFoundationComponents
{

    public class ARSetEnabledStateOfComponentsOnEvent : MonoBehaviour
    {

        public MonoBehaviour[] components;

        public void SetEnabledStateOfComponents(bool enabledState)
        {

            foreach (var component in components)
            {

                component.enabled = enabledState;

            }

        }

    }

}

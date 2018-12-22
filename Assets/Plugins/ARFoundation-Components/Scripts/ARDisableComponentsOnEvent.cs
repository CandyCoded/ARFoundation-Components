using UnityEngine;

namespace CandyCoded.ARFoundationComponents
{

    public class ARDisableComponentsOnEvent : MonoBehaviour
    {

        public MonoBehaviour[] components;

        public void DisableComponents()
        {

            foreach (var component in components)
            {

                component.enabled = false;

            }

        }

    }

}

using UnityEngine;

namespace CandyCoded.ARFoundationComponents
{

    public class SetEnabledStateOfObjectsOnEvent : MonoBehaviour
    {

        public Object[] objects;

        public void SetEnabledStateOfObjects(bool enabledState)
        {

            foreach (var obj in objects)
            {

                if (obj is MonoBehaviour)
                {

                    ((MonoBehaviour)obj).enabled = enabledState;

                }
                else if (obj is GameObject)
                {

                    ((GameObject)obj).SetActive(enabledState);

                }

            }

        }

    }

}

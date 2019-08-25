// Copyright (c) Scott Doxey. All Rights Reserved. Licensed under the MIT License. See LICENSE in the project root for license information.

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

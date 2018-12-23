# ARFoundation Components

## Features

-   ARDistanceFromPlane
-   ARLightEstimation
-   ARPlaceObjectOnPlane
-   ARPlaneEvents
-   ARShowPlacementMarkerOnPlane
-   ARSetActiveStateOfPlaneVisualsOnEvent
-   SetEnabledStateOfObjectsOnEvent
-   ARFoundationExtensions
    -   CenterOfScreen
    -   RaycastToPlane
    -   IsLookingAtPlane
    -   HasTouchedPlane
    -   SetActiveStateOfPlaneVisuals

## Installation

### Unity Package Manager _(Unity 2018.3)_

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

```json
{
    "dependencies": {
        "com.candycoded.arfoundation-components": "https://github.com/CandyCoded/ARFoundation-Components.git#upm",
        "com.candycoded.candycoded": "https://github.com/CandyCoded/CandyCoded.git#v1.1.0"
    }
}
```

## Setup

### Basic ARKit XR Plugin and AR Foundation Steps

1. Create a new Unity project.
1. Install **ARKit XR Plugin** and **AR Foundation** from Package Manager.
1. Remove **Main Camera** gameObject from scene.
1. Create new **AR Session Origin** gameObject from the **Create Asset** context menu.
1. Create new **AR Session** gameObject from the **Create Asset** context menu.
1. Create new **AR Default Plane** gameObject from the **Create Asset** context menu.
1. Drag **AR Default Plane** gameObject to **Assets** panel to create a prefab.
1. Remove **AR Default Plane** gameObject from scene.
1. Attach the **AR Plane Manager** script to **AR Session Origin** and drag the just created prefab into the **Plane Prefab** property.
1. Change **Detection Flags** to **Horizontal** (or whatever you are targeting).

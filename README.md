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
1. Install the **ARKit XR Plugin** and **AR Foundation** packages from the Package Manager.
1. Remove the **Main Camera** gameObject from the scene.
1. Create a new **AR Session Origin** gameObject from the **Create Asset** context menu.
1. Create a new **AR Session** gameObject from the **Create Asset** context menu.
1. Create a new **AR Default Plane** gameObject from the **Create Asset** context menu.
1. Drag the **AR Default Plane** gameObject to **Assets** panel to create a prefab.
1. Remove the **AR Default Plane** prefab gameObject from the scene.
1. Attach the **AR Plane Manager** script to the **AR Session Origin** gameObject and drag the **AR Default Plane** prefab from the **Assets** panel into the **Plane Prefab** property.
1. Change the **Detection Flags** to **Horizontal** (or whatever plane type you will be targeting).

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

See <https://github.com/CandyCoded/ARFoundation-Components-Demo> for a working example of the following steps.

### Basic ARKit XR Plugin and AR Foundation Steps

1. Create a new Unity project.
1. Install the **ARKit XR Plugin** and **AR Foundation** packages from the Package Manager.
1. Remove the **Main Camera** gameObject from the scene.
1. Create a new **AR Session Origin** gameObject from the **Create Asset** context menu.
1. Create a new **AR Session** gameObject from the **Create Asset** context menu.
1. Create a new **AR Default Plane** gameObject from the **Create Asset** context menu.
1. Drag the **AR Default Plane** gameObject to **Assets** panel to create a prefab.
1. Remove the **AR Default Plane** prefab gameObject from the scene.
1. Attach the **AR Plane Manager** component to the **AR Session Origin** gameObject and drag the **AR Default Plane** prefab from the **Assets** panel into the **Plane Prefab** property.
1. Change the **Detection Flags** to **Horizontal** (or whatever plane type you will be targeting).

### ARFoundation Components Steps

1. Install both the **ARFoundation Components** and **CandyCoded** package (see above).
1. Attach the **AR Plane Events** component to the **AR Session Origin** gameObject.
1. Attach the **AR Place Object On Plane** component to the **AR Session Origin** gameObject.
1. Create a new **Cube** and set the scale to `Vector3(0.1f, 0.1f, 0.1f)`.
1. Drag the **Cube** into the **Assets** panel to create a prefab.
1. Remove the **Cube** prefab gameObject from the scene.
1. Drag the **Cube** into the **Object to Place** property of the **AR Place Object On Plane** component.
1. Check the **Place Multiple** property.
1. Create a new event in the **Plane Touched with Touch Position** event section of the **AR Plane Events** component.
1. Drag the **AR Session Origin** gameObject into the object field of the event.
1. Select the **ARPlaceObjectOnPlane** > **PlaceObjectOnPlane** dynamic method from the dropdown.

### iOS Build Steps

1. Check that the scene is in the **Build Settings** window.
1. Change the build platform in the **Build Settings** window to **iOS**.
1. Add test to the **Camera Usage Description** input in the **Player Settings** panel.
1. Change **Target minimum iOS Version** to `11`.
1. Change **Architecture** to `ARM64`.
1. When the **Unity.XR.ARKit will be stripped** dialog pops up, select **Yes, fix and build**.

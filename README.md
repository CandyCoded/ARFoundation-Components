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
        "com.candycoded.candycoded": "https://github.com/CandyCoded/CandyCoded.git#v1.1.6"
    }
}
```

## Setup

See <https://github.com/CandyCoded/ARFoundation-Components-Demo> for a working example of the following steps.

### Setup AR Foundation

1. Install the **ARKit XR Plugin** and **AR Foundation** packages from the Package Manager.
1. Create a new **AR Session Origin** gameObject from the **Create Asset** context menu.
1. Remove the **Main Camera** gameObject from the hierarchy (as the **AR Session Origin** contains its own camera).
1. Create a new **AR Session** gameObject from the **Create Asset** context menu.
1. Create a new **AR Default Plane** gameObject from the **Create Asset** context menu, drag it into the **Asset** panel creating a prefab and then removing it from the hierarchy.
1. Attach the **AR Plane Manager** component to the **AR Session Origin** gameObject and drag the **AR Default Plane** prefab from the **Assets** panel into the **Plane Prefab** property.
1. Change the **Detection Flags** to **Horizontal** (or whatever plane type you will target).

### Setup ARFoundation Components

1. Install both the **ARFoundation Components** and **CandyCoded** package (see above).
1. Attach the **AR Plane Events** and **AR Place Object On Plane** components to the **AR Session Origin** gameObject.
1. Create a new **Cube** and set the scale to `Vector3(0.1f, 0.1f, 0.1f)`, drag it into the **Asset** panel creating a prefab and then removing it from the hierarchy.
1. Drag the **Cube** into the **Object to Place** property of the **AR Place Object On Plane** component and enable the option **Place Multiple**.
1. Create a new event in the **Plane Touched with Touch Position** event section of the **AR Plane Events** component.
1. Drag the **AR Session Origin** gameObject into the object field of the event.
1. Select the **ARPlaceObjectOnPlane** > **PlaceObjectOnPlane** dynamic method from the dropdown.

### Setup iOS build

1. Check that the scene is in the **Build Settings** window.
1. Change the build platform in the **Build Settings** window to **iOS**.
1. In the **Player Settings** panel, make sure there is a string in the **Camera Usage Description** field, the **Target minimum iOS Version** is at least `11`, and **Architecture** is set to `ARM64`.
1. When the **Unity.XR.ARKit will be stripped** dialog pops up, select **Yes, fix and build**.

### Setup Android build

1. Check that the scene is in the **Build Settings** window.
1. Change the build platform in the **Build Settings** window to **Android**.
1. In the **Player Settings** panel, make sure that **Multithreaded Rendering** is unchecked, the **Bundle Identifier** is set, and **Minimum API level** is set to `Android 7.0 'Nugget' (API level 24)`.
1. If the **Android SDK is outdated** dialog appears, press **Use Highest Installed**.

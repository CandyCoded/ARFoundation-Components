# ARFoundation Components

> 📱 Generic components for use with Unity's AR Foundation package

[![NPM Version](http://img.shields.io/npm/v/xyz.candycoded.arfoundation-components.svg?style=flat)](https://www.npmjs.org/package/xyz.candycoded.arfoundation-components)

## Contents

- Components
  - Helper Components
    - [ARLightEstimation](Documentation/ARLightEstimation.md)
    - [ARPlaceObjectOnPlane](Documentation/ARPlaceObjectOnPlane.md)
    - [ARShowPlacementMarkerOnPlane](Documentation/ARShowPlacementMarkerOnPlane.md)
  - Event Components
    - [ARPlaneEvents](Documentation/ARPlaneEvents.md)
    - [ARTrackedImageEvents](Documentation/ARTrackedImageEvents.md)
    - [ARTrackedObjectEvents](Documentation/ARTrackedObjectEvents.md)
- Extensions
  - [ARFoundationExtensions](Documentation/ARFoundationExtensions.md)
    - [CenterOfScreen](Documentation/ARFoundationExtensions.md#centerofscreen)
    - [RaycastToPlane](Documentation/ARFoundationExtensions.md#raycasttoplane)
    - [IsLookingAtPlane](Documentation/ARFoundationExtensions.md#islookingatplane)
    - [HasTouchedPlane](Documentation/ARFoundationExtensions.md#hastouchedplane)
    - [SetActiveStateOfPlaneVisuals](Documentation/ARFoundationExtensions.md#setactivestateofplanevisuals)

## Installation

<https://docs.unity3d.com/Packages/com.unity.package-manager-ui@2.0/manual/index.html>

### Unity Package Manager

```json
{
  "dependencies": {
    "xyz.candycoded.arfoundation-components": "https://github.com/CandyCoded/ARFoundation-Components.git#v4.0.4",
    "xyz.candycoded.candycoded": "https://github.com/CandyCoded/CandyCoded.git#v4.4.1",
    ...
  }
}
```

#### Scoped UPM Registry

```json
{
  "dependencies": {
    "xyz.candycoded.arfoundation-components": "4.0.4",
    "xyz.candycoded.candycoded": "4.4.1",
    ...
  },
  "scopedRegistries": [
    {
      "name": "candycoded",
      "url": "https://registry.npmjs.com",
      "scopes": ["xyz.candycoded"]
    }
  ]
}
```

## Setup

See <https://github.com/CandyCoded/ARFoundation-Components-Demo> for a working example of the following steps.

### Install Dependencies

1. Install the latest **ARKit XR Plugin**, **ARCore XR Plugin** and **AR Foundation** (preview) packages from the Package Manager.
2. Install both the **ARFoundation Components** and **CandyCoded** package (see above).

### Setup AR Foundation

1. Create a new **AR Session Origin** gameObject from the **Create Asset / XR** context menu.
1. Select the **AR Session Origin** gameObject and attach the **AR Raycast Manager** component.
1. Remove the **Main Camera** gameObject from the hierarchy (as the **AR Session Origin** contains its own camera).
1. Create a new **AR Session** gameObject from the **Create Asset / XR** context menu.
1. Create a new **AR Default Plane** gameObject from the **Create Asset / XR** context menu, drag it into the **Asset** panel creating a prefab and then remove it from the hierarchy.
1. Attach the **AR Plane Manager** component to the **AR Session Origin** gameObject and drag the **AR Default Plane** prefab from the **Assets** panel into the **Plane Prefab** property.
1. Change the **Detection Flags** to **Horizontal** (or whatever plane type you will target).

### Setup ARFoundation Components

1. Select the **AR Camera** in the **AR Session Origin** gameObject and change the **Light Estimation Mode** on the **AR Camera Manager** component to **AmbientIntensity**.
1. Select the **Directional Light** gameObject and attach the **AR Light Estimation** component.
1. Drag the **AR Camera** component into the **Camera Manager** property of the **AR Light Estimation** component.
1. Attach the **AR Plane Events** and **AR Place Object On Plane** components to the **AR Session Origin** gameObject.
1. Create a new **Cube** and set the scale to `Vector3(0.1f, 0.1f, 0.1f)`, drag it into the **Asset** panel creating a prefab and then remove it from the hierarchy.
1. Drag the **Cube** into the **Object to Place** property of the **AR Place Object On Plane** component and enable the option **Place Multiple**.
1. Create a new event in the **Plane Touched with Touch Position** event section of the **AR Plane Events** component.
1. Drag the **AR Session Origin** gameObject into the object field of the event.
1. Select the **ARPlaceObjectOnPlane** > **PlaceObjectOnPlane** dynamic method from the dropdown.

### Setup iOS build

1. Check that the scene is in the **Build Settings** window.
1. Change the build platform in the **Build Settings** window to **iOS**.
1. In the **XR Plug-in Managment** panel, make sure to enable the ARKit plug-in providers.
1. In the **Player Settings** panel, make sure following settings are correct:

| Setting                        | Value                         | Description                                                                              |
| ------------------------------ | ----------------------------- | ---------------------------------------------------------------------------------------- |
| **Camera Usage Description**   | `AR BABY` or any other string | This value will display when the dialog asking for camera permission displays on device. |
| **Target minimum iOS Version** | `11` or higher                | iOS 11 was the first version ARKit was available.                                        |
| **Architecture**               | `ARM64`                       | The only iOS devices that support ARKit are built on `ARM64` architecture.               |

### Setup Android build

First follow: <https://developers.google.com/ar/develop/unity/android-11-build>

1. Check that the scene is in the **Build Settings** window.
1. Change the build platform in the **Build Settings** window to **Android**.
1. In the **XR Plug-in Managment** panel, make sure to enable the ARCore plug-in providers.
1. In the **Player Settings** panel, make sure following settings are correct:

| Setting               | Value                                           | Description                                                                                                                |
| --------------------- | ----------------------------------------------- | -------------------------------------------------------------------------------------------------------------------------- |
| **Graphic APIs**      | `OPENGLs3` and remove `Vulcan`                  | ARCore does not support Vulcan [arcore-android-sdk issue #258](https://github.com/google-ar/arcore-android-sdk/issues/258) |
| **Minimum API level** | `Android 7.0 'Nugget' (API level 24)` or higher | Android 7 was the first version ARCore was available.                                                                      |

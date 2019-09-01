### ARFoundationExtensions

#### CenterOfScreen

Returns a `Vector2` that is half the screen's current width and height.

```csharp
Debug.Log(CenterOfScreen);
```

#### RaycastToPlane

Raycast from `Vector2` screen point. Returns true if raycast collides with a plane. Also returns collision pose and `ARPlane`.

```csharp
if (RaycastToPlane(CenterOfScreen, raycastManager, planeManager, out var pose, out var plane)) {
    Debug.Log(pose.position);
    Debug.Log(plane.alignment);
}
```

#### IsLookingAtPlane

Raycast from center of screen. Returns true if raycast collides with a plane. Optionally returns collision pose and `ARPlane`.

```csharp
if (IsLookingAtPlane(raycastManager, planeManager, out var pose, out var plane)) {
    Debug.Log("Plane is visible (and in the center of the screen).");
}
```

```csharp
if (IsLookingAtPlane(raycastManager, planeManager, out var pose)) {
    Debug.Log("Plane is visible (and in the center of the screen).");
}
```

```csharp
if (IsLookingAtPlane(raycastManager, planeManager)) {
    Debug.Log("Plane is visible (and in the center of the screen).");
}
```

#### HasTouchedPlane

Raycast from input position of screen. Returns true if raycast collides with a plane. Optionally returns collision pose and `ARPlane`.

```csharp
if (HasTouchedPlane(raycastManager, planeManager, out var pose, out var plane)) {
    Debug.Log("Plane has been touched.");
}
```

```csharp
if (HasTouchedPlane(raycastManager, planeManager, out var pose)) {
    Debug.Log("Plane has been touched.");
}
```

```csharp
if (HasTouchedPlane(raycastManager, planeManager)) {
    Debug.Log("Plane has been touched.");
}
```

#### SetActiveStateOfPlaneVisuals

Disables/enables all `ARPlane` gameObjects.

```csharp
SetActiveStateOfPlaneVisuals(false); // disables all ARPlane gameObjects
```

```csharp
SetActiveStateOfPlaneVisuals(true); // enables all ARPlane gameObjects
```

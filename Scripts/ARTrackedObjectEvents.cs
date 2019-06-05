using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedObjectManager))]
public class ARTrackedObjectEvents : MonoBehaviour
{

    [System.Serializable]
    public class TrackedObjectEvent : UnityEvent<ARTrackedObject>
    {

    }

    public TrackedObjectEvent TrackedObjectAdded;

    public TrackedObjectEvent TrackedObjectUpdated;

    public TrackedObjectEvent TrackedObjectRemoved;

    private ARTrackedObjectManager trackedObjectManager;

    private void Awake()
    {

        trackedObjectManager = gameObject.GetComponent<ARTrackedObjectManager>();

    }

    private void OnTrackedObjectChanged(ARTrackedObjectsChangedEventArgs eventArgs)
    {

        foreach (var trackedObject in eventArgs.added)
        {

            TrackedObjectAdded?.Invoke(trackedObject);

        }

        foreach (var trackedObject in eventArgs.updated)
        {

            TrackedObjectUpdated?.Invoke(trackedObject);

        }

        foreach (var trackedObject in eventArgs.removed)
        {

            TrackedObjectRemoved?.Invoke(trackedObject);

        }

    }

    private void OnEnable()
    {

        trackedObjectManager.trackedObjectsChanged += OnTrackedObjectChanged;

    }

    private void OnDisable()
    {

        trackedObjectManager.trackedObjectsChanged -= OnTrackedObjectChanged;

    }

}

using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARTrackedImageManager))]
[RequireComponent(typeof(ObjectManager))]
public class MultiTracking : MonoBehaviour
{
    private ARTrackedImageManager arTrackedImageManager;
    private ObjectManager objectManager;

    void Awake()
    {
        arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        objectManager = GetComponent<ObjectManager>();
    }

    private void Start()
    {
        DebugText.instance.Log($"Started");
    }

    private void OnEnable()
    {
        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
    }
    private void OnDisable()
    {
        arTrackedImageManager.trackedImagesChanged -= OnImageChanged;
    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs _args)
    {
        foreach (var trackedImage in _args.updated)
        {
            if (trackedImage.trackingState == TrackingState.Tracking)
            {
                objectManager.Show(trackedImage.referenceImage.name, trackedImage.transform);
            }
            else
            {
                objectManager.Hide(trackedImage.referenceImage.name);
            }
        }
    }
}

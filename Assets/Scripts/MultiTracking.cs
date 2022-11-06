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
            //DebugText.instance.Log($"Tracking {trackedImage.referenceImage.name}");
            //DebugText.instance.Log($"State {trackedImage.transform.position}");
            //if (trackedImage.trackingState == TrackingState.Tracking)
            //{
            //    ActivateTrackedObject(trackedImage.referenceImage.name, trackedImage.transform);

            //    for (int i = 0; i < _args.updated.Count; i++)
            //    {
            //        if (_args.updated[i].referenceImage.name != trackedImage.referenceImage.name)
            //        {
            //            DeactivateTrackedObject(_args.updated[i].referenceImage.name);
            //        }
            //    }
            //    break;
            //}

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

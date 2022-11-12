using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectManager : MonoBehaviour
{
    public delegate void TrackInfoEventHandler(object source, TrackInfoEventArgs e);
    public delegate void TrackListInfoEventHandler(object source, TrackListInfoEventArgs e);

    public List<TrackedObject> trackedObject;
    private List<TrackedObjectInfo> instantiatedObjects = new();

    public static event TrackInfoEventHandler TrackingFirst;
    public static event TrackInfoEventHandler TrackingEnter;
    public static event TrackInfoEventHandler TrackingStay;
    public static event TrackInfoEventHandler TrackingExit;
    public static event TrackListInfoEventHandler TrackingListUpdated;

    public void Show(string imageName, Transform location)
    {
        var trackedObjectInfo = instantiatedObjects.FirstOrDefault(t => t.trackedObject.ImageName == imageName);
        if (trackedObjectInfo != null)
        {
            if (!trackedObjectInfo.isVisible)
            {
                trackedObjectInfo.isVisible = true;
                TrackingEnter(this, new TrackInfoEventArgs(trackedObjectInfo, location));
            }
            TrackingStay(this, new TrackInfoEventArgs(trackedObjectInfo, location));
        }
        else
        {
            //Generate the object
            var info = trackedObject.FirstOrDefault(f => f.ImageName == imageName);
            if (info != null)
            {
                var instantiatedObject = Instantiate(info.Prefab, location);
                var instantiatedInfo = instantiatedObject.AddComponent<TrackedObjectInfo>();
                instantiatedInfo.trackedObject = info;
                instantiatedInfo.isVisible = true;

                instantiatedObjects.Add(instantiatedInfo);
                TrackingListUpdated(this, new TrackListInfoEventArgs(instantiatedObjects));

                if (instantiatedObject.GetComponent<ARAnchor>() == null)
                {
                    instantiatedObject.AddComponent<ARAnchor>();
                }

                TrackingFirst(this, new TrackInfoEventArgs(instantiatedInfo, location));
                TrackingEnter(this, new TrackInfoEventArgs(instantiatedInfo, location));
            }
        }
    }

    public void Hide(string imageName)
    {
        var trackedObjectInfo = instantiatedObjects.FirstOrDefault(t => t.trackedObject.ImageName == imageName);
        if (trackedObjectInfo.isVisible)
        {
            trackedObjectInfo.isVisible = false;
            TrackingExit(this, new TrackInfoEventArgs(trackedObjectInfo));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class ObjectManager : MonoBehaviour
{
    public class TrackInfoEventArgs : EventArgs
    {
        public TrackedObjectInfo TrackedObjectInfo;
        public Transform Location;
        public TrackInfoEventArgs(TrackedObjectInfo trackedObjectInfo, Transform location = null)
        {
            TrackedObjectInfo = trackedObjectInfo;
            Location = location;
        }
        public string GetImageName()
        {
            return TrackedObjectInfo.trackedObject.ImageName;
        }
    }
    public delegate void TrackInfoEventHandler(object source, TrackInfoEventArgs e);

    public List<TrackedObject> trackedObject;
    private List<TrackedObjectInfo> instantiatedObjects = new();

    [Range(0.1f, 1.0f)] public float globalScale;

    public event TrackInfoEventHandler TrackingFirst;
    public event TrackInfoEventHandler TrackingEnter;
    public event TrackInfoEventHandler TrackingStay;
    public event TrackInfoEventHandler TrackingExit;

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

                if (instantiatedObject.GetComponent<ARAnchor>() == null)
                {
                    instantiatedObject.AddComponent<ARAnchor>();
                }

                TrackingFirst(this, new TrackInfoEventArgs(instantiatedInfo, location));
                TrackingEnter(this, new TrackInfoEventArgs(trackedObjectInfo, location));
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

    private void Awake()
    {
        TrackingExit += OnTrackingExit;
        TrackingFirst += OnTrackingFirst;
        TrackingStay += OnTrackingStay;
        TrackingEnter += OnTrackingEnter;
    }

    private void OnTrackingEnter(object source, TrackInfoEventArgs e)
    {
        DebugText.instance.Log($"EN");
    }

    private void OnTrackingStay(object source, TrackInfoEventArgs e)
    {
        //DebugText.instance.Log($"ST");
        //Freeze rotation on 2 axis on each frame
        Quaternion q = e.Location.transform.rotation;
        q.eulerAngles = new Vector3(0.0f, 180.0f + e.Location.transform.rotation.eulerAngles.y, 0.0f);
        e.TrackedObjectInfo.gameObject.transform.rotation = q;
        e.TrackedObjectInfo.gameObject.SetActive(true);
    }

    private void OnTrackingFirst(object source, TrackInfoEventArgs e)
    {
        DebugText.instance.Log($"FR");
        e.TrackedObjectInfo.transform.localScale = Vector3.one * globalScale;
    }

    private void OnTrackingExit(object source, TrackInfoEventArgs e)
    {
        DebugText.instance.Log($"EX");
        e.TrackedObjectInfo.gameObject.SetActive(false);
    }
}

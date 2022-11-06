using System;
using UnityEngine;

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
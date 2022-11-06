using System;
using System.Collections.Generic;

public class TrackListInfoEventArgs : EventArgs
{
    public List<TrackedObjectInfo> TrackedObjectInfoList;
    public TrackListInfoEventArgs(List<TrackedObjectInfo> trackedObjectInfoList)
    {
        TrackedObjectInfoList = trackedObjectInfoList;
    }
}

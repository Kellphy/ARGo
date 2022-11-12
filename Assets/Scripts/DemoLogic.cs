using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ObjectManager;

public class DemoLogic : MonoBehaviour
{
    private List<TrackedObjectInfo> trackedObjects;
    private void Awake()
    {
        TrackingExit += OnTrackingExit;
        TrackingFirst += OnTrackingFirst;
        TrackingStay += OnTrackingStay;
        TrackingEnter += OnTrackingEnter;
        TrackingListUpdated += OnTrackingListUpdated;
    }

    private void OnTrackingListUpdated(object source, TrackListInfoEventArgs e)
    {
        trackedObjects = e.TrackedObjectInfoList;
        DebugText.Instance.Log($"Object List Updated to: {trackedObjects.Count}");
    }

    private void OnTrackingEnter(object source, TrackInfoEventArgs e)
    {
        DebugText.Instance.Log($"Found: {e.TrackedObjectInfo.trackedObject.ImageName}");
        e.TrackedObjectInfo.gameObject.SetActive(true);

        e.TrackedObjectInfo.transform.localScale = Vector3.one * GlobalSettings.Instance.GetGlobalScale();

        if (!GlobalSettings.Instance.IsPermanentDetection())
        {
            var enabledTrackedObjects = trackedObjects.Where(t => t.gameObject.activeSelf && t != e.TrackedObjectInfo);
            foreach(var enabledTrackedObject in enabledTrackedObjects)
            {
                enabledTrackedObject.gameObject.SetActive(false);
            }
        }
    }

    private void OnTrackingStay(object source, TrackInfoEventArgs e)
    {
        //DebugText.instance.Log($"Detecting {e.TrackedObjectInfo.trackedObject.ImageName}");

        //Freeze Rotation on each frame example
        //e.TrackedObjectInfo.gameObject.transform.rotation = Quaternion.identity;

        if (GlobalSettings.Instance.IsTransformFrozen())
        {
            //Freeze rotation on 2 axis on each frame
            Quaternion q = e.Location.transform.rotation;
            q.eulerAngles = new Vector3(0.0f, 180.0f + e.Location.transform.rotation.eulerAngles.y, 0.0f);
            e.TrackedObjectInfo.gameObject.transform.rotation = q;
        }
    }

    private void OnTrackingFirst(object source, TrackInfoEventArgs e)
    {
        DebugText.Instance.Log($"First Time: {e.TrackedObjectInfo.trackedObject.ImageName}");
    }

    private void OnTrackingExit(object source, TrackInfoEventArgs e)
    {
        DebugText.Instance.Log($"Lost: {e.TrackedObjectInfo.trackedObject.ImageName}");
        if (GlobalSettings.Instance.IsPermanentDetection())
        {
            e.TrackedObjectInfo.gameObject.SetActive(false);
        }
    }
}

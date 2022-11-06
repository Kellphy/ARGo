using System;
using System.Collections.Generic;
using UnityEngine;
using static ObjectManager;

public class DemoLogic : MonoBehaviour
{
    [Range(0.1f, 10.0f)] public float globalScale;
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
        DebugText.instance.Log($"LIST: {trackedObjects.Count}");
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

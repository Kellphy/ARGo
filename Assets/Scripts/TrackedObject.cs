using UnityEngine;

[CreateAssetMenu(fileName = "NewTrackedObject", menuName = "Scriptable Objects/Tracked Object", order = 1)]
public class TrackedObject : ScriptableObject
{
    public string ImageName;
    public GameObject Prefab;
}

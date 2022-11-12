using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GlobalSettings : MonoBehaviour
{
    [SerializeField] private Toggle toggleDebug;
    [SerializeField] private Toggle toggleTransformFreeze;
    [SerializeField] private Toggle toggleDetection;
    [SerializeField] private Slider sliderGlobalScale;
    public static GlobalSettings Instance;

    void Awake()
    {
        Instance = this;

        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        sliderGlobalScale.value = 1f;
        toggleDebug.isOn = false;
        toggleTransformFreeze.isOn = true;
        toggleDetection.isOn = true;
    }

    void Start()
    {
        ToggleDebug(toggleDebug.isOn);
        toggleDebug.onValueChanged.AddListener(delegate
        {
            ToggleDebug(toggleDebug.isOn);
        });

        ToggleTransformFreeze(toggleTransformFreeze.isOn);
        toggleTransformFreeze.onValueChanged.AddListener(delegate
        {
            ToggleTransformFreeze(toggleTransformFreeze.isOn);
        });

        ToggleDetection(toggleDetection.isOn);
        toggleDetection.onValueChanged.AddListener(delegate
        {
            ToggleDetection(toggleDetection.isOn);
        });
    }

    string EnabledOrDisabled(bool isOn)
    {
        return isOn ? "Enabled" : "Disabled";
    }

    void ToggleDebug(bool isOn)
    {
        DebugText.Instance.debugText.gameObject.SetActive(isOn);
        DebugText.Instance.Log($"{EnabledOrDisabled(isOn)} Debug Logs");
    }

    private void ToggleTransformFreeze(bool isOn)
    {
        DebugText.Instance.Log($"{EnabledOrDisabled(isOn)} Transform Freeze");
    }

    private void ToggleDetection(bool isOn)
    {
        DebugText.Instance.Log($"{EnabledOrDisabled(isOn)} Permanent Detection");
    }

    public float GetGlobalScale()
    {
        return sliderGlobalScale.value;
    }

    public bool IsPermanentDetection()
    {
        return toggleDetection.isOn;
    }

    public bool IsTransformFrozen()
    {
        return toggleTransformFreeze.isOn;
    }
}

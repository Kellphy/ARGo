using System;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    private int maxLogCount = 1000;
    public TextMeshProUGUI debugText;
    string keptText = string.Empty;
    public static DebugText Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Log(string text)
    {
        keptText = $"[{DateTime.Now.Second}] {text}\n{keptText}";
        if (keptText.Length > maxLogCount)
        {
            keptText = keptText[..maxLogCount];
        }
        if (debugText.gameObject.activeSelf)
        {
            debugText.text = keptText;
        }
    }
}

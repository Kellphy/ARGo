using System;
using TMPro;
using UnityEngine;

public class DebugText : MonoBehaviour
{
    public static DebugText instance;
    private void Awake()
    {
        instance = this;
    }
    public TextMeshProUGUI debugText;

    string keptText = string.Empty;

    public void Log(string text)
    {
        keptText = $"{DateTime.Now.Second} {text}\n{keptText}";
        if (keptText.Length > 1000)
        {
            keptText = keptText[..1000];
        }
        if (debugText.gameObject.activeSelf)
        {
            debugText.text = keptText;
        }
    }
}

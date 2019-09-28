using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttemptsText : MonoBehaviour
{
    [SerializeField] Text text;

    private void OnEnable()
    {
        EventManager.AddListener(EventType.ATTEMPS_CHANGED, OnAttemptsChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventType.ATTEMPS_CHANGED, OnAttemptsChanged);
    }

    private void OnAttemptsChanged(object args)
    {
        int attempts = (int)args;
        text.text = "ATTEMPTS: " + attempts;
    }
}

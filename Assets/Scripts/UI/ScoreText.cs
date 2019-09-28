using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour
{
    [SerializeField] Text text;

    private void OnEnable()
    {
        EventManager.AddListener(EventType.SCORE_CHANGED, OnScoreChanged);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventType.SCORE_CHANGED, OnScoreChanged);
    }

    private void OnScoreChanged(object args) {
        int score = (int)args;
        text.text = "SCORE: " + score;
    }
}

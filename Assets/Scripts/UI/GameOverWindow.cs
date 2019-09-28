using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    [SerializeField] Text previousScoreText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject window;

    private void OnEnable()
    {
        EventManager.AddListener(EventType.SHOW_GAME_OVER, OnShowGameOver);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventType.SHOW_GAME_OVER, OnShowGameOver);
    }

    private void OnShowGameOver(object args) {
        int[] scores = (int[])args;
        int previousScore = scores[0];
        int score = scores[1];

        previousScoreText.text = "PREVIOUS SCORE: " + previousScore;
        scoreText.text = "SCORE: " + score;
        window.SetActive(true);
    }

    public void OnRestartButton() {
        EventManager.TriggerEvent(EventType.START_GAME, null);
    }
}

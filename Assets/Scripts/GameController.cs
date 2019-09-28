using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    [SerializeField] int attempts;

    private GameData gameData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        gameData = new GameData();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        EventManager.AddListener(EventType.START_GAME, StartGame);
        EventManager.AddListener(EventType.PLAYER_KILLED, OnPlayerKilled);
        EventManager.AddListener(EventType.ENEMY_DESTROYED, OnEnemyDestroyed);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        EventManager.RemoveListener(EventType.START_GAME, StartGame);
        EventManager.RemoveListener(EventType.PLAYER_KILLED, OnPlayerKilled);
        EventManager.RemoveListener(EventType.ENEMY_DESTROYED, OnEnemyDestroyed);
    }

    private void StartGame(object args) {
        gameData.Attempts = 3;
        gameData.Score = 0;
        SceneManager.LoadScene("Game");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            EventManager.TriggerEvent(EventType.SCORE_CHANGED, gameData.Score);
            EventManager.TriggerEvent(EventType.ATTEMPS_CHANGED, gameData.Attempts);
        }
    }

    private void OnPlayerKilled(object args) {
        gameData.Attempts--;
        EventManager.TriggerEvent(EventType.ATTEMPS_CHANGED, gameData.Score);
        if (gameData.Attempts == 0)
        {
            GameOver();
        }
        else
        {
            EventManager.TriggerEvent(EventType.RESPAWN_PLAYER, null);
        }
    }

    private void GameOver() {
        int[] scoreData = new int[] { gameData.PreviousScore, gameData.Score };
        EventManager.TriggerEvent(EventType.SHOW_GAME_OVER, scoreData);
        gameData.PreviousScore = gameData.Score;
    }

    private void OnEnemyDestroyed(object args)
    {
        int score = (int)args;
        gameData.Score += score;
        EventManager.TriggerEvent(EventType.SCORE_CHANGED, gameData.Score);
    }
}

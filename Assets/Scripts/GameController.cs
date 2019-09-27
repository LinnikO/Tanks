using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    private void OnEnable()
    {
        EventManager.AddListener(EventType.START_GAME, StartGame);
    }

    private void OnDisable()
    {
        EventManager.RemoveListener(EventType.START_GAME, StartGame);
    }

    private void StartGame(object args) {
        SceneManager.LoadScene("Game");
    }
}

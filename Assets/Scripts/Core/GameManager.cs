using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Playing,
    PlayerDied,
    Completed
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameState CurrentState { get; private set; } = GameState.Playing;

    public event Action<GameState> OnStateChanged;
    public event Action OnPlayerDied;
    public event Action OnGameCompleted;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void SetPlayerDied()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.PlayerDied;

        OnStateChanged?.Invoke(CurrentState);
        OnPlayerDied?.Invoke();
    }

    public void SetGameCompleted()
    {
        if (CurrentState != GameState.Playing) return;
        CurrentState = GameState.Completed;

        OnStateChanged?.Invoke(CurrentState);
        OnGameCompleted?.Invoke();
    }

    public void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

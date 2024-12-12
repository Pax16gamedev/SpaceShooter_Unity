using System;
using UnityEngine;

public enum GameState
{
    Running,
    GameOver,
    Paused
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private GameState currentState = GameState.Running;
    public GameState CurrentState => currentState;

    bool isGamePaused;
    public bool IsGamePaused => isGamePaused;

    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void GameOver()
    {
        currentState = GameState.GameOver;

        CanvasUIManager.Instance.ShowEndScreenPanel();
        ScoreManager.Instance.CheckHighScoreAndSet();

        // Opcional: Detener música o reproducir sonido de game over
    }

    public void TogglePause()
    {
        isGamePaused = currentState == GameState.Paused;

        if (isGamePaused)
        {
            UnpauseGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        currentState = GameState.Paused;
        CanvasUIManager.Instance.PauseGame();
        Time.timeScale = 0;
        OnGamePaused?.Invoke();
    }

    private void UnpauseGame()
    {
        currentState = GameState.Running;
        CanvasUIManager.Instance.HideEndScreenPanel();
        CanvasUIManager.Instance.UnpauseGame();
        Time.timeScale = 1;
        OnGameResumed?.Invoke();
    }
}

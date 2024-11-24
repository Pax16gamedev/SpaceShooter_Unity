using System;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    int score = 0;
    public int Score => score;

    private int highScore = 0;
    public int HighScore => highScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        highScore = PlayerPrefs.GetInt(Constants.PLAYER_PREFS.HIGH_SCORE, 0);

        CanvasUIManager.Instance.ChangeScore(score);        
    }

    public void AddScore(int pointsToAdd)
    {
        if (pointsToAdd < 0)
        {
            Debug.LogWarning("No se pueden añadir puntos negativos.");
            return;
        }

        score += pointsToAdd;
        CanvasUIManager.Instance.ChangeScore(score);

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt(Constants.PLAYER_PREFS.HIGH_SCORE, highScore);
        }
    }
}

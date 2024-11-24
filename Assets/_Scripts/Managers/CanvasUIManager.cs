using TMPro;
using UnityEngine;

public class CanvasUIManager : MonoBehaviour
{
    public static CanvasUIManager Instance;

    [SerializeField] TextMeshProUGUI healthTMP;
    [SerializeField] TextMeshProUGUI levelAndWaveTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;

    // Optimizacion
    private int currentLifes = -1;
    private int currentLevel = -1;
    private int currentWave = -1;
    private int currentScore = -1;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ChangeHealth(int health)
    {
        if(currentLevel == health) return;

        healthTMP.text = FormatHealth(health);
    }

    public void ChangeLevelWave(int level, int wave)
    {
        if(level == currentLevel && wave == currentWave) return;

        levelAndWaveTMP.text = FormatLevelAndWave(level, wave);
    }

    public void ChangeScore(int score)
    {
        if(score == currentScore) return;

        scoreTMP.text = FormatScore(score);
    }

    private string FormatHealth(int health) => $"Health: {health}";
    private string FormatLevelAndWave(int level, int wave) => $"Level {level} - Wave {wave}";
    private string FormatScore(int score) => $"Score: {score}";
}

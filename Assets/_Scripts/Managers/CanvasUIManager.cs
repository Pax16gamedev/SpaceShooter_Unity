using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasUIManager : MonoBehaviour
{
    public static CanvasUIManager Instance;

    [Header("Paneles")]
    [SerializeField] GameObject endScreenPanel;

    [Header("Textos")]
    [SerializeField] TextMeshProUGUI healthTMP;
    [SerializeField] TextMeshProUGUI levelAndWaveTMP;
    [SerializeField] TextMeshProUGUI middleScreenTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;
    
    [SerializeField] TextMeshProUGUI deathTMP;
    [SerializeField] TextMeshProUGUI victoryTMP;
    [SerializeField] TextMeshProUGUI finalScoreTMP;

    [Header("Botones")]
    [SerializeField] Button resumeBTN;
    [SerializeField] Button retryBTN;

    // Optimizacion
    private int currentLifes = -1;
    private int currentScore = -1;
    private int currentLevel = -1;
    private int currentWave = -1;

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

    public void ShowLevelAndWaveMiddleScreen(int level, int wave)
    {
        middleScreenTMP.text = FormatLevelAndWave(level, wave);
        middleScreenTMP.gameObject.SetActive(true);
    }

    public void HideLevelAndWaveMiddleScreen() => middleScreenTMP.gameObject.SetActive(false);

    public void ChangeBottomLevelWave(int level, int wave)
    {
        if(level == currentLevel && wave == currentWave) return;

        levelAndWaveTMP.text = FormatLevelAndWave(level, wave);
    }

    public void ChangeScore(int score)
    {
        if(currentScore == score) return;

        currentScore = score;
        scoreTMP.text = FormatScore(score);
    }

    private string FormatHealth(int health) => $"Health: {health}";
    private string FormatLevelAndWave(int level, int wave) => $"Level {level} - Wave {wave}";
    private string FormatScore(int score) => $"Score: {score}";

    public void ShowEndScreenPanel() => endScreenPanel.SetActive(true);
    public void HideEndScreenPanel() => endScreenPanel.SetActive(false);

    public void RetryMainLevel()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(Constants.SCENES.MAIN_LEVEL);
    }

    public void GoBackToMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(Constants.SCENES.MAIN_MENU);
    }

    private void OnEnable()
    {
        PlayerStats.OnPlayerAliveStatusChanged += HandlePlayerAliveStatus;
    }

    private void OnDisable()
    {
        PlayerStats.OnPlayerAliveStatusChanged -= HandlePlayerAliveStatus;
    }

    private void HandlePlayerAliveStatus(bool isAlive)
    {
        if (isAlive) return;

        retryBTN.gameObject.SetActive(true);
        resumeBTN.gameObject.SetActive(false);

        deathTMP.gameObject.SetActive(true);
        victoryTMP.gameObject.SetActive(false);

        finalScoreTMP.text = $"Final Score: \t {currentScore}\n" +
            $"Max Score: \t {ScoreManager.Instance.HighScore}";

        finalScoreTMP.gameObject.SetActive(true);
    }

    public void PauseGame()
    {   
        retryBTN.gameObject.SetActive(false);
        resumeBTN.gameObject.SetActive(true);

        deathTMP.gameObject.SetActive(false);
        victoryTMP.gameObject.SetActive(false);

        finalScoreTMP.text = $"Score: {currentScore}";
        finalScoreTMP.gameObject.SetActive(true);

        ShowEndScreenPanel();
    }

    public void UnpauseGame()
    {
        HideEndScreenPanel();

        retryBTN.gameObject.SetActive(false);
        resumeBTN.gameObject.SetActive(false);

        deathTMP.gameObject.SetActive(false);
        victoryTMP.gameObject.SetActive(false);
        finalScoreTMP.gameObject.SetActive(false);
    }

    public void ShowEndGameScreenVictory()
    {
        retryBTN.gameObject.SetActive(true);
        resumeBTN.gameObject.SetActive(false);

        deathTMP.gameObject.SetActive(false);
        victoryTMP.gameObject.SetActive(true);

        finalScoreTMP.text = $"Final Score: \t {currentScore}\n" +
            $"Max Score: \t {ScoreManager.Instance.HighScore}";
        finalScoreTMP.gameObject.SetActive(true);

        ShowEndScreenPanel();
    }
}

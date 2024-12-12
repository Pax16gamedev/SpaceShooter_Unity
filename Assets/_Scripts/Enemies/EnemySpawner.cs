using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float initialDelay = 2f;
    [SerializeField] float delayBetweenLevels = 2f;
    [SerializeField] LevelSO[] levels;
    [Space]
    [Header("Map Limits")]
    [SerializeField] float ySpawnRange;

    [SerializeField] AudioClip victorySfx;

    int currentLevelIndex;
    int currentWaveIndex;

    void Start()
    {
        CanvasUIManager.Instance.ChangeBottomLevelWave(currentLevelIndex + 1, currentWaveIndex);
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        CanvasUIManager.Instance.ChangeBottomLevelWave(currentLevelIndex + 1, currentWaveIndex + 1);
        CanvasUIManager.Instance.ShowLevelAndWaveMiddleScreen(currentLevelIndex + 1, currentWaveIndex + 1);
        yield return new WaitForSeconds(initialDelay);
        CanvasUIManager.Instance.HideLevelAndWaveMiddleScreen();
        for (currentLevelIndex = 0; currentLevelIndex < levels.Length; currentLevelIndex++)
        {
            LevelSO currentLevel = levels[currentLevelIndex];
            yield return StartCoroutine(SpawnWave(currentLevel));
            CanvasUIManager.Instance.ChangeBottomLevelWave(currentLevelIndex + 1, currentWaveIndex + 1);
            CanvasUIManager.Instance.ShowLevelAndWaveMiddleScreen(currentLevelIndex + 1, currentWaveIndex + 1);
            yield return new WaitForSeconds(delayBetweenLevels);
            CanvasUIManager.Instance.HideLevelAndWaveMiddleScreen();
        }
        AudioManager.Instance.PlaySFX(victorySfx);
        CanvasUIManager.Instance.ShowEndGameScreenVictory();
    }

    IEnumerator SpawnWave(LevelSO level)
    {
        for (currentWaveIndex = 0; currentWaveIndex < level.wave.Length; currentWaveIndex++)
        {
            WaveSO currentWave = level.wave[currentWaveIndex];
            CanvasUIManager.Instance.ChangeBottomLevelWave(currentLevelIndex + 1, currentWaveIndex + 1);
            yield return StartCoroutine(SpawnEnemies(currentWave));
            yield return new WaitForSeconds(level.waveDelayInSeconds);
        }
    }

    IEnumerator SpawnEnemies(WaveSO wave)
    {
        for (int i = 0; i < wave.totalEnemiesPerWave; i++)
        {
            SpawnEnemy(wave);
            yield return new WaitForSeconds(wave.enemyDelayInSeconds);
        }
    }

    void SpawnEnemy(WaveSO wave)
    {
        Vector2 spawnPosition = new Vector2(transform.position.x, Random.Range(-ySpawnRange, ySpawnRange));

        // Enemigos randomizados
        GameObject enemy = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        Instantiate(enemy, spawnPosition, Quaternion.identity);
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

        StopAllCoroutines();
    }
}

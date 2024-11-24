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

    int currentLevelIndex = 0;
    int currentWaveIndex = 0;

    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        yield return new WaitForSeconds(initialDelay);
        for (currentLevelIndex = 0; currentLevelIndex < levels.Length; currentLevelIndex++)
        {
            LevelSO currentLevel = levels[currentLevelIndex];
            yield return StartCoroutine(SpawnWave(currentLevel));
            yield return new WaitForSeconds(delayBetweenLevels);
        }
        print("All levels completed!");
    }

    IEnumerator SpawnWave(LevelSO level)
    {
        for (currentWaveIndex = 0; currentLevelIndex < level.wave.Length; currentWaveIndex++)
        {
            WaveSO currentWave = level.wave[currentWaveIndex];
            CanvasUIManager.Instance.ChangeLevelWave(currentLevelIndex + 1, currentWaveIndex + 1);
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

        GameObject enemy = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

        Instantiate(enemy, spawnPosition, Quaternion.identity);
    }
}

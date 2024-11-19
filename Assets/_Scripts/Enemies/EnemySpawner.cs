using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float delayBetweenLevels = 2f;
    [SerializeField] LevelSO[] levels;
    [Space]
    [Header("More config")]
    [SerializeField] float ySpawnRange;

    int actualLevelIndex = 0;
    int actualWaveIndex = 0;

    void Start()
    {
        StartCoroutine(StartLevel());
    }

    IEnumerator StartLevel()
    {
        for (int i = 0; i < levels.Length; i++)
        {
            actualLevelIndex = i;
            print($"Level {actualLevelIndex + 1}");
            LevelSO actualLevel = levels[actualLevelIndex];
            StartCoroutine(SpawnWave(actualLevel));
            yield return new WaitForSeconds(actualLevel.GetMinimumLevelTime() + delayBetweenLevels);
        }
    }

    IEnumerator SpawnWave(LevelSO level)
    {
        for (int i = 0; i < level.wave.Length; i++)
        {
            actualWaveIndex = i;
            WaveSO actualWave = level.wave[actualWaveIndex];
            CanvasUIManager.Instance.ChangeLevelWave(actualLevelIndex + 1, actualWaveIndex + 1);
            StartCoroutine(SpawnEnemies(actualWave));
            yield return new WaitForSeconds(actualWave.GetMinimumWaveTime() + level.waveDelayInSeconds);
        }
    }

    IEnumerator SpawnEnemies(WaveSO wave)
    {
        for (int i = 0; i < wave.totalEnemiesPerWave; i++)
        {
            Vector2 randomPoint = new Vector2(transform.position.x, Random.Range(-ySpawnRange, ySpawnRange));

            GameObject randomEnemy = wave.enemyPrefabs[Random.Range(0, wave.enemyPrefabs.Length)];

            Instantiate(randomEnemy, randomPoint, Quaternion.identity);
            yield return new WaitForSeconds(wave.enemyDelayInSeconds);
        }
    }
}

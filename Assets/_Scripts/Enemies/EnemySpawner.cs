using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] float delayBetweenLevels = 2f;
    [SerializeField] LevelSO[] levels;
    [Space]
    [Header("Map Limits")]
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
            LevelSO actualLevel = levels[actualLevelIndex];
            yield return StartCoroutine(SpawnWave(actualLevel));
            yield return new WaitForSeconds(delayBetweenLevels);
        }
    }

    IEnumerator SpawnWave(LevelSO level)
    {
        for (int i = 0; i < level.wave.Length; i++)
        {
            actualWaveIndex = i;
            WaveSO actualWave = level.wave[actualWaveIndex];
            CanvasUIManager.Instance.ChangeLevelWave(actualLevelIndex + 1, actualWaveIndex + 1);
            yield return StartCoroutine(SpawnEnemies(actualWave));
            yield return new WaitForSeconds(level.waveDelayInSeconds);
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

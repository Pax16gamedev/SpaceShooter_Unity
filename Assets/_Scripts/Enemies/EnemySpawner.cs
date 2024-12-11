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
        print($"Levels length {levels.Length}");
        yield return new WaitForSeconds(initialDelay);
        for (currentLevelIndex = 0; currentLevelIndex < levels.Length; currentLevelIndex++)
        {
            LevelSO currentLevel = levels[currentLevelIndex];
            yield return StartCoroutine(SpawnWave(currentLevel));

            CanvasUIManager.Instance.ShowLevelAndWaveMiddleScreen(currentLevelIndex + 1, currentWaveIndex + 1);
            yield return new WaitForSeconds(delayBetweenLevels);
            CanvasUIManager.Instance.HideLevelAndWaveMiddleScreen();
        }
        print("All levels completed!");
        AudioManager.Instance.PlaySFX(victorySfx);
    }

    IEnumerator SpawnWave(LevelSO level)
    {
        print($"Level {currentWaveIndex + 1} wave length {level.wave.Length}");
        for (currentWaveIndex = 0; currentLevelIndex < level.wave.Length; currentWaveIndex++)
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
}

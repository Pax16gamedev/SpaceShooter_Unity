using UnityEngine;

[CreateAssetMenu(fileName = "Oleada_", menuName = "Crear oleada")]
public class WaveSO : ScriptableObject
{
    public float enemyDelayInSeconds;

    public int totalEnemiesPerWave;
    public GameObject[] enemyPrefabs;

    public float GetMinimumWaveTime()
    {        
        return totalEnemiesPerWave * enemyDelayInSeconds;
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Oleada_", menuName = "Nivel/Crear oleada")]
public class WaveSO : ScriptableObject
{
    public float enemyDelayInSeconds;

    public int totalEnemiesPerWave;
    public GameObject[] enemyPrefabs;
}

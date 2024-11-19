using UnityEngine;

[CreateAssetMenu(fileName = "Nivel_", menuName = "Crear nivel")]
public class LevelSO : ScriptableObject
{
    public WaveSO[] wave;

    public float waveDelayInSeconds;

    public float GetMinimumLevelTime()
    {
        float time = 0;
        foreach (WaveSO wave in wave)
        {
            time += (wave.enemyDelayInSeconds * wave.totalEnemiesPerWave);
        }

        return time + (wave.Length * waveDelayInSeconds);
    }

}

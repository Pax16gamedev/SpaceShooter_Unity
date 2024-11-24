using UnityEngine;

[CreateAssetMenu(fileName = "Nivel_", menuName = "Nivel/Crear nivel")]
public class LevelSO : ScriptableObject
{
    public WaveSO[] wave;

    public float waveDelayInSeconds;

}

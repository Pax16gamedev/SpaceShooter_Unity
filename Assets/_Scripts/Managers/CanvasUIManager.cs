using TMPro;
using UnityEngine;

public class CanvasUIManager : MonoBehaviour
{
    public static CanvasUIManager Instance;

    [SerializeField] TextMeshProUGUI lifesTMP;
    [SerializeField] TextMeshProUGUI waveTMP;
    [SerializeField] TextMeshProUGUI scoreTMP;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public void ChangeLife(int numLifes)
    {
        lifesTMP.text = $"Lifes: {numLifes}";
    }

    public void ChangeLevelWave(int level, int wave)
    {
        waveTMP.text = $"Level {level} - Wave {wave}";
    }

    public void ChangeScore(int score)
    {
        scoreTMP.text = $"Score: {score}";
    }
}

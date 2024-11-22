using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

        int score = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        CanvasUIManager.Instance.ChangeScore(score);        
    }

    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        CanvasUIManager.Instance.ChangeScore(score);
    }
}

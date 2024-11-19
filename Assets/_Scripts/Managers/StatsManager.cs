using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager Instance;

    [SerializeField] int lifes = 100;
    public int Lifes => lifes;

    int maxLifes;
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
        CanvasUIManager.Instance.ChangeLife(lifes);
        CanvasUIManager.Instance.ChangeScore(score);

        maxLifes = lifes;
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
        lifes = Mathf.Clamp(lifes, 0, maxLifes);
        CanvasUIManager.Instance.ChangeLife(lifes);
    }

    public void AddScore(int pointsToAdd)
    {
        score += pointsToAdd;
        CanvasUIManager.Instance.ChangeScore(score);
    }
}

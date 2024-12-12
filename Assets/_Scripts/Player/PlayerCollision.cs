using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Collision Settings")]
    [SerializeField] int playerCollisionDamage = 20;
    [SerializeField] int enemyCollisionDamage = 20;

    private PlayerStats playerStats;
    private CameraShake camShake;

    private void Awake()
    {
        playerStats = GetComponent<PlayerStats>();
    }

    private void Start()
    {
        camShake = Camera.main.GetComponent<CameraShake>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAGS.ENEMY))
        {
            HandleEnemyCollision(collision);
        }
        if (collision.CompareTag(Constants.TAGS.POWERUP))
        {
            HandlePowerUpCollision(collision);
        }
    }

    private void HandleEnemyCollision(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();

        if (enemy == null) return;

        enemy.TakeDamage(playerCollisionDamage);
        playerStats.TakeDamage(enemyCollisionDamage);
        camShake.TriggerShake();

        var score = collision.GetComponent<Enemy>().ScoreValue;
        ScoreManager.Instance.AddScore(score);

        // VFX
    }

    private void HandlePowerUpCollision(Collider2D collision)
    {
        collision.GetComponent<Powerup>()?.ApplyEffect(gameObject);
    }
}

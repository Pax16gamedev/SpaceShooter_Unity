using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 22f;
    [SerializeField] Vector2 moveDirection = Vector2.right;

    [Header("Shooting")]
    [SerializeField] int damage = 20;

    private ObjectPool<Bullet> pool;
    public ObjectPool<Bullet> Pool { get => pool; set => pool = value; }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAGS.ENEMY))
        {
            HandleEnemyCollision(collision);
        }
        if (collision.CompareTag(Constants.TAGS.PLAYER))
        {
            HandlePlayerCollision(collision);
        }

    }

    // El jugador acierta una bala en un enemigo
    private void HandleEnemyCollision(Collider2D collision)
    {
        var enemy = collision.GetComponent<Enemy>();
        if (enemy == null) return;

        enemy.TakeDamage(damage);
        StatsManager.Instance.AddScore(enemy.ScoreValue);
        AudioManager.Instance.PlaySFX(enemy.DamageSfx);
        Destroy(gameObject);
    }

    // El enemigo acierta una bala en el jugador
    private void HandlePlayerCollision(Collider2D collision)
    {
        var playerStats = collision.gameObject.GetComponent<PlayerStats>();
        if (playerStats == null) return;

        playerStats.TakeDamage(damage);

        Destroy(gameObject);
    }
}

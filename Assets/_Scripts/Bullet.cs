using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 22f;
    [SerializeField] Vector2 moveDirection = Vector2.right;

    [Header("Shooting")]
    [SerializeField] int damage = 20;

    [Header("Audio settings")]
    [SerializeField] [Range(0, 1)] float volumeSfx = 1.0f;
    [SerializeField] AudioClip bulletSfx;

    private ObjectPool<Bullet> pool;
    public ObjectPool<Bullet> Pool { get => pool; set => pool = value; }

    private void Start()
    {
        
    }

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
        if (collision.CompareTag(Constants.enemyTag))
        {
            var enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(damage);

            var score = collision.GetComponent<Enemy>().Score;
            StatsManager.Instance.AddScore(score);
            pool.Release(this);
        }

        if(collision.CompareTag(Constants.playerTag))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            pool.Release(this);
        }
        
    }

    public void ReleaseBullet()
    {
        pool.Release(this);
    }

    public void PlayBulletSfx()
    {
        AudioSource.PlayClipAtPoint(bulletSfx, Camera.main.transform.position, volumeSfx);
    }
}

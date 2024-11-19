using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 22f;
    [SerializeField] Vector2 moveDirection = Vector2.right;

    [Header("Shooting")]
    [SerializeField] int damage = 20;

    [Header("Audio settings")]
    [SerializeField] AudioClip bulletSfx;

    private void Start()
    {
        AudioSource.PlayClipAtPoint(bulletSfx, Camera.main.transform.position);
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
            Destroy(collision.gameObject);            

            var score = collision.GetComponent<Enemy>().Score;
            StatsManager.Instance.AddScore(score);

            // Vfx - instantiate
            // Sfx - get sound from component?
        }

        if(collision.CompareTag(Constants.playerTag))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(damage);
            // Vfx - instantiate
            // Sfx - get sound from component?
        }

        Destroy(gameObject);
    }
}

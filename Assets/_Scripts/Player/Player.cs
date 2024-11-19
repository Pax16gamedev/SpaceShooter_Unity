using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 20f;

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPoint;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int enemyDamageCollision = 20;

    [Header("Limites")]
    [SerializeField] float xLimit = 14;
    [SerializeField] float yLimit = 8;

    const string horizontalInput = "Horizontal";
    const string verticalInput = "Vertical";



    float horizontal;
    float vertical;

    float fireRateTimer;

    private void Start()
    {
        fireRateTimer = fireRate;
    }

    void Update()
    {
        Timers();

        GetInputs();
        Move();

    }

    void Timers()
    {
        fireRateTimer += Time.deltaTime;
    }

    void GetInputs()
    {
        horizontal = Input.GetAxisRaw(horizontalInput);
        vertical = Input.GetAxisRaw(verticalInput);

        if (fireRateTimer >= fireRate && (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)))
        {
            Shoot();
        }
    }

    void Move()
    {
        transform.Translate(new Vector2(horizontal, vertical).normalized * speed * Time.deltaTime, Space.World);
        ClampMovement();
    }

    void ClampMovement()
    {
        float newX = Mathf.Clamp(transform.position.x, -xLimit, xLimit);
        float newY = Mathf.Clamp(transform.position.y, -yLimit, yLimit);

        Vector2 newTransform = new Vector2(newX, newY);
        transform.position = newTransform;
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
        fireRateTimer = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.enemyTag))
        {
            Destroy(collision.gameObject);
            TakeDamage(enemyDamageCollision);
            var score = collision.GetComponent<Enemy>().Score;
            StatsManager.Instance.AddScore(score);
        }
    }

    public void TakeDamage(int damage)
    {
        StatsManager.Instance.TakeDamage(damage);

        if (StatsManager.Instance.Lifes <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Destroy(gameObject);
    }

    private void PickUps(Collider2D collision)
    {
        if (collision.CompareTag(Constants.powerUpTag))
        {
            // collision.GetComponent<PowerUp>.Apply(gameObject) ¿?
        }
    }
}

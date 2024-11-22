using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 20f;

    [Header("Shooting")]
    [SerializeField] Transform[] bulletSpawnPoint;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int playerCollisionDamage = 20;
    [SerializeField] int enemyCollisionDamage = 20;

    [Header("Atributes")]
    [SerializeField] int lifes = 100;

    [Header("SFX")]
    [SerializeField][Range(0, 1)] float volumeSfx = 1.0f;
    [SerializeField] AudioClip damageSfx;

    [Header("Map Limits")]
    [SerializeField] float xLimit = 14;
    [SerializeField] float yLimit = 8;

    const string horizontalInput = "Horizontal";
    const string verticalInput = "Vertical";

    float horizontal;
    float vertical;

    float fireRateTimer;

    int maxLifes;

    BulletPooling bulletPooling;

    private void Awake()
    {
        bulletPooling = GetComponent<BulletPooling>();
    }

    private void Start()
    {
        fireRateTimer = fireRate;
        maxLifes = lifes;

        CanvasUIManager.Instance.ChangeLife(lifes);
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
        // Movement
        horizontal = Input.GetAxisRaw(horizontalInput);
        vertical = Input.GetAxisRaw(verticalInput);

        // Shooting
        if (fireRateTimer >= fireRate)
        {
            if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
            {
                Shoot();
            }
            else if (Input.GetKey(KeyCode.C) || Input.GetMouseButton(1)) {
                ShootMultiple();
            }
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
        var bullet = bulletPooling.InstantiateBullet(bulletSpawnPoint[0]);        
        fireRateTimer = 0;
    }

    void SecondaryShoot()
    {
        var bullet = bulletPooling.InstantiateBullet(bulletSpawnPoint[0]);
        fireRateTimer = 0;
    }

    void ShootMultiple()
    {
        Vector3 playerOffset = new Vector2(3, 3);
        int numBullets = 60;
        float degreesPerShot = 360 / numBullets;
        for(float i = 0; i < 360; i += degreesPerShot)
        {
            var bullet = bulletPooling.InstantiateMultipleBullet(i, playerOffset);            
        }
        // Play one sfx
        fireRateTimer = -10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.enemyTag))
        {
            var enemy = collision.GetComponent<Enemy>();
            enemy.TakeDamage(playerCollisionDamage);

            TakeDamage(enemyCollisionDamage);
            var score = collision.GetComponent<Enemy>().Score;
            StatsManager.Instance.AddScore(score);
        }
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
        lifes = Mathf.Clamp(lifes, 0, maxLifes);

        CanvasUIManager.Instance.ChangeLife(lifes);

        AudioSource.PlayClipAtPoint(damageSfx, Camera.main.transform.position, volumeSfx);
        if (lifes <= 0)
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

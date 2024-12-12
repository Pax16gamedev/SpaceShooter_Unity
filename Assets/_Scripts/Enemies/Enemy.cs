using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 25f;
    [SerializeField] Vector2 moveDirection = Vector2.left;

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float initialWait = 0.5f;
    [SerializeField] float fireRate = 1.5f;


    [Header("Atributes")]
    [SerializeField] int maxHealth = 20;
    [SerializeField] int attackDamage = 20;
    public int AttackDamage => attackDamage;

    private int currentHealth;

    [Header("SFX")]
    [SerializeField][Range(0, 1)] float enemyDamageVolume = 0.5f;
    [SerializeField] private AudioClip damageSfx;
    [SerializeField] private AudioClip shootSfx;

    public AudioClip DamageSfx => damageSfx;

    [Header("Score Value")]
    [SerializeField] int scoreValue = 100;
    public int ScoreValue => scoreValue;

    private VisualFeedback visualFeedback;

    [Header("PowerUp Settings")]
    [SerializeField] PowerupWeight[] powerUps; 
    [SerializeField] float probabilityOfSpawn = 0.5f;

    // TODO: bugfix, object pooling para enemy bullets no funciona

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
        visualFeedback = GetComponentInChildren<VisualFeedback>();

        StartCoroutine(FireBullet());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    IEnumerator FireBullet()
    {
        yield return new WaitForSeconds(initialWait);
        while (true)
        {
            var bulletCopy = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bulletCopy.gameObject.SetActive(true);
            AudioManager.Instance.PlaySFX(shootSfx);
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        currentHealth -= damage;
        visualFeedback.TriggerDamageFeedback();
        AudioManager.Instance.PlaySFX(damageSfx);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        SpawnPowerUp();
        // VFX
    }

    private void SpawnPowerUp()
    {
        if (Random.value <= probabilityOfSpawn && powerUps.Length > 0)
        {
            GameObject powerUpPrefab = GetRandomWeightedPowerUp();
            
            if(powerUpPrefab != null)
            {
                Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            }
        }
    }

    private GameObject GetRandomWeightedPowerUp()
    {
        int totalWeight = 0;
        foreach (var weightedPowerUp in powerUps)
        {
            totalWeight += weightedPowerUp.weight;
        }

        int randomWeight = Random.Range(0, totalWeight);

        foreach (var weightedPowerUp in powerUps)
        {
            if (randomWeight < weightedPowerUp.weight)
            {
                return weightedPowerUp.powerUpPrefab;
            }
            randomWeight -= weightedPowerUp.weight;
        }

        return null; // En caso de que no haya ningun PowerUp seleccionado
    }

}

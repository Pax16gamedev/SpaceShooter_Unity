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
    private int currentHealth;

    [Header("SFX")]
    [SerializeField][Range(0, 1)] float enemyDamageVolume = 0.5f;

    [Header("Score Value")]
    [SerializeField] int scoreValue = 100;
    public int ScoreValue => scoreValue;

    // TODO: bugfix, object pooling para enemy bullets no funciona

    private void Awake()
    {
        currentHealth = maxHealth;
    }

    private void Start()
    {
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
            SFXManager.Instance.PlayEnemyShootSound();
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0) return;

        currentHealth -= damage;
        SFXManager.Instance.PlayEnemyDamageSound(enemyDamageVolume);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
        // VFX
    }

}

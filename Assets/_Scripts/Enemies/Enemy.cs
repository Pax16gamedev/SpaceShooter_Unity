using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 25f;
    [SerializeField] Vector2 moveDirection = Vector2.left;

    [Header("Shooting")]
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float waitTimer = 0.5f;
    [SerializeField] float fireRate = 1.5f;

    [Header("Atributes")]
    [SerializeField] int lifes = 100;

    [Header("SFX")]
    [SerializeField][Range(0, 1)] float volumeSfx = 0.5f;
    [SerializeField] AudioClip damageSfx;

    [Header("Score Value")]
    [SerializeField] int score = 100;

    public int Score => score;

    BulletPooling bulletPooling;

    private void Awake()
    {
        bulletPooling = GetComponent<BulletPooling>();
    }

    private void Start()
    {
        StartCoroutine(SpawnBullet());
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.Translate(moveDirection * speed * Time.deltaTime);
    }

    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(waitTimer);
        while (true)
        {
            var bullet = bulletPooling.InstantiateBullet(bulletSpawnPoint);
            yield return new WaitForSeconds(fireRate);
        }
    }

    public void TakeDamage(int damage)
    {
        lifes -= damage;
        AudioSource.PlayClipAtPoint(damageSfx, Camera.main.transform.position, volumeSfx);
        if (lifes <= 0)
        {
            Destroy(gameObject);
        }
    }

}

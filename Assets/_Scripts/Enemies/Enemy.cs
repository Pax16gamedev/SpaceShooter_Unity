using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 15f;
    [SerializeField] Vector2 moveDirection = Vector2.left;

    [Header("Shooting")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletPoint;
    [SerializeField] float fireRate = 1.5f;

    [Header("Value")]
    [SerializeField] int score = 100;

    public int Score => score;

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
        while (true)
        {
            Instantiate(bulletPrefab, bulletPoint.position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag(bulletTag))
    //    {
    //        Destroy(gameObject);
    //        Destroy(collision.gameObject);
    //    }
    //}
}

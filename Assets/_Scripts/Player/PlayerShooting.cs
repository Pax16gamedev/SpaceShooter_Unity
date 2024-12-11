using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting settings")]
    [SerializeField] Transform[] bulletSpawnPoints;
    [SerializeField] float fireRate = 0.5f;
    [SerializeField] int numBulletsSecondaryShot = 80;

    BulletPooling bulletPooling;
    float fireRateTimer;

    PlayerAudio playerAudio;

    private void Awake()
    {
        bulletPooling = GetComponent<BulletPooling>();
        playerAudio = GetComponent<PlayerAudio>();
    }

    void Start()
    {
        // Para poder disparar desde el principio
        fireRateTimer = fireRate;
    }

    void Update()
    {
        fireRateTimer += Time.deltaTime;
        HandleShooting();
    }

    void HandleShooting()
    {
        if (fireRateTimer < fireRate) return;

        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0))
        {
            Shoot();
        }
        else if (Input.GetKey(KeyCode.C) || Input.GetMouseButton(1))
        {
            ShootMultiple();
        }
    }

    void Shoot()
    {
        var bullet = bulletPooling.InstantiateBullet(bulletSpawnPoints[0]);
        playerAudio.PlayShootSfx();
        fireRateTimer = 0;
    }

    void ShootMultiple()
    {
        float angleStep = 360f / numBulletsSecondaryShot;
        for (int i = 0; i < numBulletsSecondaryShot; i++)
        {
            float angle = i * angleStep;

            bulletPooling.InstantiateMultipleBullet(angle, transform.position + new Vector3(1f, 0));
        }
        playerAudio.PlayShootSfx();
        fireRateTimer = 0;
    }
}

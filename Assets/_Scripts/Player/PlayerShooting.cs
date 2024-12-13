using System.Collections;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [Header("Shooting settings")]
    [SerializeField] Transform[] bulletSpawnPoints;
    [SerializeField] float fireRate = 0.5f;
    //[SerializeField] int numBulletsSecondaryShot = 80;
    [SerializeField] int maxWeapons = 3;

    [SerializeField] int attackDamage = 20;

    int baseAttackDamage;
    int currentAttackDamage;

    public int AttackDamage => attackDamage;

    BulletPooling bulletPooling;
    float fireRateTimer;

    PlayerAudio playerAudio;
    VisualFeedback visualFeedback;

    float numWeapons = 1;

    bool canShoot = true;

    private void Awake()
    {
        bulletPooling = GetComponent<BulletPooling>();
        playerAudio = GetComponent<PlayerAudio>();
        visualFeedback = GetComponentInChildren<VisualFeedback>();
    }

    void Start()
    {
        // Para poder disparar desde el principio
        fireRateTimer = fireRate;
        baseAttackDamage = attackDamage;
        currentAttackDamage = attackDamage;
    }

    void Update()
    {
        if (!canShoot) return;

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
        //else if (Input.GetKey(KeyCode.C) || Input.GetMouseButton(1))
        //{
        //    ShootMultiple();
        //}
    }

    void Shoot()
    {
        for (int i = 0; i < numWeapons; i++)
        {
            var bullet = bulletPooling.InstantiateBullet(bulletSpawnPoints[i]);
        }

        playerAudio.PlayShootSfx();
        fireRateTimer = 0;
    }

    void ShootMultiple()
    {
        //float angleStep = 360f / numBulletsSecondaryShot;
        //for (int i = 0; i < numBulletsSecondaryShot; i++)
        //{
        //    float angle = i * angleStep;

        //    bulletPooling.InstantiateMultipleBullet(angle, transform.position + new Vector3(1f, 0));
        //}
        //playerAudio.PlayShootSfx();
        //fireRateTimer = 0;
    }

    public void AddWeapon()
    {
        if (numWeapons >= maxWeapons) return;
        numWeapons++;
    }

    public void ActivateDamageBoost(float extraDamage, float duration)
    {
        StopCoroutine(DamageBoostCoroutine(extraDamage, duration));
        StartCoroutine(DamageBoostCoroutine(extraDamage, duration));
    }

    private IEnumerator DamageBoostCoroutine(float extraDamage, float duration)
    {
        currentAttackDamage = baseAttackDamage + Mathf.RoundToInt(extraDamage);
        bulletPooling.ChangeToBoostDamageBullet();
        visualFeedback.TriggerDamageBoostColor(duration);
        yield return new WaitForSeconds(duration);
        currentAttackDamage = baseAttackDamage;
        bulletPooling.ResetBulletPrefab();
    }

    private void OnEnable()
    {
        GameManager.OnGamePaused += DisableShooting;
        GameManager.OnGameResumed += EnableShooting;
    }

    private void OnDisable()
    {
        GameManager.OnGamePaused -= DisableShooting;
        GameManager.OnGameResumed -= EnableShooting;
    }

    private void DisableShooting()
    {
        canShoot = false;
    }

    private void EnableShooting()
    {
        canShoot = true;
    }
}

using System;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] int currentHealth = 100;
    [SerializeField] int maxHealth;

    [Header("Shields")]
    [SerializeField] GameObject[] shieldPrefabs;
    [SerializeField] Transform shieldSpawnPoint;

    GameObject shieldSpawnedGO;

    bool isPlayerAlive = true;
    public static event Action<bool> OnPlayerAliveStatusChanged;

    PlayerShooting playerShooting;
    PlayerAudio playerAudio;
    VisualFeedback visualFeedback;
    private CameraShake camShake;

    [SerializeField] float maxShields = 3;
    int activeShields = 0;
    int shieldIndex = -1;

    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
        playerShooting = GetComponent<PlayerShooting>();
    }

    void Start()
    {
        visualFeedback = GetComponentInChildren<VisualFeedback>();
        camShake = Camera.main.GetComponent<CameraShake>();
        CanvasUIManager.Instance.ChangeHealth(currentHealth);
    }

    private void Update()
    {
        // testing
        if (Input.GetKeyUp(KeyCode.T))
        {
            AddShield(activeShields++);
        }
        else if (Input.GetKeyUp(KeyCode.Y))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        if (!isPlayerAlive) return;

        if (activeShields > 0)
        {
            print($"Damage stopped {damage}");
            activeShields--;
            ChangeShield();
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        CanvasUIManager.Instance.ChangeHealth(currentHealth);
        visualFeedback.TriggerDamageFeedback();
        camShake.TriggerShake();
        playerAudio.PlayDamageSfx();
        // VFX

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (!isPlayerAlive) return;

        currentHealth += Mathf.RoundToInt(amount);
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        visualFeedback.TriggerHealFeedback();
        CanvasUIManager.Instance.ChangeHealth(currentHealth);
    }

    private void Die()
    {
        if (!isPlayerAlive) return;

        Debug.Log("Player has died!");
        isPlayerAlive = false;
        Destroy(gameObject);
        // VFX - Explosion o algo
        NotifyPlayerAliveStatus();
        GameManager.Instance.GameOver();
    }

    public void AddShield(float value)
    {
        if (!isPlayerAlive) return;

        bool limitShields = activeShields >= maxShields;
        bool valueLessThanActiveShields = value <= activeShields;
        bool valueZeroOrNegative = value <= 0;

        print($"Shield stats - limitShields {limitShields} | valueLessThanActiveShields - {valueLessThanActiveShields} | valueZeroOrNegative - {valueZeroOrNegative}");

        if (activeShields >= maxShields) return;
        if (value <= activeShields || value <= 0) return;

        activeShields++;
        ChangeShield();
    }

    private void ChangeShield()
    {
        Destroy(shieldSpawnedGO);
        print("Nuevo escudo manin");

        if (activeShields >= 3)
        {
            shieldIndex = 2;
        }
        else if (activeShields == 2)
        {
            shieldIndex = 1;
        }
        else if (activeShields == 1)
        {
            shieldIndex = 0;
        }
        else
        {
            shieldIndex = -1;
        }

        shieldSpawnedGO = Instantiate(shieldPrefabs[shieldIndex], shieldSpawnPoint.position, shieldPrefabs[shieldIndex].transform.rotation);
        shieldSpawnedGO.transform.SetParent(shieldSpawnPoint);
    }

    public int GetDamage() => playerShooting.AttackDamage;

    private void NotifyPlayerAliveStatus()
    {
        OnPlayerAliveStatusChanged?.Invoke(isPlayerAlive);
    }
}

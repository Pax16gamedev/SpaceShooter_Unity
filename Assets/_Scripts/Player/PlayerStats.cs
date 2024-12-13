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

    public void TakeDamage(int damage)
    {
        if (!isPlayerAlive) return;

        camShake.TriggerShake();
        playerAudio.PlayDamageSfx();

        if (activeShields > 0)
        {
            activeShields--;
            ChangeShield();
            return;
        }

        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        CanvasUIManager.Instance.ChangeHealth(currentHealth);
        visualFeedback.TriggerDamageFeedback();
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

        if (activeShields >= maxShields) return;
        if (value <= activeShields || value <= 0) return;

        activeShields = (int)Mathf.Clamp(activeShields + Mathf.RoundToInt(value), 0, maxShields);
        ChangeShield();
    }

    private void ChangeShield()
    {
        if (shieldSpawnedGO != null)
        {
            Destroy(shieldSpawnedGO);
        }

        if (activeShields > 0 && activeShields <= maxShields)
        {
            shieldIndex = Mathf.Clamp(activeShields - 1, 0, shieldPrefabs.Length - 1);
            shieldSpawnedGO = Instantiate(
                shieldPrefabs[shieldIndex],
                shieldSpawnPoint.position,
                shieldPrefabs[shieldIndex].transform.rotation
            );
            shieldSpawnedGO.transform.SetParent(shieldSpawnPoint);
        }
        else
        {
            shieldIndex = -1; // No escudo activo
        }
    }

    public int GetDamage() => playerShooting.AttackDamage;

    private void NotifyPlayerAliveStatus()
    {
        OnPlayerAliveStatusChanged?.Invoke(isPlayerAlive);
    }
}

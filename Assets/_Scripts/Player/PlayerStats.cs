using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] int currentHealth = 100;

    int maxHealth;
    bool isPlayerAlive = true; // Event ?

    PlayerAudio playerAudio;
    DamageFeedback damageFeedback;
    private CameraShake camShake;

    private void Awake()
    {
        playerAudio = GetComponent<PlayerAudio>();
    }

    void Start()
    {
        maxHealth = currentHealth;
        damageFeedback = GetComponentInChildren<DamageFeedback>();
        camShake = Camera.main.GetComponent<CameraShake>();
        CanvasUIManager.Instance.ChangeHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        CanvasUIManager.Instance.ChangeHealth(currentHealth);
        damageFeedback.TriggerDamageFeedback();
        camShake.TriggerShake();
        playerAudio.PlayDamageSfx();
        // VFX

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // VFX

        CanvasUIManager.Instance.ChangeHealth(currentHealth);
    }

    private void Die()
    {
        Debug.Log("Player has died!");
        isPlayerAlive = false;
        Destroy(gameObject);
        // VFX
        // GameOver
    }

}

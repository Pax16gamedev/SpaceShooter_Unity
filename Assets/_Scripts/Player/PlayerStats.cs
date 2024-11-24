using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Atributes")]
    [SerializeField] int currentHealth = 100;

    int maxHealth;

    bool isPlayerAlive = true; // Event ?

    void Start()
    {
        maxHealth = currentHealth;

        CanvasUIManager.Instance.ChangeHealth(currentHealth);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        CanvasUIManager.Instance.ChangeHealth(currentHealth);

        SFXManager.Instance.PlayPlayerDamageSound();
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

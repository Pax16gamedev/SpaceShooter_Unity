using UnityEngine;

public enum PowerUpType
{
    Heal,
    IncreaseWeapons,
    Shield,
    SpeedBoost,
    DamageBoost
}

public class Powerup : MonoBehaviour
{
    [Header("Power-Up Settings")]
    [SerializeField] private PowerUpType powerUpType;
    [SerializeField] private float duration = 5f;
    [SerializeField] private float value = 1;
    [SerializeField] private float timeAlive = 25f;
    [SerializeField] private AudioClip powerUpSfx;

    [Header("Blink config")]
    [SerializeField] float blinkStartTime = 3f; 
    [SerializeField] float blinkInterval = 0.15f;

    public float TimeAlive => timeAlive;
    public AudioClip PowerUpSfx => powerUpSfx;

    private float timeLeftToDespawn;
    private SpriteRenderer spriteRenderer;
    private bool isBlinking;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        timeLeftToDespawn = timeAlive;
        Invoke(nameof(StartBlinking), timeAlive - blinkStartTime);
    }

    private void Update()
    {
        timeLeftToDespawn -= Time.deltaTime;
        if(timeLeftToDespawn < 0 )
        {
            Destroy(gameObject);
        }
    }

    public void ApplyEffect(GameObject player)
    {
        switch (powerUpType)
        {
            case PowerUpType.Heal:
                player.GetComponent<PlayerStats>()?.Heal(value);
                break;

            case PowerUpType.IncreaseWeapons:
                player.GetComponent<PlayerShooting>()?.AddWeapon();
                break;

            case PowerUpType.SpeedBoost:
                player.GetComponent<PlayerMovement>()?.SetSpeedMultiplier(value, duration);
                break;

            case PowerUpType.Shield:
                player.GetComponent<PlayerStats>()?.AddShield(value);
                break;

            case PowerUpType.DamageBoost:
                player.GetComponent<PlayerShooting>()?.ActivateDamageBoost(value, duration);
                break;

            default:
                Debug.LogWarning($"Powerup type {powerUpType} not handled!");
                break;
        }
        AudioManager.Instance.PlaySFX(powerUpSfx);
        Destroy(gameObject);
    }

    void StartBlinking()
    {
        if (isBlinking) return;

        isBlinking = true;
        StartCoroutine(BlinkEffect());
    }

    System.Collections.IEnumerator BlinkEffect()
    {
        while (isBlinking)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}

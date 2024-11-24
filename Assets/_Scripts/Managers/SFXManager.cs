using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [Header("Player SFX")]
    [SerializeField] private AudioClip damageSfx;
    [SerializeField] private AudioClip shootSfx;
    [SerializeField] private AudioClip powerUpSfx;

    [Header("Enemy SFX")]
    [SerializeField] private AudioClip enemyDamageSfx;
    [SerializeField] private AudioClip enemyShootSfx;

    [Header("Volume Settings")]
    [Range(0, 1)] public float sfxVolume = 1.0f;

    Camera cam;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        cam = Camera.main;
    }

    public void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, cam.transform.position, sfxVolume);
    }

    public void PlaySound(AudioClip clip, float volume)
    {
        if (clip == null) return;

        AudioSource.PlayClipAtPoint(clip, cam.transform.position, volume);
    }

    public void PlayPlayerDamageSound()
    {
        PlaySound(damageSfx);
    }

    public void PlayPlayerShootSound()
    {
        PlaySound(shootSfx);
    }

    public void PlayPlayerPowerUpSound()
    {
        PlaySound(powerUpSfx);
    }

    public void PlayEnemyDamageSound(float volume)
    {
        PlaySound(enemyDamageSfx, volume);
    }

    public void PlayEnemyShootSound()
    {
        PlaySound(enemyShootSfx);
    }
}
